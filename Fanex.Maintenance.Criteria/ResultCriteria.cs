// --------------------------------------------------------------------------
// <copyright file="ResultCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class ResultCriteria : CriteriaBase
    {
        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public ResultCriteria()
        {
            ErrCode = 0;
            ErrMsg = string.Empty;
        }
    }
}
