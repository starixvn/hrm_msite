// --------------------------------------------------------------------------
// <copyright file="AliasRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2013.1.17">Created</change>
// </history>

namespace Fanex.Maintenance.Repository.Impl.ConnectionString
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using Fanex.Common.Extensions;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Criteria.Misc;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Alias repository.
    /// </summary>
    public class AliasRepository : BaseRepository<Alias>, IAliasRepository
    {
        /// <summary>
        /// Get all alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of alias connection string.</returns>
        public IEnumerable<AliasConnectionString> GetAll(ICriteria criteria)
        {
            Func<IDataReader, AliasConnectionString> objMapFunc = (reader) =>
                    {
                        return new AliasConnectionString
                        {
                            Alias = new Alias
                            {
                                Id = reader.ToInt("AliasId"),
                                Name = reader.ToString("Name"),
                                Description = reader.ToString("Description"),
                                IsActive = reader.ToBool("IsActive"),
                                Modifier = !reader.IsDBNull("ModifierUserId") ? new Users
                                {
                                    Id = reader.ToInt("ModifierUserId"),
                                    Name = reader.ToString("ModifierUsername")
                                } 
                                : new Users(),
                                ModifiedDate = !reader.IsDBNull("ModifiedDate") ? reader.ToDateTime("ModifiedDate") : DateTime.Now
                            },
                            ConnectionString = new ConnectionString
                            {
                                Id = reader.ToInt("ConnectionStringId"),
                                Alias = new Alias
                                {
                                    Id = reader.ToInt("AliasId"),
                                    Name = reader.ToString("Name")
                                },
                                InitialCatalog = reader.ToString("InitialCatalog"),
                                IntegratedSecurity = reader.ToBool("IntegratedSecurity"),
                                UserID = reader.ToString("UserId"),
                                Password = reader.ToString("Password"),
                                FreeText = reader.ToString("FreeText"),
                                Server = new DatabaseServer
                                {
                                    Id = reader.ToInt("DbServerId"),
                                    ServerIP = reader.ToString("ServerIP")
                                }
                            },
                            NumOfSite = reader.ToInt("NumOfSite")
                        };
                    };

            return this.Fetch<AliasConnectionString>(criteria, objMapFunc).OrderBy(x => x.Alias.Name).ToList();
        }

        /// <summary>
        /// Get alias by site.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of alias.</returns>
        public IEnumerable<Alias> GetBySite(ICriteria criteria)
        {
            return Fetch<Alias>(criteria).OrderBy(x => x.Name).ToList();
        }

        /// <summary>
        /// Get switch connection alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List of alias connection string connection.</returns>
        public IEnumerable<AliasConnectionStringConnection> GetSwitchConnection(ICriteria criteria)
        {
            Func<IDataReader, AliasConnectionStringConnection> objMapFunc = (reader) =>
            {
                return new AliasConnectionStringConnection
                {
                    Alias = new Alias
                    {
                        Id = reader.ToInt("AliasId"),
                        Name = reader.ToString("Name")
                    },
                    FromConnectionString = new ConnectionString
                    {
                        Id = reader.ToInt("ConnectionStringId"),
                        Alias = new Alias
                        {
                            Id = reader.ToInt("AliasId"),
                            Name = reader.ToString("Name")
                        },
                        InitialCatalog = reader.ToString("InitialCatalog"),
                        IntegratedSecurity = reader.ToBool("IntegratedSecurity"),
                        UserID = reader.ToString("UserId"),
                        Password = reader.ToString("Password"),
                        FreeText = reader.ToString("FreeText"),
                        Server = new DatabaseServer
                        {
                            Id = reader.ToInt("DbServerId"),
                            ServerIP = reader.ToString("ServerIP")
                        }
                    },
                    ToConnectionString = new ConnectionString
                    {
                        Id = reader.ToInt("ToConnectionStringId"),
                        Alias = new Alias
                        {
                            Id = reader.ToInt("AliasId"),
                            Name = reader.ToString("Name")
                        },
                        InitialCatalog = reader.ToString("ToInitialCatalog"),
                        IntegratedSecurity = reader.ToBool("ToIntegratedSecurity"),
                        UserID = reader.ToString("ToUserId"),
                        Password = reader.ToString("ToPassword"),
                        FreeText = reader.ToString("ToFreeText"),
                        Server = new DatabaseServer
                        {
                            Id = reader.ToInt("ToDbServerId"),
                            ServerIP = reader.ToString("ToServerIP")
                        }
                    }
                };
            };

            return this.Fetch<AliasConnectionStringConnection>(criteria, objMapFunc).OrderBy(x => x.Alias.Name).ToList();
        }

        /// <summary>
        /// Get alias by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Alias object.</returns>
        public Alias GetById(ICriteria criteria)
        {
            return Get<Alias>(criteria);
        }

        /// <summary>
        /// Insert or update alias.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="logCriteria">Log criteria.</param>
        /// <returns>Command result.</returns>
        public ICommandResult InsertOrUpdate(ICriteria criteria, ICriteria logCriteria)
        {
            Hashtable hash = criteria.GetSpParams();
            hash = logCriteria.AddSpParams(hash);
            hash = (new ResultCriteria()).AddSpParams(hash);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(hash);

                return GetCommandResult(hash);
            }
        }
    }
}