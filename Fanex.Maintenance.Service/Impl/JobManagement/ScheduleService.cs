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
            return schedules.Where<Schedule>((schedule) =>
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
                });
        }


        public IScheduleService WithSearchDateType(int? searchDateType)
        {
            this.SearchDateType = searchDateType != null ? (int)searchDateType : 0;
            return this;
        }

        private string CombineFreqAndFreqInterval(int freq, int freqInreval)
        {
            string combinedString = string.Empty;

            switch (freq)
            {
                case (int)FrequentTime.OneTimeOnly:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.OneTimeOnly);
                    break;
                case (int)FrequentTime.Daily:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Daily);
                    break;
                case (int)FrequentTime.Weekly:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Weekly);
                    break;
                case (int)FrequentTime.Monthly:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.Monthly);
                    break;
                case (int)FrequentTime.MonlyRelative:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.MonlyRelative);
                    break;
                case (int)FrequentTime.SqlAgentStart:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.SqlAgentStart);
                    break;
                case (int)FrequentTime.ComputerIdle:
                    combinedString += Enum.GetName(typeof(FrequentTime), FrequentTime.ComputerIdle);
                    break;
            }


            return combinedString;
        }

        private string GetFreqIntervalName(int freqIntreval)
        {
            string freqIntealName = string.Empty;
            string hexValue = freqIntreval.ToString("X");
            foreach (var charValue in hexValue)
            {
                if (charValue != '1')
                {

                }
            }
            return freqIntealName;
        }

        private string CombineFreqSubAndFreqSubInterval(int freqSub, int freqSubInreval)
        {
            string combinedString = string.Empty;


            return combinedString;
        }
    }
}
