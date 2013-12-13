// --------------------------------------------------------------------------
// <copyright file="DatabaseServerViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.12.10">Created</change>
// </history>

namespace Fanex.Maintenance.ViewModel.Connection
{
    using System.ComponentModel.DataAnnotations;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// The database server view model.
    /// </summary>
    public class DatabaseServerViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5,
           ErrorMessageResourceName = "InputLengthValidationMsg",
           ErrorMessageResourceType = typeof(ViewResources))]
        public string Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10,
           ErrorMessageResourceName = "InputLengthValidationMsg",
           ErrorMessageResourceType = typeof(ViewResources))]
        public string ServerIP { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}
