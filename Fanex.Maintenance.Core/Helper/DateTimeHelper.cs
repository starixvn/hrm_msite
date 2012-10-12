// --------------------------------------------------------------------------
// <copyright file="DateTimeHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.12">Created</change>
// </history>


namespace Fanex.Maintenance.Core.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// Get Full time String from int
        /// </summary>
        /// <param name="timeValue">contains time value by int, example 092113 is 09:21:13</param>
        /// <returns>fulltime string</returns>
        public static string GetFullTime(int timeValue)
        {
            string fullTimeString = timeValue.ToString();
            if (fullTimeString.Length == 5)
            {
                fullTimeString = "0" + fullTimeString;
            }
            fullTimeString = fullTimeString.Length == 6 ? DateTime.ParseExact(fullTimeString,
                    "HHmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None).ToLongTimeString() : DateTime.MinValue.ToLongTimeString();
            return fullTimeString;
        }

        /// <summary>
        /// Get Full date time string from 
        /// </summary>
        /// <param name="dateTimeString">dateTime String from Database, it is </param>
        /// <returns></returns>
        public static DateTime GetFullDate(string dateString)
        {
            DateTime fullDate = dateString.Length == 8 ? DateTime.ParseExact(dateString,
                                "yyyyMMdd",
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.None) : DateTime.MinValue;

            return fullDate;
        }
    }
}
