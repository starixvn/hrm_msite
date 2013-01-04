// --------------------------------------------------------------------------
// <copyright file="BaseWorker.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.17">Created</change>
// </history>

namespace Fanex.Maintenance.Controller.Worker
{
    using System.Threading;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class BaseWorker
    {
        private Thread _workerThread;
        private IMaintainanceService _maintenceService;

        protected bool IsStop { get; set; }

        public BaseWorker()
        {
            _workerThread = new Thread(Run);
            _maintenceService = new MaintainanceService();
        }

        protected virtual void Run()
        {
        }

        public void Start()
        {
            if (_workerThread != null)
            {
                _workerThread.Start();
                IsStop = false;
            }
            else
            {
                //// Log
            }
        }

        public void Stop()
        {
            IsStop = true;
        }

        public bool IsDownTime
        {
            get
            {
                var downtimeInfo = _maintenceService.GetDowntimeInfo();

                return downtimeInfo.IsDowntime;
            }
        }
    }
}