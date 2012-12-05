// --------------------------------------------------------------------------
// <copyright file="ServerRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.24">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.JobManagement
{
    using Fanex.Data;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;
    using Fanex.Common.Extensions;
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServerRepository : BaseRepository<ResourceInfo>, IServerRepository
    {
        /// <summary>
        /// Get Resource information of database
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public ResourceInfo GetServerResourceInfo(ICriteria criteria, string connectionString)
        {
            ResourceInfo resourceInfo = new ResourceInfo
            {
                SqlCpuPercent = 0,
                SystemCpuPercent = 0,
                MemoryUsage = 0,
                TotalMemory = 0,
                EventTime = DateTime.Now
            };

            DbSetting dbSetting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(dbSetting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        resourceInfo.MemoryUsage = (float)Math.Round((reader.ToFloat("MemoryUsage") / 1024),2);
                        resourceInfo.TotalMemory = (float)Math.Round((reader.ToFloat("TotalMemory") / 1024), 2);
                        resourceInfo.SystemCpuPercent = reader.ToFloat("system_cpu_utilization");
                        resourceInfo.SqlCpuPercent = reader.ToFloat("sql_cpu_utilization");
                        resourceInfo.EventTime = reader.ToDateTime("EventTime");
                        break;
                    }
                }
            }

            return resourceInfo;
        }

        /// <summary>
        /// Get Server information, Cpu Core, Total ram
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns></returns>
        public ServerFullInfo GetServerFullInformation(ICriteria criteria, string serverIp)
        {
            ServerFullInfo serverFullInfo = new ServerFullInfo();

            DbSetting dbSetting = new DbSetting
            {
                ConnectionString = SectionHelper.GetConnectionStringFromServerIp(serverIp, (int)ServerType.Resource),
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(dbSetting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        serverFullInfo.NumberLogicalCpu = reader.ToInt("NumberLogicalCPU");
                        serverFullInfo.HyperThreadRatio = reader.ToInt("hyperthread_ratio");
                        serverFullInfo.NumberPhysicalCpu = reader.ToInt("NumberPhysicalCPU");
                        serverFullInfo.TotalPhysicalMemory = reader.ToInt("TotalPhysicalMemory");
                        break;
                    }
                }
            }

            return serverFullInfo;
        }

        /// <summary>
        /// Get Server Status like SqlServerAgent Status
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="serverIp"></param>
        /// <returns>Current Server Status</returns>
        public ServerStatus GetServerStatus(ICriteria criteria, string connectionString)
        {
            ServerStatus serverStatus = new ServerStatus();

            DbSetting dbSetting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(dbSetting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        serverStatus.SqlServerAgentRunning = reader.ToString("SQLAgent").ToLower().Contains("running");
                        break;
                    }
                }
            }

            return serverStatus;
        }


    }
}