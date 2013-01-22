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
    using System.Linq;
    using System.Text;
    using Fanex.Maintenance.Repository.Interface.ConnectionString;
    using Fanex.ConnectionString.Domain;
    using Fanex.Maintenance.Entity.Model.ConnectionString;
    using Fanex.NAP.Framework.Data;
    using Fanex.Data;
    using System.Data;
    using Fanex.Common.Extensions;

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
                                   Id = reader.ToInt("Id"),
                                   Name = reader.ToString("Name"),
                                   Description = reader.ToString("Description"),
                                   IsActive = reader.ToBool("IsActive"),
                                   Modifier = reader.IsDBNull("Modifier") ? new Users
                                   {
                                       Id = reader.ToInt("Modifier"),
                                       Name = reader.ToString("ModifierUsername")
                                   } : new Users(),
                                   ModifiedDate = reader.IsDBNull("ModifiedDate") ? reader.ToDateTime("ModifiedDate") : DateTime.Now
                               },
                               ConnectionString = new ConnectionString
                               {
                                   Alias = new Alias
                                   {
                                       Id = reader.ToInt("Id"),
                                       Name = reader.ToString("Name")
                                   },
                                   InitialCatalog = reader.ToString("InitialCatalog"),
                                   IntegratedSecurity = reader.ToBool("IntegratedSecurity"),
                                   UserID = reader.ToString("UserId"),
                                   Password = reader.ToString("Password"),
                                   FreeText = reader.ToString("FreeText"),
                                   Server = new DatabaseServer
                                   {
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
    }
}
