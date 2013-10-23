// --------------------------------------------------------------------------
// <copyright file="StepService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.10.9">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.JobManagement
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Service.Interface.JobManagement;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StepService : IStepService
    {
        #region Member

        /// <summary>
        /// Step repository.
        /// </summary>
        private readonly IStepRepository _stepRepository = new StepRepository();

        #endregion

        #region Properties

        private string _serverName;

        private string _jobName;

        #endregion

        #region Ctor

        private StepService()
            : this(null)
        {
        }

        private StepService(IStepRepository stepRepository)
        {
            _stepRepository = stepRepository ?? new StepRepository();

            this._serverName = string.Empty;
            this._jobName = string.Empty;
        }

        /// <summary>
        /// Create step service.
        /// </summary>
        /// <returns>Step service.</returns>
        public static IStepService Create()
        {
            return new StepService();
        }

        #endregion

        /// <summary>
        /// Get from server.
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <returns>Step service.</returns>
        public IStepService FromServer(string serverName)
        {
            this._serverName = serverName;

            return this;
        }

        /// <summary>
        /// Filter by job name.
        /// </summary>
        /// <param name="jobName">Job name.</param>
        /// <returns>Step service.</returns>
        public IStepService WithJob(string jobName)
        {
            this._jobName = jobName;

            return this;
        }

        /// <summary>
        /// Get step.
        /// </summary>
        /// <returns>List of step detail.</returns>
        public IEnumerable<StepDetail> GetSteps()
        {
            var criteria = new GetListJobCriteria
            {
                JobName = this._jobName,
                JobAspect = Constants.STEPS
            };

            return _stepRepository.GetStepByJob(criteria, this._serverName);
        }
    }
}
