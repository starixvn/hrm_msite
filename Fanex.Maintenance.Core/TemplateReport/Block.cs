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
        /// Bg Sports book
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
        /// <param name="bgSBBA"></param>
        /// <param name="bgSportsbook"></param>
        /// <param name="bgBA"></param>
        /// <param name="bgRacing"></param>
        /// <param name="bgNumberGame"></param>
        public void SetBackgrounds(
            ref string bgSBBA,
            ref string bgSportsbook,
            ref string bgBA,
            ref string bgRacing,
            ref string bgNumberGame)
        {
            bgSBBA = this.BgSBBA;
            bgSportsbook = this.BgSportsbook;
            bgBA = this.BgBA;
            bgRacing = this.BgRacing;
            bgNumberGame = this.BgNumberGame;
        }
    }
}
