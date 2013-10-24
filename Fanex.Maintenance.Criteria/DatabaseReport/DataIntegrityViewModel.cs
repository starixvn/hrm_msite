// --------------------------------------------------------------------------
// <copyright file="DataIntegrityViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.12.6">Created</change>
// </history>

namespace Fanex.Maintenance.Criteria.DatabaseReport
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Data integrity view model.
    /// </summary>
    public class DataIntegrityViewModel
    {
        public int DrawFilter { get; set; }

        public string Date { get; set; }

        public DateTime GetDate()
        {
            return DateTime.ParseExact(Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
    }
}
