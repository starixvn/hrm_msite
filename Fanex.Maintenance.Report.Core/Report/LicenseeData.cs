using System;
using System.Data;
using System.Linq;
using System.Text;
using IBCMaintenance.Configuration;
using DNAide.Data;
using System.Collections;
using System.Collections.Generic;
using DNAide.Core;

namespace Fanex.Maintenance.Report.Core.Report
{
    public partial class GenerateHTML
    {
        #region Field

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

        #region Set Data and Color For Report

        /// <summary>
        /// Method use set all data for report
        /// </summary>
        /// <param name="date"></param>
        /// <param name="drawFilter"></param>
        /// <returns></returns>
        public static string ToString(DateTime date, int drawFilter)
        {
            StoreName sp = new StoreName();
            StringBuilder _s = new StringBuilder();
            LicenseedataTemplate temp = new LicenseedataTemplate();

            // Get sp name for table sum winloss
            CommonDao commonDao = new CommonDao();
            DataTable balanceUp = commonDao.Lrf_SelBalanceUpDay();
            var moveBetsDay = (DateTime)balanceUp.Rows[0][1];
            var balanceUpDay = (DateTime)balanceUp.Rows[0][0];
            var sumWinLossSpName = date < balanceUpDay ? sp.GET_DepositDataToValidate_SumWLOld : sp.GET_DepositDataToValidate_SumWL;

            DataTable dtSumwinloss = GetData(sumWinLossSpName, date, drawFilter);
            DataTable dtRptLicwinloss = GetData(sp.GET_DepositDataToValidate_LicenseeWL, date, drawFilter);
            DataTable dtRptBetting = GetData(sp.GET_DepositDataToValidate_Betting, date, drawFilter);
            DataTable dtDW = GetData(sp.GET_DepositDataToValidate_DW, date, drawFilter);

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

        private static void WriteLog(string message)
        {
            var path = @"E:\IBCStandby\mSite\bin\a.txt";
            using (var stream = new System.IO.StreamWriter(path, true))
            {
                stream.WriteLine(message);
            }
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

        #endregion

        #region Get Data From Database

        /// <summary>
        /// Method use get data from database depend date and drawfilter
        /// date range is limited within balance up day and move bet day
        /// drawfilter with 1 = normal, 2 = no draw, 3 = no draw for turnover only
        /// </summary>
        /// <param name="spName">name of storedprocedure</param>
        /// <param name="date">date choose in balance up date and move bet day</param>
        /// <param name="drawFilter">int value 1, 2, 3</param>
        /// <returns>datatable consist 5 columns category, sportbook+ba, racing, numbergame, ba</returns>
        protected static DataTable GetData(string spName, DateTime date, int drawFilter)
        {
            Hashtable htb = new Hashtable()
            {
                {"date", date},
                {"drawfilter", drawFilter}
            };
            DataTable dt = DbHelper.Execute(spName, htb, DatabaseType.LICENSEE).ResultSet.Tables[0];
            return dt;
        }

        #endregion

        #region Utility

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

        #endregion
    }
}
