// --------------------------------------------------------------------------
// <copyright file="LogService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.28">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl.ConnectionString
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Core;
    using Fanex.Maintenance.Criteria.ConnectionString.LogExecSp;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Impl.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.Maintenance.Service.Interface.ConnectionString;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class LogService : BaseService, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService()
            : this(null)
        {
        }

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository ?? new LogRepository();
        }

        /// <summary>
        /// Search execute SP log.
        /// </summary>
        /// <param name="pageIndex">Page index.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="totalRows">Number of rows.</param>
        /// <param name="name">Log name filter.</param>
        /// <param name="username">Username filter.</param>
        /// <param name="from">From DateTime.</param>
        /// <param name="to">To DateTime.</param>
        /// <param name="filterBy">Filter options.</param>
        /// <returns>List of execute SP log.</returns>
        public IQueryable<LogExecSp> SearchExecSp(
            int pageIndex, 
            int pageSize, 
            out int totalRows, 
            string name, 
            string username, 
            DateTime from, 
            DateTime to, 
            int filterBy = 0)
        {
            IQueryable<LogExecSp> logList = null;
            totalRows = 0;

            try
            {
                var criteria = new LogExecSpSearchCriteria
                {
                    Username = username,
                    Name = name,
                    From = from,
                    To = to,
                    FilterType = filterBy,
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };

                if (criteria.IsValid())
                {
                    logList = _logRepository.Fetch(criteria, out totalRows).AsQueryable();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return logList;
        }

        /// <summary>
        /// Get log by ID.
        /// </summary>
        /// <param name="logId">Execute SP log ID.</param>
        /// <returns>Execute SP log.</returns>
        public LogExecSp GetById(int logId)
        {
            try
            {
                var criteria = new LogGetByIdCriteria
                {
                    LogId = logId
                };

                if (criteria.IsValid())
                {
                    var logExec = _logRepository.GetById(criteria);

                    return logExec;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }

            return null;
        }

        /// <summary>
        /// Get value alias insert.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <param name="listSite">List site.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueAliasInsert(LogExecSp logExecSp, ref List<LogData> listSite)
        {
            var listLogData = new List<LogData>();

            string data = logExecSp.Data;
            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value
                });
                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value
                });
                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value)
                });

                string strSite = DecodeXMLValue(xelement.Attribute("Sites").Value);
                var documentSite = XDocument.Parse(strSite);
                var elementSite = documentSite.Descendants("Site");
                foreach (var itemSite in elementSite)
                {
                    listSite.Add(new LogData
                    {
                        FieldName = ViewResource.Name,
                        NewValue = itemSite.Attribute("SiteName").Value
                    });
                }
            }

            return listLogData;
        }

        /// <summary>
        /// Get update alias value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <param name="listSite">List of site log data.</param>
        /// <param name="listOldSite">List of site old log data.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueAliasUpdate(
            LogExecSp logExecSp, 
            ref List<LogData> listSite, 
            ref List<LogData> listOldSite)
        {
            var listLogData = new List<LogData>();

            string data = logExecSp.Data;
            string oldData = logExecSp.OldData;

            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                var oldXelement = XElement.Parse(oldData);

                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value,
                    OldValue = oldXelement.Attribute("Name").Value
                });
                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value,
                    OldValue = oldXelement.Attribute("Description").Value
                });
                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value),
                    OldValue = IsActiveToString(oldXelement.Attribute("IsActive").Value)
                });

                string strSite = DecodeXMLValue(xelement.Attribute("Sites").Value);
                var documentSite = XDocument.Parse(strSite);
                var elementSite = documentSite.Descendants("Site");

                foreach (var itemSite in elementSite)
                {
                    listSite.Add(new LogData
                    {
                        FieldName = ViewResource.Name,
                        NewValue = itemSite.Attribute("SiteName").Value
                    });
                }

                string strOldSite = DecodeXMLValue(oldXelement.Attribute("Sites").Value);
                var documentOldSite = XDocument.Parse(strOldSite);
                var elementOldSite = documentOldSite.Descendants("Site");

                foreach (var itemSite in elementOldSite)
                {
                    listOldSite.Add(new LogData
                    {
                        FieldName = ViewResource.Name,
                        NewValue = itemSite.Attribute("SiteName").Value
                    });
                }
            }

            return listLogData;
        }

        /// <summary>
        /// Get update status value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueUpdateStatus(LogExecSp logExecSp)
        {
            var listLogData = new List<LogData>();

            string data = logExecSp.Data;
            string oldData = logExecSp.OldData;

            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                var oldXelement = XElement.Parse(oldData);

                listLogData.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value),
                    OldValue = IsActiveToString(oldXelement.Attribute("IsActive").Value)
                });
            }

            return listLogData;
        }

        /// <summary>
        /// Get insert connection value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueConnectionInsert(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);

                string strSite = DecodeXMLValue(xelement.Attribute("ConnectionString").Value);
                var documentConnection = XDocument.Parse(strSite);
                var xmlConnection = documentConnection.Descendants("ConnectionString");

                foreach (var elementConnection in xmlConnection)
                {
                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Server_IP,
                        NewValue = elementConnection.Attribute("ServerIP").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Initial_Catalog,
                        NewValue = elementConnection.Attribute("InitialCatalog").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Integrated_Security,
                        NewValue = IsActiveToString(elementConnection.Attribute("IntegratedSecurity").Value)
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.User_ID,
                        NewValue = elementConnection.Attribute("UserId").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Password,
                        NewValue = elementConnection.Attribute("Password").Value
                    });
                }
            }

            return listLog;
        }

        /// <summary>
        /// Get update connection value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueConnectionUpdate(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            string oldData = logExecSp.OldData;

            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                var oldXelement = XElement.Parse(oldData);

                string strSite = DecodeXMLValue(xelement.Attribute("ConnectionString").Value);
                string strOldSite = DecodeXMLValue(oldXelement.Attribute("ConnectionString").Value);

                var documentConnection = XDocument.Parse(strSite);
                var documentOldConnection = XDocument.Parse(strOldSite);

                var xmlConnection = documentConnection.Descendants("ConnectionString");
                var xmlOldConnection = documentOldConnection.Descendants("ConnectionString");

                var elementOldConnection = xmlOldConnection.FirstOrDefault();

                foreach (var elementConnection in xmlConnection)
                {
                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Server_IP,
                        NewValue = elementConnection.Attribute("ServerIP").Value,
                        OldValue = elementOldConnection.Attribute("ServerIP").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Initial_Catalog,
                        NewValue = elementConnection.Attribute("InitialCatalog").Value,
                        OldValue = elementOldConnection.Attribute("InitialCatalog").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Integrated_Security,
                        NewValue = IsActiveToString(elementConnection.Attribute("IntegratedSecurity").Value),
                        OldValue = IsActiveToString(elementOldConnection.Attribute("IntegratedSecurity").Value)
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.User_ID,
                        NewValue = elementConnection.Attribute("UserId").Value,
                        OldValue = elementOldConnection.Attribute("UserId").Value
                    });

                    listLog.Add(new LogData
                    {
                        FieldName = ViewResource.Password,
                        NewValue = elementConnection.Attribute("Password").Value,
                        OldValue = elementOldConnection.Attribute("Password").Value
                    });
                }
            }

            return listLog;
        }

        /// <summary>
        /// Get insert database server value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueDatabaseServerInsert(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Server_IP,
                    NewValue = xelement.Attribute("ServerIP").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value)
                });
            }

            return listLog;
        }

        /// <summary>
        /// Get update database server value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueDatabaseServerUpdate(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            string oldData = logExecSp.OldData;

            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                var oldXelement = XElement.Parse(oldData);

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value,
                    OldValue = oldXelement.Attribute("Name").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Server_IP,
                    NewValue = xelement.Attribute("ServerIP").Value,
                    OldValue = oldXelement.Attribute("ServerIP").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value,
                    OldValue = oldXelement.Attribute("Description").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value),
                    OldValue = IsActiveToString(oldXelement.Attribute("IsActive").Value)
                });
            }

            return listLog;
        }

        /// <summary>
        /// Get insert site value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueSiteInsert(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value)
                });
            }

            return listLog;
        }

        /// <summary>
        /// Get update site value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <returns>List of log data.</returns>
        public List<LogData> GetValueSiteUpdate(LogExecSp logExecSp)
        {
            var listLog = new List<LogData>();

            string data = logExecSp.Data;
            string oldData = logExecSp.OldData;

            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);
                var oldXelement = XElement.Parse(oldData);

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Name,
                    NewValue = xelement.Attribute("Name").Value,
                    OldValue = oldXelement.Attribute("Name").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.Description,
                    NewValue = xelement.Attribute("Description").Value,
                    OldValue = oldXelement.Attribute("Description").Value
                });

                listLog.Add(new LogData
                {
                    FieldName = ViewResource.IsActive,
                    NewValue = IsActiveToString(xelement.Attribute("IsActive").Value),
                    OldValue = IsActiveToString(oldXelement.Attribute("IsActive").Value)
                });
            }

            return listLog;
        }

        /// <summary>
        /// Get change connection value.
        /// </summary>
        /// <param name="logExecSp">Execute SP log.</param>
        /// <param name="listOld">Old list of connection string.</param>
        /// <returns>List of connection string.</returns>
        public List<ConnectionString> GetValueChangeConnection(LogExecSp logExecSp, ref List<ConnectionString> listOld)
        {
            var listConnection = new List<ConnectionString>();

            string data = logExecSp.Data;
            if (!string.IsNullOrEmpty(data))
            {
                var xelement = XElement.Parse(data);

                string strValue = DecodeXMLValue(xelement.Attribute("Value").Value);
                var documentConnection = XDocument.Parse(strValue);
                var xmlAliasConnection = documentConnection.Descendants("AliasConnection");

                foreach (var elementConnection in xmlAliasConnection)
                {
                    var tempConnection = new ConnectionString
                    {
                        Alias = new Alias
                        {
                            Id = Convert.ToInt32(elementConnection.Attribute("AliasId").Value),
                            Name = elementConnection.Attribute("Name").Value
                        },
                        Server = new DatabaseServer
                        {
                            ServerIP = elementConnection.Attribute("ServerIP").Value
                        },
                        InitialCatalog = elementConnection.Attribute("InitialCatalog").Value,
                        IntegratedSecurity = elementConnection.Attribute("IntegratedSecurity").Value == "1" ? true : false,
                        UserID = elementConnection.Attribute("UserId").Value,
                        Password = elementConnection.Attribute("Password").Value
                    };

                    listConnection.Add(tempConnection);
                }
            }

            string dataOld = logExecSp.OldData;
            if (!string.IsNullOrEmpty(dataOld))
            {
                var xelement = XElement.Parse(dataOld);

                string strValue = DecodeXMLValue(xelement.Attribute("Value").Value);
                var documentConnection = XDocument.Parse(strValue);
                var xmlAliasConnection = documentConnection.Descendants("AliasConnection");

                foreach (var elementConnection in xmlAliasConnection)
                {
                    var tempConnection = new ConnectionString
                    {
                        Alias = new Alias
                        {
                            Id = Convert.ToInt32(elementConnection.Attribute("AliasId").Value),
                            Name = elementConnection.Attribute("Name").Value
                        },
                        Server = new DatabaseServer
                        {
                            ServerIP = elementConnection.Attribute("ServerIP").Value
                        },
                        InitialCatalog = elementConnection.Attribute("InitialCatalog").Value,
                        IntegratedSecurity = elementConnection.Attribute("IntegratedSecurity").Value == "1" ? true : false,
                        UserID = elementConnection.Attribute("UserId").Value,
                        Password = elementConnection.Attribute("Password").Value
                    };

                    listOld.Add(tempConnection);
                }
            }

            return listConnection;
        }

        /// <summary>
        /// Decode XML value.
        /// </summary>
        /// <param name="value">Encoded XML value.</param>
        /// <returns>XML value.</returns>
        public string DecodeXMLValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Replace("&amp;", "&");
                value = value.Replace("&lt;", "<");
                value = value.Replace("&gt;", ">");
                value = value.Replace("&quot;", "\"");
                value = value.Replace("&apos;", "''");

                return value;
            }

            return value;
        }

        public string IsActiveToString(string value)
        {
            return value == "1" ? ViewResource.Yes : ViewResource.No;
        }
    }
}