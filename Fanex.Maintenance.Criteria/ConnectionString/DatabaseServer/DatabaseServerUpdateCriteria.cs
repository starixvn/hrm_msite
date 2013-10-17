// --------------------------------------------------------------------------
// <copyright file="DatabaseServerUpdateCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.21">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.DatabaseServer
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Fanex.Maintenance.Core;
    using Fanex.NAP.Framework.Data;

    public class DatabaseServerUpdateCriteria : CriteriaBase
    {
        private string _name;

        private string _serverIp;

        private string _description;

        public int DbServerId { get; set; }

        [Required]
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        [Required]
        [Display(Name = "Server_IP", ResourceType = typeof(ViewResource))]
        public string ServerIp
        {
            get
            {
                return _serverIp;
            }

            set
            {
                _serverIp = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = !string.IsNullOrWhiteSpace(value) ? value.Trim() : string.Empty;
            }
        }

        public bool IsActive { get; set; }

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_DatabaseServer_Update";
        }
    }
}
