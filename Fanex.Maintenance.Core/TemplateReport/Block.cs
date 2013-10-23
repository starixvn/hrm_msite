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
    /// Block.
    /// </summary>
    public class Block
    {
        /// <summary>
        /// Gets or sets background Sportsbook BA.
        /// </summary>
        /// <value>Background Sportsbook BA string.</value>
        public string BgSBBA { get; set; }

        /// <summary>
        /// Gets or sets background Sportsbook.
        /// </summary>
        /// <value>Background Sportsbook string.</value>
        public string BgSportsbook { get; set; }

        /// <summary>
        /// Gets or sets background BA.
        /// </summary>
        /// <value>Background BA string.</value>
        public string BgBA { get; set; }

        /// <summary>
        /// Gets or sets background Racing.
        /// </summary>
        /// <value>Background Racing string.</value>
        public string BgRacing { get; set; }

        /// <summary>
        /// Gets or sets background NumberGame.
        /// </summary>
        /// <value>Background Number Game string.</value>
        public string BgNumberGame { get; set; }

        /// <summary>
        /// SetBackgrounds.
        /// </summary>
        /// <param name="bgSBBA">Background Sportsbook BA.</param>
        /// <param name="bgSportsbook">Background Sportsbook.</param>
        /// <param name="bgBA">Background BA.</param>
        /// <param name="bgRacing">Background Racing.</param>
        /// <param name="bgNumberGame">Background Number Game.</param>
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
