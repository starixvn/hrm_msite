// --------------------------------------------------------------------------
// <copyright file="HtmlHelperExtensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
//   <change who="Ricky.Nguyen" date="2012.10.16">Stylecop</change>
// </history>

namespace Fanex.Maintenance.Core.Web
{
    using System;
    using System.Globalization;
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
            string dateFormat = "MMM dd, yyyy", 
            string timeFormat = "HH:mm tt")
        {
            return new HtmlString(string.Format("<span class='datetime'><span class='date'>{0}</span> <span class='time'>{1}</span></span>",
                value.ToString(dateFormat), 
                value.ToString(timeFormat)));
        }

        /// <summary>
        /// Convert the Date value and combine with time(int) value to specific format for showing on wiew
        /// </summary>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <param name="dateFormat"></param>
        /// <param name="timeFormat"></param>
        /// <returns></returns>
        public static IHtmlString ToFullDateWithTimeAdded(this DateTime value,
            int time = 0,
            string dateFormat = "MMM dd, yyyy",
            string timeFormat = "HH:mm tt")
        {
            //// check format time correct HHmmss
            string strTime = time.ToString();
            if (strTime.Length < 6)
            {
                int count = 6 - strTime.Length;
                for (int i = 0; i < count; i++)
                {
                    strTime = strTime.Insert(0, "0");
                }
            }

            DateTime date = DateTime.ParseExact(strTime, 
                "HHmmss", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None);           
           
            return new HtmlString(string.Format("<span class='datetime'><span class='date'>{0}</span> <span class='time'>{1}</span></span>",
                value.ToString(dateFormat), 
                date.ToString(timeFormat)));
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString ToDateString(this DateTime value,
            string dateFormat = "MMM dd, yyyy")
        {
            return new HtmlString(string.Format("<span class='datetime'><span class='date'>{0}</span>",
                value.ToString(dateFormat)));
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent string
        /// representation using the specified format.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString ToFullDateString(this int value, 
            int time,
            string dateFormat = "MMM dd, yyyy", 
            string timeFormat = "HH:mm:ss tt")
        {
            //// check format time correct HHmmss
            string strTime = time.ToString();
            if (strTime.Length < 6)
            {
                int count = 6 - strTime.Length;
                for (int i = 0; i < count; i++)
                {
                    strTime = strTime.Insert(0, "0");
                }
            }

            DateTime date = DateTime.ParseExact(value.ToString() + " " + strTime,
                "yyyyMMdd HHmmss",
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None);

            return new HtmlString(string.Format("<span class='datetime'><span class='date'>{0}</span> <span class='time'>{1}</span></span>",
                date.ToString(dateFormat), 
                date.ToString(timeFormat)));
        }

        #endregion DateTime

        #region Execution Status

        /// <summary>
        /// Paste Execute Type to Html String
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IHtmlString ToExecutionString(this int value)
        {
            string htmlValue = string.Empty;

            switch (value)
            {
                case (int)ExecutionStatus.BetweenRtries:
                    htmlValue = string.Format("<span class='BetweenRtries'>{0}</span>", "BetweenRtries");
                    break;
                case (int)ExecutionStatus.Executing:
                    htmlValue = string.Format("<span class='Executing'>{0}</span>", "Executing");
                    break;
                case (int)ExecutionStatus.Idle:
                    htmlValue = string.Format("<span class='Idle'>{0}</span>", "Idle");
                    break;
                case (int)ExecutionStatus.Obsolete:
                    htmlValue = string.Format("<span class='Obsolete'>{0}</span>", "Obsolete");
                    break;
                case (int)ExecutionStatus.PerformingCompletionActions:
                    htmlValue = string.Format("<span class='PerformingCompletionActions'>{0}</span>", "Performing Completion Actions");
                    break;
                case (int)ExecutionStatus.Suspended:
                    htmlValue = string.Format("<span class='Suspended'>{0}</span>", "Suspended");
                    break;
                case (int)ExecutionStatus.WaitingFoThread:
                    htmlValue = string.Format("<span class='WaitingFoThread'>{0}</span>", "Waiting For Thread");
                    break;
                default:
                    htmlValue = value.ToString();
                    break;
            }

            return new HtmlString(htmlValue);
        }

        #endregion Execution Status
    }
}