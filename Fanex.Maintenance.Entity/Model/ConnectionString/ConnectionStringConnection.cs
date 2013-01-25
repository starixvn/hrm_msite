// --------------------------------------------------------------------------
// <copyright file="ConnectionStringConnection.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model.ConnectionString
{
    using System.Diagnostics.CodeAnalysis;
    using Fanex.ConnectionString.Domain;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ConnectionStringConnection
    {
        public int NumOfConection { get; set; }        

        public ConnectionString ConnectionString { get; set; }      
    }
}
