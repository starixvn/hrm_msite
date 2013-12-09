using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Fanex.Maintenance.Repository.Criteria.DatabaseReport
{
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
