// --------------------------------------------------------------------------
// <copyright file="DateTimeHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.12">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Helper
{
    using System;
    using System.Globalization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DateTimeHelper
    {
        /// <summary>
        /// Get Full time String from integer
        /// </summary>
        /// <param name="timeValue">contains time value by integer, example 092113 is 09:21:13</param>
        /// <returns>fulltime string</returns>
        public static string GetFullTime(int timeValue)
        {
            string strTime = timeValue.ToString();

            if (strTime.Length < 6)
            {
                int count = 6 - strTime.Length;
                for (int i = 0; i < count; i++)
                {
                    strTime = strTime.Insert(0, "0");
                }
            }

            strTime = DateTime.ParseExact(
                strTime,
                "HHmmss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None).ToString("HH:mm:ss tt");

            return strTime;
        }

        /// <summary>
        /// Get Full date time string from
        /// </summary>
        /// <param name="dateTimeString">dateTime String from Database, it is </param>
        /// <returns></returns>
        public static DateTime GetFullDate(string dateString)
        {
            DateTime fullDate = dateString.Length == 8 ? DateTime.ParseExact(
                dateString,
                "yyyyMMdd",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None) : DateTime.MinValue;

            return fullDate;
        }

        public static DateTime GetFullDatetime(int dateNumber = 0, int timeNumber = 0)
        {
            if (dateNumber == 0 && timeNumber == 0)
            {
                return DateTime.Now;
            }
            else
            {
                //// check format time correct HHmmss
                string strTime = timeNumber.ToString();
                if (strTime.Length < 6)
                {
                    int count = 6 - strTime.Length;
                    for (int i = 0; i < count; i++)
                    {
                        strTime = strTime.Insert(0, "0");
                    }
                }

                DateTime fullDatetime = DateTime.ParseExact(
                    dateNumber.ToString() + " " + strTime,
                    "yyyyMMdd HHmmss",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);

                return fullDatetime;
            }
        }
    }
}