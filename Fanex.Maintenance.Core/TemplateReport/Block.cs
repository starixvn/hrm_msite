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
    /// Template block.
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
        /// Set backgrounds.
        /// </summary>
        /// <param name="backgroundSBBA">Background Sportsbook BA.</param>
        /// <param name="backgroundSportsbook">Background Sportsbook.</param>
        /// <param name="backgroundBA">Background BA.</param>
        /// <param name="backgroundRacing">Background Racing.</param>
        /// <param name="backgroundNumberGame">Background Number Game.</param>
        public void SetBackgrounds(
            ref string backgroundSBBA,
            ref string backgroundSportsbook,
            ref string backgroundBA,
            ref string backgroundRacing,
            ref string backgroundNumberGame)
        {
            backgroundSBBA = this.BgSBBA;
            backgroundSportsbook = this.BgSportsbook;
            backgroundBA = this.BgBA;
            backgroundRacing = this.BgRacing;
            backgroundNumberGame = this.BgNumberGame;
        }
    }
}
