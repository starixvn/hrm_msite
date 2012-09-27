using System;
using System.Data;
using System.Collections.Generic;
using IBCMaintenance.Configuration;

namespace Fanex.Maintenance.Report.Core.Report
{
    public partial class GenerateHTML
    {
        #region Fields

        private static readonly Dictionary<string, string> bgColorCredit = new Dictionary<string, string>()
        {
            {"BgSBBA","bgSBBA"},
            {"BgSportsbook","bgSportsbook"},
            {"BgBA","bgBA"},
            {"BgRacing","bgRacing"},
            {"BgNumberGame","bgNumberGame"},
        };

        #endregion

        public string ToCreditString(DateTime date, int drawFilter)
        {
            return CreditBody(date, drawFilter);
        }

        /// <summary>
        /// Method use to return string mail body 
        /// </summary>
        /// <param name="date">date move bet</param>
        /// <param name="drawFilter">consist 3 values 1, 2, 3</param>
        /// <param name="title">return title has error or not error</param>
        /// <returns>string email body</returns>
        public string CreditBody(DateTime date, int drawFilter)
        {
            StoreName sp = new StoreName();

            var temp = new TemplateCredit_Template();

            //Get block, they are container data get from database
            var turnover = temp.Turnover;
            var betCount = temp.BetCount;
            var customerWinLoss = temp.CustomerWinLoss;

            //Get data from database
            DataTable dtBetting = GetData(sp.GET_CreditDataToValidate_Betting, date, drawFilter);
            DataTable dtDW = GetData(sp.GET_CreditDataToValidate_DW, date, drawFilter);

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
            block.SBBetting = FormatCreditThounsand(dtBetting.Rows[rowIndex][1]);
            block.SBDW = FormatCreditThounsand(dtDW.Rows[rowIndex][1]);

            block.RacingBetting = FormatCreditThounsand(dtBetting.Rows[rowIndex][2]);
            block.RacingDW = FormatCreditThounsand(dtDW.Rows[rowIndex][2]);

            block.NGBetting = FormatCreditThounsand(dtBetting.Rows[rowIndex][3]);
            block.NGDW = FormatCreditThounsand(dtDW.Rows[rowIndex][3]);

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

            block.BgSportsbook = checkValues[0] ? string.Empty : bgColorCredit["BgSportsbook"].ToString();
            block.BgRacing = checkValues[1] ? string.Empty : bgColorCredit["BgRacing"].ToString();
            block.BgNumberGame = checkValues[2] ? string.Empty : bgColorCredit["BgNumberGame"].ToString();

            return checkValues.Contains(false) ? false : true;
        }

        #endregion

        #region Utility

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

        #endregion

        protected static string FormatCreditThounsand(object value)
        {
            string formatThounsand = "{0:#,###,###}";
            return Convert.ToDecimal(value) == 0 || Math.Round(Convert.ToDecimal(value)) == 0 ? "0" : string.Format(formatThounsand, Math.Round(Convert.ToDecimal(value))); 
        }
    }
}
