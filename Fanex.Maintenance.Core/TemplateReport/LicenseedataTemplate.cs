// --------------------------------------------------------------------------
// <copyright file="LicenseedataTemplate.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.TemplateReport
{
    using System.Text;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Licensee data Template
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class LicenseedataTemplate
    {
        public LicenseedataTemplate()
        {
            Turnover = new LicenseedataTemplate_Turnover_Block();
            Winloss = new LicenseedataTemplate_Winloss_Block();
            BetCount = new LicenseedataTemplate_BetCount_Block();
        }

        private StringBuilder _stringBuilder = new StringBuilder();

        /// <summary>
        /// Turn Over
        /// </summary>
        public LicenseedataTemplate_Turnover_Block Turnover { get; set; }

        /// <summary>
        /// Winloss
        /// </summary>
        public LicenseedataTemplate_Winloss_Block Winloss { get; set; }

        /// <summary>
        /// Betcount
        /// </summary>
        public LicenseedataTemplate_BetCount_Block BetCount { get; set; }

        /// <summary>
        /// s1
        /// </summary>
        protected static string _s1 = "</table>";

        /// <summary>
        /// To String
        /// </summary>
        /// <param name="bd"></param>
        public void ToString(ref StringBuilder bd)
        {
            bd.Append("<table class='tblLicenseedata' >");
            bd.Append("<thead><tr>");
            bd.Append("<th colspan='2'>DEPOSIT SITES</th>");
            bd.Append("<th>sum_winloss</th>");
            bd.Append("<th>rptLicWinLoss</th>");
            bd.Append("<th>rpt_betting</th>");
            bd.Append("<th>DW</th>");
            bd.Append("</tr></thead>");
            if (null != Turnover)
            {
                Turnover.ToString(ref bd);
            }

            if (null != Winloss)
            {
                Winloss.ToString(ref bd);
            }

            if (null != BetCount)
            {
                BetCount.ToString(ref bd);
            }

            bd.Append(_s1);
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder bd = new StringBuilder();
            ToString(ref bd);

            return bd.ToString();
        }
    }

    /// <summary>
    /// LicenseedataTemplate_Turnover_Block
    /// </summary>
    public class LicenseedataTemplate_Turnover_Block : Block
    {
        /// <summary>
        /// Field
        /// </summary>
        private static string _s2 = "<tr><td rowspan=\"5\" class=\"b firstCol weightBorderBottom\">Turnover</td><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgSBBA = "{BgSBBA}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s3 = "\">SB + BA</td><td rowspan=\"15\">";

        /// <summary>
        /// Field
        /// </summary>
        public string DataSumWinloss = "{DataSumWinloss}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s4 = "</td><td rowspan=\"15\">";

        /// <summary>
        /// Field
        /// </summary>
        public string DataLicWinLoss = "{DataLicWinLoss}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s5 = "</td><td rowspan=\"15\">";

        /// <summary>
        /// Field
        /// </summary>
        public string DataBetting = "{DataBetting}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s6 = "</td><td rowspan=\"15\">";

        /// <summary>
        /// Field
        /// </summary>
        public string DataDW = "{DataDW}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s7 = "</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgSportsbook = "{BgSportsbook}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s8 = "\">Sportsbook</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgBA = "{BgBA}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s9 = "\">BA</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgRacing = "{BgRacing}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s10 = "\">Racing</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgNumberGame = "{BgNumberGame}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s11 = " weightBorderBottom\">Number Game</td></tr>";

        /// <summary>
        /// To String
        /// </summary>
        public void ToString(ref StringBuilder bd)
        {
            SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);

            bd.Append(_s2);
            bd.Append(BgSBBA);
            bd.Append(_s3);
            bd.Append(DataSumWinloss);
            bd.Append(_s4);
            bd.Append(DataLicWinLoss);
            bd.Append(_s5);
            bd.Append(DataBetting);
            bd.Append(_s6);
            bd.Append(DataDW);
            bd.Append(_s7);
            bd.Append(BgSportsbook);
            bd.Append(_s8);
            bd.Append(BgBA);
            bd.Append(_s9);
            bd.Append(BgRacing);
            bd.Append(_s10);
            bd.Append(BgNumberGame);
            bd.Append(_s11);
        }
    }

    /// <summary>
    /// LicenseedataTemplate_Winloss_Block
    /// </summary>
    public class LicenseedataTemplate_Winloss_Block : Block
    {
        /// <summary>
        /// Field
        /// </summary>
        private static string _s12 = "<tr><td rowspan=\"5\" class=\"b weightBorderBottom\">Customer Win Loss</td><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgSBBA = "{BgSBBA}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s13 = "\">SB + BA</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgSportsbook = "{BgSportsbook}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s14 = "\">Sportsbook</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgBA = "{BgBA}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s15 = "\">BA</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgRacing = "{BgRacing}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s16 = "\">Racing</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        public string BgNumberGame = "{BgNumberGame}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s17 = " weightBorderBottom\">Number Game</td></tr>";

        /// <summary>
        /// To String
        /// </summary>
        public void ToString(ref StringBuilder bd)
        {
            SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);
            bd.Append(_s12);
            bd.Append(BgSBBA);
            bd.Append(_s13);
            bd.Append(BgSportsbook);
            bd.Append(_s14);
            bd.Append(BgBA);
            bd.Append(_s15);
            bd.Append(BgRacing);
            bd.Append(_s16);
            bd.Append(BgNumberGame);
            bd.Append(_s17);
        }
    }

    /// <summary>
    /// LicenseedataTemplate_BetCount_Block
    /// </summary>
    public class LicenseedataTemplate_BetCount_Block : Block
    {
        /// <summary>
        /// Field
        /// </summary>
        private static string _s18 = "<tr><td rowspan=\"5\" class=\"b\">Bet Count</td><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s19 = "\">SB + BA</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s20 = "\">Sportsbook</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s21 = "\">BA</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s22 = "\">Racing</td></tr><tr><td class=\"l ";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s23 = "\">Number Game</td></tr>";

        /// <summary>
        /// Field
        /// </summary>
        public LicenseedataTemplate_BetCount_Block()
        {
            BgSBBA = "{BgSBBA}";
            BgSportsbook = "{BgSportsbook}";
            BgBA = "{BgBA}";
            BgRacing = "{BgRacing}";
            BgNumberGame = "{BgNumberGame}";
        }

        /// <summary>
        /// To string template
        /// </summary>
        /// <param name="bd"></param>
        public void ToString(ref StringBuilder bd)
        {
            //SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);
            bd.Append(_s18);
            bd.Append(BgSBBA);
            bd.Append(_s19);
            bd.Append(BgSportsbook);
            bd.Append(_s20);
            bd.Append(BgBA);
            bd.Append(_s21);
            bd.Append(BgRacing);
            bd.Append(_s22);
            bd.Append(BgNumberGame);
            bd.Append(_s23);
        }
    }

    /// <summary>
    /// DataBind_Template
    /// </summary>
    public class DataBind_Template
    {
        /// <summary>
        /// Field
        /// </summary>
        StringBuilder _bd = new StringBuilder();

        /// <summary>
        /// Field
        /// </summary>
        private static string _s0 = "<table class=\"tblData\" cellpadding=\"0px\" cellspacing=\"0px\">";

        /// <summary>
        /// Field
        /// </summary>
        public string BindData = "{BindData}";

        /// <summary>
        /// Field
        /// </summary>
        private static string _s1 = "</table>";

        /// <summary>
        /// To string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _bd.ToString();
        }

        /// <summary>
        /// Append string
        /// </summary>
        public void Append()
        {
            _bd.Append(_s0);
            _bd.Append(BindData);
            _bd.Append(_s1);
        }
    }

    /// <summary>
    /// DataBindContent
    /// </summary>
    public class DataBindContent
    {
        /// <summary>
        /// Field
        /// </summary>
        StringBuilder bd = new StringBuilder();

        /// <summary>
        /// Field
        /// </summary>
        private static string _s0 = "<tr><td class=\"number ";

        /// <summary>
        /// Field
        /// </summary>
        public string NoBorder = string.Empty;

        /// <summary>
        /// Field
        /// </summary>
        private static string _s1 = " ";

        /// <summary>
        /// Field
        /// </summary>
        public string WeightBoder = string.Empty;

        /// <summary>
        /// Field
        /// </summary>
        private static string _s2 = "\">";

        /// <summary>
        /// Field
        /// </summary>
        public string value;

        /// <summary>
        /// Field
        /// </summary>
        private static string _s3 = "</td></tr>";

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return bd.ToString();
        }

        /// <summary>
        /// Append string
        /// </summary>
        public void Append()
        {
            bd.Append(_s0);
            bd.Append(NoBorder);
            bd.Append(_s1);
            bd.Append(WeightBoder);
            bd.Append(_s2);
            bd.Append(value);
            bd.Append(_s3);
        }
    }
}
