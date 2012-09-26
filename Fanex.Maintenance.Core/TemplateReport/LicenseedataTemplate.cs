using System.Text;

namespace Fanex.Maintenance.Core.TemplateReport
{
    public class LicenseedataTemplate
    {
        StringBuilder bd = new StringBuilder();
        protected static string s0 = "<table class=\"grid\" cellspacing=\"0px\"><tr><td colspan=\"2\" style=\"background-color: #B64926; text-align:center; color:WHITE;font-weight: bold\">DEPOSIT SITES</td><td class=\"tblHeader\">sum_winloss</td><td class=\"tblHeader\">rptLicWinLoss</td><td class=\"tblHeader\">rpt_betting</td><td class=\"tblHeader\">DW</td></tr>";
        public LicenseedataTemplate_Turnover_Block Turnover = new LicenseedataTemplate_Turnover_Block();
        public LicenseedataTemplate_Winloss_Block Winloss = new LicenseedataTemplate_Winloss_Block();
        public LicenseedataTemplate_BetCount_Block BetCount = new LicenseedataTemplate_BetCount_Block();
        protected static string s1 = "</table>";

        public void ToString(ref StringBuilder bd)
        {
            bd.Append(s0);
            if (null != Turnover) Turnover.ToString(ref bd);
            if (null != Winloss) Winloss.ToString(ref bd);
            if (null != BetCount) BetCount.ToString(ref bd);
            bd.Append(s1);
        }
        public override string ToString()
        {
            StringBuilder bd = new StringBuilder();
            ToString(ref bd);
            return bd.ToString();
        }
    }

    public class LicenseedataTemplate_Turnover_Block : Block
    {
        protected static string s2 = "<tr><td rowspan=\"5\" class=\"b firstCol weightBorderBottom\">Turnover</td><td class=\"l ";
        public string BgSBBA = "{BgSBBA}";
        protected static string s3 = "\">SB + BA</td><td rowspan=\"15\">";
        public string DataSumWinloss = "{DataSumWinloss}";
        protected static string s4 = "</td><td rowspan=\"15\">";
        public string DataLicWinLoss = "{DataLicWinLoss}";
        protected static string s5 = "</td><td rowspan=\"15\">";
        public string DataBetting = "{DataBetting}";
        protected static string s6 = "</td><td rowspan=\"15\">";
        public string DataDW = "{DataDW}";
        protected static string s7 = "</td></tr><tr><td class=\"l ";
        public string BgSportsbook = "{BgSportsbook}";
        protected static string s8 = "\">Sportsbook</td></tr><tr><td class=\"l ";
        public string BgBA = "{BgBA}";
        protected static string s9 = "\">BA</td></tr><tr><td class=\"l ";
        public string BgRacing = "{BgRacing}";
        protected static string s10 = "\">Racing</td></tr><tr><td class=\"l ";
        public string BgNumberGame = "{BgNumberGame}";
        protected static string s11 = " weightBorderBottom\">Number Game</td></tr>";
        public void ToString(ref StringBuilder bd)
        {
            SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);

            bd.Append(s2);
            bd.Append(BgSBBA);
            bd.Append(s3);
            bd.Append(DataSumWinloss);
            bd.Append(s4);
            bd.Append(DataLicWinLoss);
            bd.Append(s5);
            bd.Append(DataBetting);
            bd.Append(s6);
            bd.Append(DataDW);
            bd.Append(s7);
            bd.Append(BgSportsbook);
            bd.Append(s8);
            bd.Append(BgBA);
            bd.Append(s9);
            bd.Append(BgRacing);
            bd.Append(s10);
            bd.Append(BgNumberGame);
            bd.Append(s11);
        }
    }

    public class LicenseedataTemplate_Winloss_Block : Block
    {
        protected static string s12 = "<tr><td rowspan=\"5\" class=\"b weightBorderBottom\">Customer Win Loss</td><td class=\"l ";
        public string BgSBBA = "{BgSBBA}";
        protected static string s13 = "\">SB + BA</td></tr><tr><td class=\"l ";
        public string BgSportsbook = "{BgSportsbook}";
        protected static string s14 = "\">Sportsbook</td></tr><tr><td class=\"l ";
        public string BgBA = "{BgBA}";
        protected static string s15 = "\">BA</td></tr><tr><td class=\"l ";
        public string BgRacing = "{BgRacing}";
        protected static string s16 = "\">Racing</td></tr><tr><td class=\"l ";
        public string BgNumberGame = "{BgNumberGame}";
        protected static string s17 = " weightBorderBottom\">Number Game</td></tr>";
        public void ToString(ref StringBuilder bd)
        {
            SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);
            bd.Append(s12);
            bd.Append(BgSBBA);
            bd.Append(s13);
            bd.Append(BgSportsbook);
            bd.Append(s14);
            bd.Append(BgBA);
            bd.Append(s15);
            bd.Append(BgRacing);
            bd.Append(s16);
            bd.Append(BgNumberGame);
            bd.Append(s17);
        }
    }

    public class LicenseedataTemplate_BetCount_Block : Block
    {
        protected static string s18 = "<tr><td rowspan=\"5\" class=\"b\">Bet Count</td><td class=\"l ";
        public string BgSBBA = "{BgSBBA}";
        protected static string s19 = "\">SB + BA</td></tr><tr><td class=\"l ";
        public string BgSportsbook = "{BgSportsbook}";
        protected static string s20 = "\">Sportsbook</td></tr><tr><td class=\"l ";
        public string BgBA = "{BgBA}";
        protected static string s21 = "\">BA</td></tr><tr><td class=\"l ";
        public string BgRacing = "{BgRacing}";
        protected static string s22 = "\">Racing</td></tr><tr><td class=\"l ";
        public string BgNumberGame = "{BgNumberGame}";
        protected static string s23 = "\">Number Game</td></tr>";
        public void ToString(ref StringBuilder bd)
        {
            SetBackgrounds(ref BgSBBA, ref BgSportsbook, ref BgBA, ref BgRacing, ref BgNumberGame);
            bd.Append(s18);
            bd.Append(BgSBBA);
            bd.Append(s19);
            bd.Append(BgSportsbook);
            bd.Append(s20);
            bd.Append(BgBA);
            bd.Append(s21);
            bd.Append(BgRacing);
            bd.Append(s22);
            bd.Append(BgNumberGame);
            bd.Append(s23);
        }
    }

    public class DataBind_Template
    {
        StringBuilder bd = new StringBuilder();
        protected static string s0 = "<table class=\"grid\" cellpadding=\"0px\" cellspacing=\"0px\">";
        public string BindData = "{BindData}";
        protected static string s1 = "</table>";
        public override string ToString()
        {
            return bd.ToString();
        }
        public void Append()
        {
            bd.Append(s0);
            bd.Append(BindData);
            bd.Append(s1);
        }
    }

    public class DataBindContent
    {
        StringBuilder bd = new StringBuilder();
        protected static string s0 = "<tr><td class=\"";
        public string NoBorder = string.Empty;
        protected static string s1 = " ";
        public string WeightBoder = string.Empty;
        protected static string s2 = "\">";
        public string value;
        protected static string s3 = "</td></tr>";

        public override string ToString()
        {
            return bd.ToString();
        }

        public void Append()
        {
            bd.Append(s0);
            bd.Append(NoBorder);
            bd.Append(s1);
            bd.Append(WeightBoder);
            bd.Append(s2);
            bd.Append(value);
            bd.Append(s3);
        }
    }
}
