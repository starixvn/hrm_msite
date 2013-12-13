// --------------------------------------------------------------------------
// <copyright file="SearchLogViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.12.12">Created</change>
// </history>

namespace Fanex.Maintenance.ViewModel.Connection
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;
    using Fanex.ConnectionString.Domain;
    using Fanex.ConnectionString.Model;
    using Fanex.NAP.Core.Helpers;

    /// <summary>
    /// The search log view model.
    /// </summary>
    public class SearchLogViewModel
    {
        public SearchLogViewModel()
        {
            Name = string.Empty;
            Username = string.Empty;
            From = DateTime.Now;
            To = DateTime.Now;
            PageIndex = 1;
            PageSize = 10;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd,yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd,yyyy}",
               ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }

        public int FilterType { get; set; }

        public PagerList<LogExecSp> LogDatas { get; set; }
    }
}
