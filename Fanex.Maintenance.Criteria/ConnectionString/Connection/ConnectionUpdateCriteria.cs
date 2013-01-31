// --------------------------------------------------------------------------
// <copyright file="ConnectionUpdateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Connection
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionUpdateCriteria : CriteriaBase
    {
        private string _initialCatalog;

        private string _userId;

        private string _freeText;

        public int ConnectionId { get; set; }

        [Required]
        [Display(Name = "Data_Source", ResourceType = typeof(ViewResource))]
        public int ServerId { get; set; }

        public int AliasId { get; set; }

        [Required]
        [Display(Name = "Initial_Catalog", ResourceType = typeof(ViewResource))]
        public string InitialCatalog
        {
            get
            {
                return _initialCatalog;
            }
            set
            {
                _initialCatalog = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        [Display(Name = "Integrated_Security", ResourceType = typeof(ViewResource))]
        public bool IntegratedSecurity { get; set; }

        [Display(Name = "User_ID", ResourceType = typeof(ViewResource))]
        public string UserID
        {
            get
            {
                return _userId;
            }
            set
            {
                _userId = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }


        public string Password { get; set; }

        [Display(Name = "Free_Text", ResourceType = typeof(ViewResource))]
        public string FreeText
        {
            get
            {
                return _freeText;
            }
            set
            {
                _freeText = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        [Display(Name = "IsDefault", ResourceType = typeof(ViewResource))]
        public bool IsDefault { get; set; }

        [Display(Name = "Primary_Connection", ResourceType = typeof(ViewResource))]
        public int? PrimaryConnectionStringId { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_ConnectionString_Update";
        }
    }
}
