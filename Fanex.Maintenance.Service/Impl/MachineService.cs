// --------------------------------------------------------------------------
// <copyright file="MachineService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.8.7">Created</change>
// </history>

namespace Fanex.Maintenance.Service.Impl
{
    using System.Collections.Generic;
    using Fanex.Maintenance.Criteria.Log;
    using Fanex.Maintenance.Entity.Model;
    using Fanex.Maintenance.Repository.Impl;

    /// <summary>
    /// Machine Service
    /// </summary>
    public class MachineService
    {
        #region Member

        private readonly MachineRepository _machineRepository = new MachineRepository();

        #endregion Member

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private MachineService()
        {
        }

        /// <summary>
        /// Create machine service
        /// </summary>
        /// <returns></returns>
        public static MachineService Create()
        {
            return new MachineService();
        }

        #endregion Constructor

        /// <summary>
        /// Get all machines
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Machine> GetAll()
        {
            GetAllMachinesCriteria criteria = new GetAllMachinesCriteria();

            return _machineRepository.Fetch(criteria);
        }

        /// <summary>
        /// Get machines by machine type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Machine> GetByType(bool? machineType)
        {
            GetMachinesByTypeCriteria criteria = new GetMachinesByTypeCriteria
            {
                MachineType = machineType
            };

            return _machineRepository.Fetch(criteria);
        }

        /// <summary>
        /// Insert machine
        /// </summary>
        /// <param name="machineName"></param>
        /// <param name="machineType"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public CommandResult InsertMachine(string machineName, bool machineType, short timeZone)
        {
            InsertMachineCriteria criteria = new InsertMachineCriteria
            {
                MachineName = machineName,
                IsProduction = machineType,
                Timezone = timeZone
            };

            return _machineRepository.InsertMachine(criteria);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="machineType"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public CommandResult InsertMachine(bool machineType, short timeZone)
        {
            UpdateMachineCriteria criteria = new UpdateMachineCriteria
            {
                IsProduction = machineType,
                Timezone = timeZone
            };

            return _machineRepository.UpdateMachine(criteria);
        }
    }
}