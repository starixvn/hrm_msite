// --------------------------------------------------------------------------
// <copyright file="CommandResult.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.5">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Model
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The command result class.
    /// </summary>
    public class CommandResult : CommandResultBase
    {
        public override bool IsSuccessful
        {
            get { return Code == 0; }
        }
    }
}
