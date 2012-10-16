// --------------------------------------------------------------------------
// <copyright file="Block.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.16">StyleCop</change>
// </history>

namespace Fanex.Maintenance.Core.TemplateReport
{
    /// <summary>
    /// Block
    /// </summary>
    public class Block
    {
        /// <summary>
        /// BgSBBA
        /// </summary>
        public string BgSBBA { get; set; }
        /// <summary>
        /// BgSportsbook
        /// </summary>
        public string BgSportsbook { get; set; }
        /// <summary>
        /// BgBA
        /// </summary>
        public string BgBA { get; set; }
        /// <summary>
        /// BgRacing
        /// </summary>
        public string BgRacing { get; set; }
        /// <summary>
        /// BgNumberGame
        /// </summary>
        public string BgNumberGame { get; set; }

        /// <summary>
        /// SetBackgrounds
        /// </summary>
        /// <param name="BgSBBA"></param>
        /// <param name="BgSportsbook"></param>
        /// <param name="BgBA"></param>
        /// <param name="BgRacing"></param>
        /// <param name="BgNumberGame"></param>
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
