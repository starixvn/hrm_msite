// --------------------------------------------------------------------------
// <copyright file="UserLoginCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.User
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class UserLoginCriteria : CriteriaBase
    {
        [Required]       
        public string Username { get; set; }

        [Required]
        public string UserPwd { get; set; }

        public string LoginIp { get; set; }

        public string From { get; set; }

        public int CustId { get; set; }

        public string PageAccess { get; set; }

        public int RoleId { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "Acc_LoginAdmin";
        }
    }
}
