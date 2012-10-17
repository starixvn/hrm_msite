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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseWorker
    {
        private Thread _workerThread;

        protected bool IsStop { get; set; }

        public BaseWorker()
        {
            _workerThread = new Thread(Run);
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
    }
}