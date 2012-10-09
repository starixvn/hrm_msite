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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Service.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.Maintenance.Repository.Impl.JobManagement;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Criteria.Job;
    using Fanex.Maintenance.Core.Web;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StepService : IStepService
    {
        #region Member

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

        public static IStepService Create()
        {
            return new StepService();
        }

        #endregion


        public IStepService FromServer(string serverName)
        {
            this._serverName = serverName;
            return this;
        }

        public IStepService WithJob(string jobName)
        {
            this._jobName = jobName;
            return this;
        }


        public IEnumerable<StepDetail> GetSteps()
        {
            return _stepRepository.GetStepByJob(this._serverName, this._jobName);
        }
    }
}
