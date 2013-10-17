// --------------------------------------------------------------------------
// <copyright file="ConnectionChangeConnectionFromDbServerCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.22">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.ConnectionString.Connection
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionChangeConnectionFromDbServerCriteria : CriteriaBase
    {
        private readonly string _formatXml = "<AliasConnection AliasId='{0}' ConnectionId='{1}'/>";

        public int Modifier { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Value
        {
            get
            {
                string[] arrayAliasConnection = AliasConnection.Split(';');
                StringBuilder strXml = new StringBuilder("<ROOT>");

                foreach (var itemAliasConnection in arrayAliasConnection)
                {
                    if (!string.IsNullOrWhiteSpace(itemAliasConnection))
                    {
                        string[] item = itemAliasConnection.Split(',');
                        var aliasId = item[0];
                        var connectionId = item[1];
                        strXml.Append(string.Format(_formatXml, aliasId, connectionId));
                    }
                }

                strXml.Append("</ROOT>");

                return strXml.ToString();
            }

            set
            {
                Value = value;
            }
        }

        [SpParam(Ignored = true)]
        public string AliasConnection { get; set; }

        public override bool IsValid()
        {
            return true;
        }

        public override string GetSettingKey()
        {
            return "ConnectionString_ConnectionString_ChangeConnectionFromDbServer";
        }
    }
}
