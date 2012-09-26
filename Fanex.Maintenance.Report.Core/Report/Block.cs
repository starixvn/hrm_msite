namespace Fanex.Maintenance.Report.Core.Report
{
    public class Block
    {
        public string BgSBBA { get; set; }
        public string BgSportsbook { get; set; }
        public string BgBA { get; set; }
        public string BgRacing { get; set; }
        public string BgNumberGame { get; set; }

        public void SetBackgrounds(ref string BgSBBA, ref string BgSportsbook,
                                   ref string BgBA, ref string BgRacing, ref string BgNumberGame)
        {
            BgSBBA = this.BgSBBA;
            BgSportsbook = this.BgSportsbook;
            BgBA = this.BgBA;
            BgRacing = this.BgRacing;
            BgNumberGame = this.BgNumberGame;
        }
    }
}
