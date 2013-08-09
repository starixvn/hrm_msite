// --------------------------------------------------------------------------
// <copyright file="Machine.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    using System.Diagnostics.CodeAnalysis;
    using System.ComponentModel.DataAnnotations;

    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class Machine
    {
        public int MachineId { get; set; }

        [Required]
        public string MachineName { get; set; }

        public bool IsProduction { get; set; }

        [Range(-12, 12)]
        public short TimeZone { get; set; }
    }
}