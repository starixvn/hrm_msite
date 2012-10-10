// -----------------------------------------------------------------------
// <copyright file="ScheduleService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Entity.Domain;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Core.Configurations;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Core;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ScheduleService : IScheduleService
    {
        #region Member

        private readonly IScheduleRepository _scheduleRepository;

        #endregion

        #region Properties

        public string ServerName { get; set; }
        public string ScheduleName { get; set; }
        public int FreqType { get; set; }
        public int FreSubType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int SearchDateType { get; set; }

        #endregion

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

        #endregion

        public IScheduleService WithServer(string serverName)
        {
            this.ServerName = serverName;
            return this;
        }

        public IScheduleService WithFreqType(int? freqType)
        {
            this.FreqType = freqType != null ? (int)freqType : 0;
            return this;
        }

        public IScheduleService WithFreqSubType(int? freqSubType)
        {
            this.FreSubType = freqSubType != null ? (int)freqSubType : 0;
            return this;
        }

        public IScheduleService WithScheduleName(string scheduleName)
        {
            if (!string.IsNullOrEmpty(scheduleName))
            {
                this.ScheduleName = scheduleName;
            }
            return this;
        }

        public IScheduleService WithDateRange(DateTime? fromDate, DateTime? toDate)
        {
            this.FromDate = fromDate != null ? (DateTime)fromDate : DateTime.Now;
            this.ToDate = toDate != null ? (DateTime)toDate : DateTime.Now;
            return this;
        }

        public IEnumerable<Schedule> GetSchedules()
        {
            GetAllScheduleCriteria getAllSchedule = new GetAllScheduleCriteria
            {
                Schedule_Name = ScheduleName
            };

            List<Schedule> schedules = new List<Schedule>();

            //// Get All Schedule from Database
            if (!string.IsNullOrEmpty(ServerName))
            {
                schedules.AddRange(_scheduleRepository.GetAllSchedule(getAllSchedule, ServerName));
            }
            else
            {
                foreach (ServerElement server in DatabaseServersSection.Instance.Servers)
                {
                    schedules.AddRange(_scheduleRepository.GetAllSchedule(getAllSchedule, server.ServerName));
                }
            }

            //// Search from result list
            schedules = schedules.Where<Schedule>((schedule) =>
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
                    schedule.FreqSubIntervalCombine = CombineFreqSubAndFreqSubInterval(schedule.FreqSubdayType, schedule.FreqSubdayInterval);
                });

            return schedules;
        }


        public IScheduleService WithSearchDateType(int? searchDateType)
        {
            this.SearchDateType = searchDateType != null ? (int)searchDateType : 0;
            return this;
        }

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
                freqIntervalName = " : " + freqIntervalName.Substring(0,freqIntervalName.Length-1);
            }
            return freqIntervalName;
        }

        private string CombineFreqSubAndFreqSubInterval(int freqSub, int freqSubInreval)
        {
            string combinedString = string.Empty;
            switch (freqSub)
            {
                case (int)FrequentSubTime.SpecificTime:
                    combinedString += ViewResource.FreqSubSpecificTime;
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
