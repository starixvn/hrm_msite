// -----------------------------------------------------------------------
// <copyright file="CommandResult.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommandResult : CommandResultBase
    {
        public override bool IsSuccessful
        {
            get { return Code == 0; }
        }
    }
}
