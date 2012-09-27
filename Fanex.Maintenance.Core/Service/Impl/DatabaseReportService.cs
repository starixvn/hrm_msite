// -----------------------------------------------------------------------
// <copyright file="DatabaseReportService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Service.Interface;
    using Fanex.NAP.Framework.Data;
    using System.Data;
    using Fanex.Maintenance.Core.Repository.Impl;
    using Fanex.Maintenance.Core.Criteria.Licensee;
    using Fanex.Maintenance.Core.TemplateReport;
    using Fanex.Maintenance.Core.Criteria.User;
    using Fanex.Maintenance.Core.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseReportService : IDatabaseReportService
    {
        #region Member

        private readonly DatabaseReportRepository _databaseReportRepository = new DatabaseReportRepository();

        //This is string use compary values from 4 tables
        static string strCompare = "{0},{1},{2},{3}";

        //This is dictionary use add background color dynamic to field depend compary value from 4 table
        private static readonly Dictionary<string, string> bgColorCss = new Dictionary<string, string>()
        {
            {"BgSBBA","bgSBBA"},
            {"BgSportsbook","bgSportsbook"},
            {"BgBA","bgBA"},
            {"BgRacing","bgRacing"},
            {"BgNumberGame","bgNumberGame"},
        };        

        #endregion

        public DataTable GetSumWinloss(DateTime date, int drawFilter)
        {
            DataSet result = new DataSet();

            var criteria = new GetBalanceUpDayCriteria();
            DataTable balanceUp = this.GetBalanceUpDay(criteria);
            var moveBetsDay = (DateTime)balanceUp.Rows[0][1];
            var balanceUpDay = (DateTime)balanceUp.Rows[0][0];

            var dtSumwinloss = new DataTable();

            if (date < balanceUpDay)
            {
                var criteriaSumOld = new GetDepositDataToValidateSumWLOldCriteria
                {
                    Date = date,
                    DrawFilter = drawFilter
                };
                dtSumwinloss = _databaseReportRepository.GetDataTable(criteriaSumOld);
            }
            else
            {
                var criteriaSumWl = new GetDepositDataToValidateSumWLCriteria
                {
                    Date = date,
                    DrawFilter = drawFilter
                };
                dtSumwinloss = _databaseReportRepository.GetDataTable(criteriaSumWl);
            }
            return dtSumwinloss;
        }

        /// <summary>
        /// BalanceUpDay
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public DataTable GetBalanceUpDay(ICriteria criteria)
        {
            if (criteria.IsValid())
            {
                return _databaseReportRepository.GetDataTable(criteria);
            }

            return null;
        }

        public DataTable GetLicenseeWinloss(DateTime date, int drawFilter)
        {
            var criteria = new GetDepositDataToValidateLicenseeWLCriteria
            {
                Date = date,
                DrawFilter = drawFilter
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public DataTable GetBetting(DateTime date, int drawFilter)
        {
            var criteria = new GetDepositDataToValidateBettingCriteria
            {
                Date = date,
                DrawFilter = drawFilter
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public DataTable GetDW(DateTime date, int drawFilter)
        {
            var criteria = new GetDepositDataToValidateDWCriteria
            {
                Date = date,
                DrawFilter = drawFilter
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public string GenerateTemplateHtmlLicensee(DateTime date, int drawFilter)
        {
            DataTable dtSumwinloss = GetSumWinloss(date, drawFilter);
            DataTable dtRptLicwinloss = GetLicenseeWinloss(date, drawFilter);
            DataTable dtRptBetting = GetBetting(date, drawFilter);
            DataTable dtDW = GetDW(date, drawFilter);

            LicenseedataTemplate temp = new LicenseedataTemplate();
            temp.Turnover.DataSumWinloss = drawFilter != 1 ? SetDataTurnover(dtSumwinloss) :
                                                             SetData(dtSumwinloss, true);
            temp.Turnover.DataLicWinLoss = SetData(dtRptLicwinloss, false);
            temp.Turnover.DataBetting = SetData(dtRptBetting, false);
            temp.Turnover.DataDW = SetData(dtDW, false);

            var turnover = temp.Turnover;
            var winLoss = temp.Winloss; ;
            var betCount = temp.BetCount;

            SetBackgroundColor(dtSumwinloss, dtRptLicwinloss, dtRptBetting, dtDW, drawFilter, turnover, winLoss, betCount);

            return temp.ToString();
        }

        /// <summary>
        /// Method use set data for every block data
        /// 4 blocks : sumwinloss, licwinloss, betting and dw
        /// </summary>
        /// <param name="dt">datatable</param>
        /// <param name="isTurnOver">use check is turnover</param>
        /// <returns>string with value setted</returns>
        protected static string SetData(DataTable dt, bool isTurnOver)
        {
            StringBuilder bd = new StringBuilder();
            var dataBindTemp = new DataBind_Template();
            var dataBindContent = new DataBindContent();
            dt.Columns[4].SetOrdinal(2);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    /* 
                     * case turnover : 
                     *      set sportbook and ba is N/A
                     * case another turnover : 
                     *      sportbook + ba is N/A
                     */
                    if (isTurnOver && j == 2)
                    {
                        dataBindContent.value = "N/A";
                        dataBindContent.Append();
                        dataBindContent.value = "N/A";
                        dataBindContent.Append();
                        continue;
                    }
                    else if (isTurnOver == false && j == 1)
                    {
                        dataBindContent.value = FormatThounsand(Math.Round((decimal)dt.Rows[i][1], 0, MidpointRounding.AwayFromZero) +
                                                                Math.Round((decimal)dt.Rows[i][2], 0, MidpointRounding.AwayFromZero));
                        dataBindContent.Append();
                    }

                    //when value is lastcolumn then add weight border to it
                    if (i != dt.Rows.Count - 1 && j == dt.Columns.Count - 1)
                    {
                        dataBindContent.WeightBoder = "weightBorderBottom";
                    }
                    else
                    {
                        dataBindContent.WeightBoder = string.Empty;
                    }


                    //when value is lastrow in report then add noborder to it
                    if (i == dt.Rows.Count - 1 && j == dt.Columns.Count - 1)
                        dataBindContent.NoBorder = "noBorder";

                    dataBindContent.value = FormatThounsand(dt.Rows[i][j]);
                    dataBindContent.Append();
                    dataBindContent.WeightBoder = string.Empty;
                }

            }
            dataBindTemp.BindData = dataBindContent.ToString();
            dataBindTemp.Append();
            return dataBindTemp.ToString();
        }

        /// <summary>
        /// Method use set values for Turnover when drawfilter another 1
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected static string SetDataTurnover(DataTable dt)
        {
            StringBuilder bd = new StringBuilder();
            var dataBindTemp = new DataBind_Template();
            var dataBindContent = new DataBindContent();
            dt.Columns[4].SetOrdinal(2);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dataBindContent.value = "N/A";
                dataBindContent.Append();
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    if (i != dt.Rows.Count - 1 && j == dt.Columns.Count - 1)
                    {
                        dataBindContent.WeightBoder = "weightBorderBottom";
                    }

                    if (i == dt.Rows.Count - 1 && j == dt.Columns.Count - 1)
                        dataBindContent.NoBorder = "noBorder";

                    dataBindContent.value = "N/A";
                    dataBindContent.Append();
                }
                dataBindContent.WeightBoder = string.Empty;
            }
            dataBindTemp.BindData = dataBindContent.ToString();
            dataBindTemp.Append();
            return dataBindTemp.ToString();
        }

        /// <summary>
        /// Method use set background color depend compare values from 4 tables
        /// </summary>
        /// <param name="dtSumWinloss">table sumwinloss</param>
        /// <param name="dtRptLicWinloss">table licwinloss</param>
        /// <param name="dtRptBetting">table betting</param>
        /// <param name="dtDw">table dw</param>
        /// <param name="drawFilter">draw filter consist 3 values 1,2,3</param>
        /// <param name="blocks">array block</param>
        public static void SetBackgroundColor(DataTable dtSumWinloss, DataTable dtRptLicWinloss,
                                              DataTable dtRptBetting, DataTable dtDw, int drawFilter,
                                              params Block[] blocks)
        {
            bool[] checkValues = CheckValuesTurnOver(dtSumWinloss, dtRptLicWinloss, dtRptBetting, dtDw, drawFilter);

            int i = 0;

            foreach (Block block in blocks)
            {
                block.BgSBBA = checkValues[i] ? bgColorCss["BgSBBA"].ToString() : string.Empty;
                block.BgSportsbook = checkValues[i + 1] ? bgColorCss["BgSportsbook"].ToString() : string.Empty;
                block.BgBA = checkValues[i + 2] ? bgColorCss["BgBA"].ToString() : string.Empty;
                block.BgRacing = checkValues[i + 3] ? bgColorCss["BgRacing"].ToString() : string.Empty;
                block.BgNumberGame = checkValues[i + 4] ? bgColorCss["BgNumberGame"].ToString() : string.Empty;
                i = i + 5;
            }
        }

        /// <summary>
        /// Method used compare data in 4 table SumWinLoss, LicWinloss, Betting, Dw
        /// Compare each cell of table with another table, check they are same or not same
        /// </summary>
        /// <param name="dtSumWinloss">Datatable SumWinloss</param>
        /// <param name="dtRptLicWinloss">Datatable LicWinloss</param>
        /// <param name="dtRptBetting">Datatable Betting</param>
        /// <param name="dtDw">Datatable Dw</param>
        /// <param name="drawFilter">draw filter</param>
        /// <returns>array values true or false</returns>
        public static bool[] CheckValuesTurnOver(DataTable dtSumWinloss, DataTable dtRptLicWinloss,
                                                 DataTable dtRptBetting, DataTable dtDw, int drawFilter)
        {
            bool[] chkAllValues = new bool[15];

            int i = 0;

            for (int j = 0; j < dtSumWinloss.Rows.Count; j++)
            {
                for (int k = 0; k < dtSumWinloss.Columns.Count; k++)
                {
                    if (k == 0)
                    {
                        decimal winLossSBBA = Math.Round((decimal)dtRptLicWinloss.Rows[j][1], 0, MidpointRounding.AwayFromZero) +
                                              Math.Round((decimal)dtRptLicWinloss.Rows[j][2], 0, MidpointRounding.AwayFromZero);
                        decimal bettingSBBA = Math.Round((decimal)dtRptBetting.Rows[j][1], 0, MidpointRounding.AwayFromZero) +
                                              Math.Round((decimal)dtRptBetting.Rows[j][2], 0, MidpointRounding.AwayFromZero);
                        decimal dWSBBA = Math.Round((decimal)dtDw.Rows[j][1], 0, MidpointRounding.AwayFromZero) +
                                         Math.Round((decimal)dtDw.Rows[j][2], 0, MidpointRounding.AwayFromZero);
                        chkAllValues[i] = IsAnother(dtSumWinloss.Rows[j][1], winLossSBBA,
                                                    bettingSBBA, dWSBBA, drawFilter);
                    }

                    else
                    {
                        decimal sumWinlossValue = 0;
                        if (k == 1 || k == 2)
                        {
                            sumWinlossValue = Convert.ToDecimal(dtRptLicWinloss.Rows[j][k]);
                        }
                        else
                        {
                            sumWinlossValue = Convert.ToDecimal(dtSumWinloss.Rows[j][k]);
                        }
                        chkAllValues[i] = IsAnother(sumWinlossValue, dtRptLicWinloss.Rows[j][k],
                                                    dtRptBetting.Rows[j][k], dtDw.Rows[j][k], drawFilter);

                    }

                    i++;
                }

            }
            return chkAllValues;
        }

        /// <summary>
        /// Method use compare values in each column, consist sportbook,
        /// racing, numbergame, and ba
        /// </summary>
        /// <param name="sumwinlossValue">value of sumwinloss</param>
        /// <param name="rptLicwinlossValue">value of rptLicwinloss</param>
        /// <param name="rptBettingValue">value of rptBetting</param>
        /// <param name="dwValue">value of DW</param>
        /// <returns>if have another value then true else then false</returns>
        protected static bool IsAnother(object sumwinlossValue, object rptLicwinlossValue,
                                          object rptBettingValue, object dwValue, int drawFilter)
        {
            string concat = string.Empty;
            if (drawFilter != 1)
                sumwinlossValue = rptLicwinlossValue;
            concat = string.Format(strCompare,
                                         Math.Round((decimal)sumwinlossValue, 0, MidpointRounding.AwayFromZero),
                                         Math.Round((decimal)rptLicwinlossValue, 0, MidpointRounding.AwayFromZero),
                                         Math.Round((decimal)rptBettingValue, 0, MidpointRounding.AwayFromZero),
                                         Math.Round((decimal)dwValue, 0, MidpointRounding.AwayFromZero));
            bool compare = concat.Split(',').Distinct().Count() > 1;
            return compare;
        }

        /// <summary>
        /// Method use format object to number thounsand with seperate is comma
        /// </summary>
        /// <param name="value">object value</param>
        /// <returns>string after format</returns>
        protected static string FormatThounsand(object value)
        {
            string formatThounsand = "{0:#,###,###.#########}";
            return (decimal)value == 0 ? "0" : string.Format(formatThounsand, Math.Round((decimal)value, 0, MidpointRounding.AwayFromZero));
        }

        public IList<bool> CompareValues(DataTable dtBetting, DataTable dtDW, int rowIndex)
        {
            var chkAllValues = new List<bool>();

            for (int i = 1; i < dtBetting.Columns.Count; i++)
            {
                chkAllValues.Add(Math.Round(Convert.ToDecimal(dtBetting.Rows[rowIndex][i])) ==
                                 Math.Round(Convert.ToDecimal(dtDW.Rows[rowIndex][i])));
            }

            return chkAllValues;
        }

        public DataTable CreditDataBetting(DateTime date, int drawFilter)
        {
            var criteria = new GetCreditDataToValidateBettingCriteria
            {
                Date = date,
                DrawFilter = drawFilter
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public DataTable CreditDataDW(DateTime date, int drawFilter)
        {
            var criteria = new GetCreditDataToValidateDWCriteria
            {
                Date = date,
                DrawFilter = drawFilter
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public string GenerateTemplateHtmlCredit(DateTime date, int drawFilter)
        {
            var temp = new TemplateCredit_Template();

            //Get block, they are container data get from database
            var turnover = temp.Turnover;
            var betCount = temp.BetCount;
            var customerWinLoss = temp.CustomerWinLoss;

            DataTable dtBetting = CreditDataBetting(date, drawFilter);
            DataTable dtDW = CreditDataDW(date, drawFilter);

            //Push data to block
            SetDataTurnover(dtBetting, dtDW, turnover);
            SetDataCustomerWL(dtBetting, dtDW, customerWinLoss);
            SetDataBetCount(dtBetting, dtDW, betCount);

            //Set background and check data error
            var checkTurnover = SetBackgroundColorTurnover(dtBetting, dtDW, turnover);
            var checkCustomer = SetBackgroundColorCustomerWL(dtBetting, dtDW, customerWinLoss);
            var checkBetCount = SetBackgroundColorBetCount(dtBetting, dtDW, betCount);

            return temp.ToString();
        }

        #region Set Data

        protected string SetDataTurnover(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetData(dtBetting, dtDW, block, 0);
        }

        protected string SetDataCustomerWL(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetData(dtBetting, dtDW, block, 1);
        }

        protected string SetDataBetCount(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetData(dtBetting, dtDW, block, 2);
        }

        protected string SetData(DataTable dtBetting, DataTable dtDW, BlockCredit block, int rowIndex)
        {
            block.SBBetting = FormatThounsand(dtBetting.Rows[rowIndex][1]);
            block.SBDW = FormatThounsand(dtDW.Rows[rowIndex][1]);

            block.RacingBetting = FormatThounsand(dtBetting.Rows[rowIndex][2]);
            block.RacingDW = FormatThounsand(dtDW.Rows[rowIndex][2]);

            block.NGBetting = FormatThounsand(dtBetting.Rows[rowIndex][3]);
            block.NGDW = FormatThounsand(dtDW.Rows[rowIndex][3]);

            return block.ToString();
        }

        #endregion

        #region Set background

        protected bool SetBackgroundColorTurnover(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetBackgroundColor(dtBetting, dtDW, block, 0);
        }

        protected bool SetBackgroundColorCustomerWL(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetBackgroundColor(dtBetting, dtDW, block, 1);
        }

        protected bool SetBackgroundColorBetCount(DataTable dtBetting, DataTable dtDW, BlockCredit block)
        {
            return SetBackgroundColor(dtBetting, dtDW, block, 2);
        }

        public bool SetBackgroundColor(DataTable dtBetting, DataTable dtDW, BlockCredit block, int rowIndex)
        {
            var checkValues = CompareValues(dtBetting, dtDW, rowIndex);

            block.BgSportsbook = checkValues[0] ? string.Empty : bgColorCss["BgSportsbook"].ToString();
            block.BgRacing = checkValues[1] ? string.Empty : bgColorCss["BgRacing"].ToString();
            block.BgNumberGame = checkValues[2] ? string.Empty : bgColorCss["BgNumberGame"].ToString();

            return checkValues.Contains(false) ? false : true;
        }

        #endregion

        public DataTable GetUserOnlineDaily(DateTime date)
        {
            var criteria = new GetUserOnlineDailyCriteria
            {
                Date = date
            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public string GenerateTemplateUserDaily(DataTable data)
        {
            StringBuilder _s = new StringBuilder();
            foreach (DataRow dr in data.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + dr["timerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["localtimerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["acc"].ToString() + "</td>");
                _s.Append("<td>" + dr["aff"].ToString() + "</td>");
                _s.Append("<td>" + dr["affadmin"].ToString() + "</td>");
                _s.Append("</tr>");
            }
            return _s.ToString();
        }

        public DataTable GetUserOnline(int top)
        {
            var criteria = new GetUserOnlineCriteria
            {

            };
            return _databaseReportRepository.GetDataTable(criteria);
        }

        public string GenerateTemplateUser(DataTable data, int show)
        {
            StringBuilder _s = new StringBuilder();
            foreach (DataRow i in data.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + i["logdate"].ToString() + "</td>");
                _s.Append("<td>" + i["totalAgent"].ToString() + "</td>");
                _s.Append("<td>" + i["super"].ToString() + "</td>");
                _s.Append("<td>" + i["master"].ToString() + "</td>");
                _s.Append("<td>" + i["agent"].ToString() + "</td>");
                _s.Append("<td>" + i["accounting"].ToString() + "</td>");
                _s.Append("<td>" + i["affiliate"].ToString() + "</td>");
                _s.Append("</tr>");
            }
            return _s.ToString();
        }

        private static DataTable GenerateDataTable(DataTable dtb, int RowNumber)
        {
            string _datetime = dtb.Rows[0]["logdate"].ToString();
            for (int i = 0; i < RowNumber; i++)
            {
                string _date = _datetime.Substring(0, _datetime.IndexOf(" ")).Trim();
                string _hour = _datetime.Substring(_datetime.IndexOf(" "), _datetime.Length - _datetime.IndexOf(" ")).Trim();
                string[] Arr = _hour.Split(':');
                DataRow dr = dtb.NewRow();
                if (Convert.ToInt32(Arr[1]) == 60)
                {
                    int myhour = Convert.ToInt32(Arr[0]) + AppSetting.Minute;
                    int myminute = Convert.ToInt32(Arr[1]) / 60;
                    _datetime = _date + " " + (myhour < 10 ? "0" + myhour.ToString() : myhour.ToString()) + ":" + (myminute < 10 ? "0" + myminute.ToString() : myminute.ToString());
                }
                else
                {
                    int _minute = Convert.ToInt32(Arr[1]) + AppSetting.Minute;

                    _datetime = _date + " " + Arr[0] + ":" + (_minute < 10 ? "0" + _minute.ToString() : _minute.ToString());
                }
                dr[0] = _datetime;
                dr[1] = "-1";
                dr[2] = "-1";
                dr[3] = "-1";
                dr[4] = "-1";
                dr[5] = "-1";
                dr[6] = "-1";
                dtb.Rows.Add(dr);
            }
            return dtb;
        }

        public string GenerateDataChart(DataTable dtb, int show)
        {
            string data = "";
            if (dtb.Rows.Count > 0)
            {
                if (show > dtb.Rows.Count)
                {
                    int addRow = show - dtb.Rows.Count;
                    dtb = GenerateDataTable(dtb, addRow);
                }
                DataRow[] drs = dtb.Select(null, "logdate desc");

                foreach (DataRow dr in drs)
                {

                    data += ChangeHourToMinute(dr["logdate"].ToString()) + ";" + dr["super"] + ";" + dr["master"] + ";" + dr["agent"] + ";" + dr["accounting"] + ";" + dr["affiliate"] + "^";
                }
                data = data.Substring(0, data.Length - 1);
            }
            return data;
        }

        public static string ChangeHourToMinute(string _DateTime)
        {
            string ngay = _DateTime.Substring(0, _DateTime.IndexOf(" "));
            string hour = _DateTime.Substring(_DateTime.IndexOf(" "), _DateTime.Length - _DateTime.IndexOf(" "));
            string[] arrList = hour.Split(":".ToCharArray());
            int minute = (Convert.ToInt32(arrList[0]) * 60) + Convert.ToInt32(arrList[1]);
            return minute.ToString();
        }

        public static string SplitDateToHour(string date)
        {
            return date = date.Substring(date.Length - 5, 5);
        }

        public static string ChangeMinuteToHour(int minute)
        {
            int hour = minute / 60;
            int min = minute % 60;
            return hour.ToString() + ":" + min.ToString();
        }

    }
}
