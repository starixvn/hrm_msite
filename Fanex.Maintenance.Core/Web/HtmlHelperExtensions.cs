// --------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>


namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Common.Helper;    
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region DateTime
        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString ToFullDateString(this DateTime value,
            string dateFormat = "MMM dd, yyyy", string timeFormat = "HH:mm tt")
        {
            return new HtmlString(string.Format("<span class='datetime'><span class='date'>{0}</span> <span class='time'>{1}</span><span>",
                value.ToString(dateFormat), value.ToString(timeFormat)));
        }
        #endregion
    }
}
