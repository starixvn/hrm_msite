// -----------------------------------------------------------------------
// <copyright file="UserLoginCriteria.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Core.Criteria.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.NAP.Framework.Data;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
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
