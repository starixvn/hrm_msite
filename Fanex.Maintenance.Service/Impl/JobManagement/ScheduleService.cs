// --------------------------------------------------------------------------
// <copyright file="ScheduleService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.10">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleService : IScheduleService
    {
        #region Member

        private readonly IScheduleRepository _scheduleRepository;

        #endregion Member

        #region Properties

        public string ServerIp { get; set; }

        public string ScheduleName { get; set; }

        public int FreqType { get; set; }

        public int FreSubType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int SearchDateType { get; set; }

        #endregion Properties

        #region Ctor

        private ScheduleService()
            : this(null)
        {
        }

        private ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository ?? new ScheduleRepository();
        }

        public static IScheduleService Create()
        {
            return new ScheduleService();
        }

        #endregion Ctor

        /// <summary>
        /// Search Schedule with Server IP
        /// </summary>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public IScheduleService WithServer(string serverIp)
        {
            this.ServerIp = serverIp;
            return this;
        }

        /// <summary>
        /// Search Schedule with Freq Type
        /// </summary>
        /// <param name="freqType"></param>
        /// <returns></returns>
        public IScheduleService WithFreqType(int? freqType)
        {
            this.FreqType = freqType != null ? (int)freqType : 0;
            return this;
        }

        /// <summary>
        /// Search Schedule with Freq Sub Type
        /// </summary>
        /// <param name="freqSubType"></param>
        /// <returns></returns>
        public IScheduleService WithFreqSubType(int? freqSubType)
        {
            this.FreSubType = freqSubType != null ? (int)freqSubType : 0;
            return this;
        }

        /// <summary>
        /// Search Schedule with Schedule Name
        /// </summary>
        /// <param name="scheduleName"></param>
        /// <returns></returns>
        public IScheduleService WithScheduleName(string scheduleName)
        {
            if (!string.IsNullOrEmpty(scheduleName))
            {
                this.ScheduleName = scheduleName;
            }
            return this;
        }

        /// <summary>
        /// Search Schedule with Date Range
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public IScheduleService WithDateRange(DateTime? fromDate, DateTime? toDate)
        {
            this.FromDate = fromDate != null ? (DateTime)fromDate : DateTime.Now;
            this.ToDate = toDate != null ? (DateTime)toDate : DateTime.Now;
            return this;
        }

        /// <summary>
        /// Get Schedule with filtered
        /// </summary>
        /// <returns>List Schedule satisfy filter</returns>
        public IEnumerable<Schedule> GetSchedules()
        {
            GetAllScheduleCriteria getAllSchedule = new GetAllScheduleCriteria
            {
                Schedule_Name = ScheduleName
            };

            List<ScheduleDetail> schedules = new List<ScheduleDetail>();

            //// Get All Schedule from Database
            if (!string.IsNullOrEmpty(ServerIp))
            {
                schedules.AddRange(_scheduleRepository.GetAllSchedule(getAllSchedule, ServerIp));
            }
            else
            {
                foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                {
                    schedules.AddRange(_scheduleRepository.GetAllSchedule(getAllSchedule, server.ServerIp));
                }
            }

            //// Search from result list
            schedules = schedules.Where<ScheduleDetail>((schedule) =>
                {
                    bool choose = true;
                    if (FreqType != 0 && FreqType != schedule.FreqType)
                    {
                        choose = false;
                    }

                    if (FreSubType != 0 && FreSubType != schedule.FreqSubdayType)
                    {
                        choose = false;
                    }

                    //// Search with Date Type
                    switch (SearchDateType)
                    {
                        case (int)SearchDate.All:
                            //// Search None
                            break;
                        case (int)SearchDate.StartDate:
                            //// Search Active Start Date
                            if (FromDate == null
                                || ToDate == null
                                || schedule.ActiveStartDate < FromDate
                                || schedule.ActiveStartDate > ToDate)
                            {
                                choose = false;
                            }
                            break;
                        case (int)SearchDate.EndDate:
                            //// Search Active End Date
                            if (FromDate == null
                                || ToDate == null
                                || schedule.ActiveEndDate < FromDate
                                || schedule.ActiveEndDate > ToDate)
                            {
                                choose = false;
                            }
                            break;
                    }
                    return choose;
                }).ToList();

            //// Format Data To Show on View
            schedules.ForEach((schedule) =>
                {
                    schedule.FreqIntervalCombine = CombineFreqAndFreqInterval(schedule.FreqType, schedule.FreqInterval);
                    schedule.FreqSubIntervalCombine = CombineFreqSubAndFreqSubInterval(schedule.FreqSubdayType, schedule.FreqSubdayInterval, schedule.ActiveStartTime);
                });

            return schedules;
        }

        /// <summary>
        /// Search Schedule with Date
        /// </summary>
        /// <param name="searchDateType">Search Date Type</param>
        /// <returns></returns>
        public IScheduleService WithSearchDateType(int? searchDateType)
        {
            this.SearchDateType = searchDateType != null ? (int)searchDateType : 0;
            return this;
        }

        /// <summary>
        /// Combine Freq and Interver to a string
        /// </summary>
        /// <param name="freq">Freq Value</param>
        /// <param name="freqInterval">Freq Interval Value</param>
        /// <returns>String combined</returns>
        private string CombineFreqAndFreqInterval(int freq, int freqInterval)
        {
            string combinedString = string.Empty;

            switch (freq)
            {
                case (int)FrequentTime.OneTimeOnly:
                    combinedString += ViewResource.FreqOneTime;
                    break;
                case (int)FrequentTime.Daily:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Daily);
                    break;
                case (int)FrequentTime.Weekly:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Weekly);
                    combinedString += GetFreqIntervalValue(freq, freqInterval);
                    break;
                case (int)FrequentTime.Monthly:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Monthly);
                    break;
                case (int)FrequentTime.MonlyRelative:
                    combinedString += ViewResource.FreqMonthlyRelative;
                    combinedString += GetFreqIntervalValue(freq, freqInterval);
                    break;
                case (int)FrequentTime.SqlAgentStart:
                    combinedString += ViewResource.FreqAgentStart;
                    break;
                case (int)FrequentTime.ComputerIdle:
                    combinedString += ViewResource.FreqComputerIdle;
                    break;
            }
            return combinedString;
        }

        /// <summary>
        /// Get Freq Interval string
        /// </summary>
        /// <param name="freq">Freq Value</param>
        /// <param name="freqInterval">Freq Interval Value</param>
        /// <returns>String combind Schedule Freq</returns>
        private string GetFreqIntervalValue(int freq, int freqInterval)
        {
            string freqIntervalName = string.Empty;
            int index = 0;
            switch (freq)
            {
                case (int)FrequentTime.Weekly:
                    string hexValue = Convert.ToString(freqInterval, 2);
                    foreach (var charValue in hexValue)
                    {
                        if (charValue == '1')
                        {
                            freqIntervalName += Constants.FreqIntervalValues[index] + ",";
                        }
                        index++;
                    }
                    break;
                case (int)FrequentTime.MonlyRelative:
                    freqIntervalName = Constants.FreqIntervalValues[freqInterval];
                    break;
            }

            if (freqIntervalName != string.Empty)
            {
                freqIntervalName = " : " + freqIntervalName.Substring(0, freqIntervalName.Length - 1);
            }
            return freqIntervalName;
        }

        /// <summary>
        /// Combine FreqSub and Interval To String for showing in View
        /// </summary>
        /// <param name="freqSub">Freq Sub Value</param>
        /// <param name="freqSubInreval">Freq Sub Interval</param>
        /// <param name="activeStartTime">Active start</param>
        /// <returns>Combined Freq Name</returns>
        private string CombineFreqSubAndFreqSubInterval(int freqSub, int freqSubInreval, int activeStartTime)
        {
            string combinedString = string.Empty;
            switch (freqSub)
            {
                case (int)FrequentSubTime.SpecificTime:
                    combinedString += ViewResource.FreqSubSpecificTime + " : ";
                    combinedString += activeStartTime.ToString().Length == 6 ? DateTime.ParseExact(activeStartTime.ToString(), "HHmmss"
                                            , CultureInfo.InvariantCulture
                                            , DateTimeStyles.None).ToLongTimeString() : DateTime.MinValue.ToLongTimeString();
                    break;
                case (int)FrequentSubTime.Seconds:
                    combinedString += Enum.GetName(typeof(FrequentSubTime), FrequentSubTime.Seconds);
                    break;
                case (int)FrequentSubTime.Minutes:
                    combinedString += Enum.GetName(typeof(FrequentSubTime), FrequentSubTime.Minutes);
                    break;
                case (int)FrequentSubTime.Hours:
                    combinedString += Enum.GetName(typeof(FrequentSubTime), FrequentSubTime.Hours);
                    break;
            }
            return combinedString;
        }
    }
}