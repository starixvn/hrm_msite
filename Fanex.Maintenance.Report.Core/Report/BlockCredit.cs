using System.Text;

namespace Fanex.Maintenance.Report.Core.Report
{
    public class BlockCredit
    {
        public string BgSportsbook { get; set; }
        public string BgRacing { get; set; }
        public string BgNumberGame { get; set; }

        public string SBBetting { get; set; }
        public string SBDW { get; set; }

        public string RacingBetting { get; set; }
        public string RacingDW { get; set; }

        public string NGBetting { get; set; }
        public string NGDW { get; set; }

        protected static string s2 = "<tr><td rowspan=\"3\" class=\"b firstCol weightBorderBottom\">Turnover</td><td class=\"l ";
        protected static string s2CustomerWL = "<tr><td rowspan=\"3\" class=\"b firstCol weightBorderBottom\">Customer Win Loss</td><td class=\"l ";
        protected static string s2BetCount = "<tr><td rowspan=\"3\" class=\"b firstCol\">Bet Count</td><td class=\"l ";
        protected static string s3 = "\" >Sportsbook</td><td class=\"r\">";
        protected static string s4 = "</td><td class=\"r\">";
        protected static string s5 = "</td></tr><tr><td class=\"l ";
        protected static string s6 = "\">Racing</td><td class=\"r\">";
        protected static string s7 = "</td><td class=\"r\">";
        protected static string s8 = "</td></tr><tr><td class=\"l weightBorderBottom ";
        protected static string s8BetCount = "</td></tr><tr><td class=\"l ";
        protected static string s9 = "\">Number Game</td><td class=\"r weightBorderBottom\">";
        protected static string s10 = "</td><td class=\"r weightBorderBottom\">";

        protected static string s9BetCount = "\">Number Game</td><td class=\"r\">";
        protected static string s10BetCount = "</td><td class=\"r\">";

        protected static string s11 = "</td></tr>";
        public virtual void ToString(ref StringBuilder bd, string typeBlock)
        {
            if (typeBlock == "Turnover")
            {
                bd.Append(s2);
            }
            else if (typeBlock == "CustomerWL")
            {
                bd.Append(s2CustomerWL);
            }
            else if (typeBlock == "BetCount")
            {
                bd.Append(s2BetCount);
            }
            bd.Append(BgSportsbook);
            bd.Append(s3);
            bd.Append(SBBetting);
            bd.Append(s4);
            bd.Append(SBDW);
            bd.Append(s5);
            bd.Append(BgRacing);
            bd.Append(s6);
            bd.Append(RacingBetting);
            bd.Append(s7);
            bd.Append(RacingDW);
            if (typeBlock == "BetCount")
            {
                bd.Append(s8BetCount);
            }
            else
            {
                bd.Append(s8);
            }
            bd.Append(BgNumberGame);
            if (typeBlock == "BetCount")
            {
                bd.Append(s9BetCount);
            }
            else
            {
                bd.Append(s9);
            }
            bd.Append(NGBetting);
            if (typeBlock == "BetCount")
            {
                bd.Append(s10BetCount);
            }
            else
            {
                bd.Append(s10);
            }
            bd.Append(NGDW);
            bd.Append(s11);
        }
    }
}
