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
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common.Extensions;
    using Fanex.ConnectionString.Domain;
    using Fanex.Data;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.NAP.Framework.Data;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class AliasRepository : BaseRepository<Alias>, IAliasRepository
    {
        public IEnumerable<AliasConnectionString> GetAll(ICriteria criteria)
        {
            try
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
                                   } : new Users(),
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

                return this.Fetch<AliasConnectionString>(criteria, objMapFunc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertOrUpdate(ICriteria criteria)
        {
            try
            {
                using (var db = new ObjectDb(criteria.GetSettingKey()))
                {
                    var hsParam = criteria.GetSpParams();

                    db.ExecuteNonQuery(hsParam);

                    return (int)hsParam["aliasid"];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Alias GetById(ICriteria criteria)
        {
            try
            {
                return Get<Alias>(criteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Alias> GetBySite(ICriteria criteria)
        {
            try
            {
                return Fetch<Alias>(criteria);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<AliasConnectionStringConnection> GetSwitchConnection(ICriteria criteria)
        {
            try
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

                return this.Fetch<AliasConnectionStringConnection>(criteria, objMapFunc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}