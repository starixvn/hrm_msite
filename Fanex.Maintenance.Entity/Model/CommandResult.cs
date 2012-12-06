// -----------------------------------------------------------------------
// <copyright file="CommandResult.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Entity.Model
{
    using Fanex.NAP.Framework.Data;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class CommandResult : CommandResultBase
    {
        public override bool IsSuccessful
        {
            get { return Code == 0; }
        }
    }
}
