// --------------------------------------------------------------------------
// <copyright file="ServerFullInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Ricky.Nguyen" date="2012.10.25">Created</change>
// </history>

namespace Fanex.Maintenance.Entity.Model
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServerFullInfo
    {
        //-- Scritp to get Processor Info from Registry
        //EXEC xp_instance_regread
        //'HKEY_LOCAL_MACHINE',
        //'HARDWARE\DESCRIPTION\System\CentralProcessor\0',
        //'ProcessorNameString';

        //-- Script to get CPU and Memory Info
        //SELECT 
        // cpu_count AS [Number of Logical CPU]
        //,hyperthread_ratio
        //,cpu_count/hyperthread_ratio AS [Number of Physical CPU]
        //,physical_memory_in_bytes/1048576 AS [Total Physical Memory IN MB]
        //FROM sys.dm_os_sys_info OPTION (RECOMPILE);
        public string CpuName { get; set; }

        public int NumberLogicalCpu { get; set; }

        public int HyperThreadRatio { get; set; }

        public int NumberPhysicalCpu { get; set; }

        public int TotalPhysicalMemory { get; set; }
    }
}
