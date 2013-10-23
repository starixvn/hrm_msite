// --------------------------------------------------------------------------
// <copyright file="DatabaseReportService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.16">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System;
    using System.Data;
    using System.Text;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.User;
    using Fanex.Maintenance.Repository.Impl;
    using Fanex.Maintenance.Service.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DatabaseReportService : IDatabaseReportService
    {
        private readonly DatabaseReportRepository _databaseReportRepository = new DatabaseReportRepository();

        /// <summary>
        /// Change hour to minute.
        /// </summary>
        /// <param name="dateTime">DateTime string.</param>
        /// <returns>Minute string.</returns>
        public static string ChangeHourToMinute(string dateTime)
        {
            string ngay = dateTime.Substring(0, dateTime.IndexOf(" "));
            string hour = dateTime.Substring(dateTime.IndexOf(" "), dateTime.Length - dateTime.IndexOf(" "));
            string[] arrList = hour.Split(":".ToCharArray());
            int minute = (Convert.ToInt32(arrList[0]) * 60) + Convert.ToInt32(arrList[1]);

            return minute.ToString();
        }

        /// <summary>
        /// Split date to hour.
        /// </summary>
        /// <param name="date">Date string.</param>
        /// <returns>Hour string.</returns>
        public static string SplitDateToHour(string date)
        {
            return date = date.Substring(date.Length - 5, 5);
        }

        /// <summary>
        /// Change minute to hour.
        /// </summary>
        /// <param name="minute">Minute string.</param>
        /// <returns>Hour string.</returns>
        public static string ChangeMinuteToHour(int minute)
        {
            int hour = minute / 60;
            int min = minute % 60;

            return hour.ToString() + ":" + min.ToString();
        }

        /// <summary>
        /// Get user online daily.
        /// </summary>
        /// <param name="date">Date string.</param>
        /// <returns>Data table.</returns>
        public DataTable GetUserOnlineDaily(DateTime date)
        {
            var criteria = new GetUserOnlineDailyCriteria
            {
                Date = date
            };

            return _databaseReportRepository.GetDataTable(criteria);
        }

        /// <summary>
        /// Get list user online.
        /// </summary>
        /// <param name="top">Number of users.</param>
        /// <returns>Data table.</returns>
        public DataTable GetUserOnline(int top)
        {
            var criteria = new GetUserOnlineCriteria
            {
            };

            return _databaseReportRepository.GetDataTable(criteria);
        }

        #region Generate Template

        /// <summary>
        /// Generate template user daily list.
        /// </summary>
        /// <param name="data">Template data.</param>
        /// <returns>Template string.</returns>
        public string GenerateTemplateUserDaily(DataTable data)
        {
            StringBuilder _s = new StringBuilder();

            foreach (DataRow dr in data.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + dr["timerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["localtimerange"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag"].ToString() + "</td>");
                _s.Append("<td>" + dr["sag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["mag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["ag_w"].ToString() + "</td>");
                _s.Append("<td>" + dr["acc"].ToString() + "</td>");
                _s.Append("<td>" + dr["aff"].ToString() + "</td>");
                _s.Append("<td>" + dr["affadmin"].ToString() + "</td>");
                _s.Append("</tr>");
            }

            return _s.ToString();
        }

        /// <summary>
        /// Generate template user list.
        /// </summary>
        /// <param name="data">Template data.</param>
        /// <returns>Template string.</returns>
        public string GenerateTemplateUser(DataTable data)
        {
            StringBuilder _s = new StringBuilder();

            foreach (DataRow i in data.Rows)
            {
                _s.Append("<tr class=\"row1h\">");
                _s.Append("<td>" + i["logdate"].ToString() + "</td>");
                _s.Append("<td>" + i["totalAgent"].ToString() + "</td>");
                _s.Append("<td>" + i["super"].ToString() + "</td>");
                _s.Append("<td>" + i["master"].ToString() + "</td>");
                _s.Append("<td>" + i["agent"].ToString() + "</td>");
                _s.Append("<td>" + i["accounting"].ToString() + "</td>");
                _s.Append("<td>" + i["affiliate"].ToString() + "</td>");
                _s.Append("</tr>");
            }

            return _s.ToString();
        }

        /// <summary>
        /// Generate chart.
        /// </summary>
        /// <param name="dtb">Data table.</param>
        /// <param name="show">Number of rows.</param>
        /// <returns>Result string.</returns>
        public string GenerateDataChart(DataTable dtb, int show)
        {
            string data = string.Empty;
            if (dtb.Rows.Count > 0)
            {
                if (show > dtb.Rows.Count)
                {
                    int addRow = show - dtb.Rows.Count;
                    dtb = GenerateDataTable(dtb, addRow);
                }

                DataRow[] drs = dtb.Select(null, "logdate desc");

                foreach (DataRow dr in drs)
                {
                    data += ChangeHourToMinute(dr["logdate"].ToString()) + ";" + dr["super"] + ";" + dr["master"] + ";" 
                        + dr["agent"] + ";" + dr["accounting"] + ";" + dr["affiliate"] + "^";
                }

                data = data.Substring(0, data.Length - 1);
            }

            return data;
        }

        #endregion Generate Template

        /// <summary>
        /// Generate data table.
        /// </summary>
        /// <param name="dtb">Data table.</param>
        /// <param name="rowNumber">Row number.</param>
        /// <returns>Data table.</returns>
        private static DataTable GenerateDataTable(DataTable dtb, int rowNumber)
        {
            string _datetime = dtb.Rows[0]["logdate"].ToString();

            for (int i = 0; i < rowNumber; i++)
            {
                string _date = _datetime.Substring(0, _datetime.IndexOf(" ")).Trim();
                string _hour = _datetime.Substring(_datetime.IndexOf(" "), _datetime.Length - _datetime.IndexOf(" ")).Trim();
                string[] arr = _hour.Split(':');
                DataRow dr = dtb.NewRow();

                if (Convert.ToInt32(arr[1]) == 60)
                {
                    int myhour = Convert.ToInt32(arr[0]) + AppSetting.Minute;
                    int myminute = Convert.ToInt32(arr[1]) / 60;
                    _datetime = _date + " " + (myhour < 10 ? "0" + myhour.ToString() : myhour.ToString()) + ":" + (myminute < 10 ? "0" + myminute.ToString() : myminute.ToString());
                }
                else
                {
                    int _minute = Convert.ToInt32(arr[1]) + AppSetting.Minute;

                    _datetime = _date + " " + arr[0] + ":" + (_minute < 10 ? "0" + _minute.ToString() : _minute.ToString());
                }

                dr[0] = _datetime;
                dr[1] = "-1";
                dr[2] = "-1";
                dr[3] = "-1";
                dr[4] = "-1";
                dr[5] = "-1";
                dr[6] = "-1";

                dtb.Rows.Add(dr);
            }

            return dtb;
        }
    }
}