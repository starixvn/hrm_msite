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
        public DateTime ActiveStartFromDate { get; set; }
        public DateTime ActiveStartToDate { get; set; }
        public DateTime ActiveEndFromDate { get; set; }
        public DateTime ActiveEndToDate { get; set; }
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

        public IScheduleService WithFreqType(int freqType)
        {
            this.FreqType = freqType;
            return this;
        }

        public IScheduleService WithFreqSubType(int freqSubType)
        {
            this.FreSubType = freqSubType;
            return this;
        }

        public IScheduleService WithScheduleName(string scheduleName)
        {
            this.ScheduleName = scheduleName;
            return this;
        }

        public IScheduleService WithActiveStartDateRange(DateTime fromStartDate, DateTime toStartDate)
        {
            this.ActiveStartFromDate = fromStartDate;
            this.ActiveStartToDate = toStartDate;
            return this;
        }

        public IScheduleService WithActiveEndDateRange(DateTime fromEndDate, DateTime toEndDate)
        {
            this.ActiveEndFromDate = fromEndDate;
            this.ActiveEndToDate = toEndDate;
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
            int activeStartFromDate = ActiveStartToDate != null ? int.Parse(this.ActiveStartToDate.ToString("yyyyMMdd")) : 0;
            int activeStartToDate = ActiveStartToDate != null ? int.Parse(this.ActiveStartToDate.ToString("yyyyMMdd")) : 0;
            int activeEndFromDate = ActiveEndFromDate != null ? int.Parse(this.ActiveEndFromDate.ToString("yyyyMMdd")) : 0;
            int activeEndToDate = ActiveEndToDate != null ? int.Parse(this.ActiveEndToDate.ToString("yyyyMMdd")) : 0;
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
                        case 0:
                            //// Search None
                            break;
                        case 1:
                            //// Search Active Start Date
                            if (ActiveStartFromDate == null
                                || ActiveStartToDate == null
                                || schedule.ActiveStartDate < activeStartFromDate
                                || schedule.ActiveStartDate > activeStartToDate)
                            {
                                choose = false;
                            }
                            break;
                        case 2:
                            //// Search Active End Date
                            if (ActiveEndFromDate == null
                                || ActiveEndToDate == null
                                || schedule.ActiveEndDate < activeEndFromDate
                                || schedule.ActiveEndDate > activeEndToDate)
                            {
                                choose = false;
                            }
                            break;
                        case -1:
                            //// Search Active Start Date
                            if (ActiveStartFromDate == null
                                || ActiveStartToDate == null
                                || schedule.ActiveStartDate < activeStartFromDate
                                || schedule.ActiveStartDate > activeStartToDate)
                            {
                                choose = false;
                            }
                            //// Search Active End Date
                            if (ActiveEndFromDate == null
                                || ActiveEndToDate == null
                                || schedule.ActiveEndDate < activeEndFromDate
                                || schedule.ActiveEndDate > activeEndToDate)
                            {
                                choose = false;
                            }
                            break;

                    }
                    return choose;
                });
        }
    }
}
