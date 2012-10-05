// -----------------------------------------------------------------------
// <copyright file="DataIntegrityViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Entity.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// TODO: Update summary.
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
