// --------------------------------------------------------------------------
// <copyright file="LogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.9.20">Created</change>
// </history>

namespace Fanex.Maintenance.Core.Service.Impl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Core.Model;
    using Fanex.Maintenance.Core.Criteria.Log;
    using Fanex.Maintenance.Core.Repository.Impl;
    using Fanex.Maintenance.Core.Repository.Interface;

    public class LogService
    {
        #region Member

        private int _categoryId;
        private string _severity;
        private DateTime _dateFrom;
        private DateTime _dateTo;
        private int _page;
        private int _size;

        private readonly LogRepository _logRepository = new LogRepository();

        #endregion        

        #region Ctor

        private LogService()
        {
            _categoryId = 0;
            _severity = "All";
            _dateFrom = DateTime.Now;
            _dateTo = DateTime.Now.AddDays(-1);
            _page = 0;
            _size = 100;
        }

        public static LogService Create()
        {
            return new LogService();
        }

        #endregion
                
        /// <summary>
        /// By categoryId
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public LogService With(int categoryId)
        {
            this._categoryId = categoryId;
            return this;
        }

        /// <summary>
        /// By categoryId and Serverity
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="severity"></param>
        /// <returns></returns>
        public LogService With(int categoryId, string severity)
        {
            this._categoryId = categoryId;
            this._severity = severity;
            return this;
        }

        /// <summary>
        /// From date
        /// </summary>
        /// <param name="fromDate"></param>
        /// <returns></returns>
        public LogService From(DateTime fromDate)
        {
            this._dateFrom = fromDate;
            return this;
        }

        /// <summary>
        /// To date
        /// </summary>
        /// <param name="toDate"></param>
        /// <returns></returns>
        public LogService To(DateTime toDate)
        {
            this._dateTo = toDate;
            return this;
        }

        /// <summary>
        /// Pagination
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public LogService WithPagination(int page, int size)
        {
            this._page = page;
            this._size = size;
            return this;
        }

        /// <summary>
        /// Get message detail
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public string GetMessage(long logId)
        {
            string message = string.Empty;
            message = _logRepository.GetLogMessage(logId);
            return message;
        }

        /// <summary>
        /// Get list logs
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Logs> GetLogs()
        {
            GetLogCriteria criteria = new GetLogCriteria
            {
                CategoryId = this._categoryId,
                From = this._dateFrom,
                To = this._dateTo,
                Page = this._page,
                Size = this._size,
                Severity = this._severity
            };

            return _logRepository.Fetch(criteria);
        }


        public int RemoveLogs()
        {
            return _logRepository.DeleteLogs(this._dateFrom);
        }
    }
}
