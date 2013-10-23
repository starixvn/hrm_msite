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
    using System;
    using Fanex.Common.Extensions;
    using Fanex.Data;
    using Fanex.Maintenance.Core.Helper;
    using Fanex.Maintenance.Core.Web;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Interface.JobManagement;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Server repository.
    /// </summary>
    public class ServerRepository : BaseRepository<ResourceInfo>, IServerRepository
    {
        /// <summary>
        /// Get Resource information of database.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="connectionString">Connection string.</param>
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

            DbSetting setting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(setting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        resourceInfo.MemoryUsage = (float)Math.Round(reader.ToFloat("MemoryUsage") / 1024, 2);
                        resourceInfo.TotalMemory = (float)Math.Round(reader.ToFloat("TotalMemory") / 1024, 2);
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
        /// Get Server information, Cpu Core, Total ram.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="serverIp">Server IP.</param>
        /// <returns></returns>
        public ServerFullInfo GetServerFullInformation(ICriteria criteria, string serverIp)
        {
            ServerFullInfo serverFullInfo = new ServerFullInfo();

            DbSetting setting = new DbSetting
            {
                ConnectionString = SectionHelper.GetConnectionStringFromServerIp(serverIp, (int)ServerType.Resource),
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(setting))
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
        /// Get Server Status like SqlServerAgent Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Current Server Status.</returns>
        public ServerStatus GetServerStatus(ICriteria criteria, string connectionString)
        {
            ServerStatus serverStatus = new ServerStatus();

            DbSetting setting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(setting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        serverStatus.SqlServerAgentRunning = reader.ToString("SQLAgent").ToLower().Contains("running");
                        serverStatus.SqlServerCanConnect = true;
                        break;
                    }
                }
            }

            return serverStatus;
        }

        /// <summary>
        /// Get server CPU utilization.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Current Server Status.</returns>
        public CPUInfo GetServerCpuUtilization(ICriteria criteria, string connectionString)
        {
            CPUInfo cpuInfo = new CPUInfo
            {
                SqlCpuPercent = 0,
                SystemCpuPercent = 0,
                EventTime = DateTime.Now
            };

            DbSetting setting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(setting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        cpuInfo.SystemCpuPercent = reader.ToFloat("system_cpu_utilization");
                        cpuInfo.SqlCpuPercent = reader.ToFloat("sql_cpu_utilization");
                        cpuInfo.EventTime = reader.ToDateTime("EventTime");
                        break;
                    }
                }
            }

            return cpuInfo;
        }

        /// <summary>
        /// Get server memory utilization.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="connectionString">Connection string.</param>
        /// <returns>Current Server Status.</returns>
        public MemoryInfo GetServerMemoryUtilization(ICriteria criteria, string connectionString)
        {
            MemoryInfo memoryInfo = new MemoryInfo
            {
                MemoryUsage = 0,
                TotalMemory = 0
            };

            DbSetting setting = new DbSetting
            {
                ConnectionString = connectionString,
                SpName = SectionHelper.GetSpNameFromSpKey(criteria.GetSettingKey())
            };

            using (ObjectDb objectDb = new ObjectDb(setting))
            {
                using (var reader = objectDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        memoryInfo.MemoryUsage = (float)Math.Round(reader.ToFloat("MemoryUsage") / 1024, 2);
                        memoryInfo.TotalMemory = (float)Math.Round(reader.ToFloat("TotalMemory") / 1024, 2);
                        break;
                    }
                }
            }

            return memoryInfo;
        }
    }
}