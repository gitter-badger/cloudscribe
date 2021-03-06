﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2014-08-10
// Last Modified:			2015-11-18
// 

using cloudscribe.DbHelpers.MSSQL;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace cloudscribe.Core.Repositories.MSSQL
{

    internal class DBUserLogins
    {
        internal DBUserLogins(
            string dbReadConnectionString,
            string dbWriteConnectionString,
            ILoggerFactory loggerFactory)
        {
            logFactory = loggerFactory;
            readConnectionString = dbReadConnectionString;
            writeConnectionString = dbWriteConnectionString;
        }

        private ILoggerFactory logFactory;
        //private ILogger log;
        private string readConnectionString;
        private string writeConnectionString;

        /// <summary>
        /// Inserts a row in the mp_UserLogins table. Returns new integer id.
        /// </summary>
        /// <returns>int</returns>
        public async Task<bool> Create(
            int siteId,
            string loginProvider, 
            string providerKey,
            string providerDisplayName,
            string userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                writeConnectionString, 
                "mp_UserLogins_Insert", 
                5);

            sph.DefineSqlParameter("@LoginProvider", SqlDbType.NVarChar, 128, ParameterDirection.Input, loginProvider);
            sph.DefineSqlParameter("@ProviderKey", SqlDbType.NVarChar, 128, ParameterDirection.Input, providerKey);
            sph.DefineSqlParameter("@UserId", SqlDbType.NVarChar, 128, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            sph.DefineSqlParameter("@ProviderDisplayName", SqlDbType.NVarChar, 100, ParameterDirection.Input, providerDisplayName);

            int rowsAffected = await sph.ExecuteNonQueryAsync();
            return (rowsAffected > 0);
        }


        /// <summary>
        /// Deletes a row from the mp_UserLogins table. Returns true if row deleted.
        /// </summary>
        /// <param name="loginProvider"> loginProvider </param>
        /// <param name="providerKey"> providerKey </param>
        /// <param name="userId"> userId </param>
        /// <returns>bool</returns>
        public async Task<bool> Delete(int siteId, string loginProvider, string providerKey, string userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                writeConnectionString, 
                "mp_UserLogins_Delete", 
                4);

            sph.DefineSqlParameter("@LoginProvider", SqlDbType.NVarChar, 128, ParameterDirection.Input, loginProvider);
            sph.DefineSqlParameter("@ProviderKey", SqlDbType.NVarChar, 128, ParameterDirection.Input, providerKey);
            sph.DefineSqlParameter("@UserId", SqlDbType.NVarChar, 128, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            int rowsAffected = await sph.ExecuteNonQueryAsync();
            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteByUser(int siteId, string userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                writeConnectionString, 
                "mp_UserLogins_DeleteByUser", 
                2);

            sph.DefineSqlParameter("@UserId", SqlDbType.NVarChar, 128, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            int rowsAffected = await sph.ExecuteNonQueryAsync();
            return (rowsAffected > 0);

        }

        public async Task<bool> DeleteBySite(int siteId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                writeConnectionString, 
                "mp_UserLogins_DeleteBySite", 
                1);

            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            int rowsAffected = await sph.ExecuteNonQueryAsync();
            return (rowsAffected > 0);

        }

        public async Task<DbDataReader> Find(
            int siteId,
            string loginProvider, 
            string providerKey)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                readConnectionString, 
                "mp_UserLogins_Find", 
                3);

            sph.DefineSqlParameter("@LoginProvider", SqlDbType.NVarChar, 128, ParameterDirection.Input, loginProvider);
            sph.DefineSqlParameter("@ProviderKey", SqlDbType.NVarChar, 128, ParameterDirection.Input, providerKey);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            return await sph.ExecuteReaderAsync();

        }


        public async Task<DbDataReader> GetByUser(
            int siteId,
            string userId)
        {
            SqlParameterHelper sph = new SqlParameterHelper(
                logFactory,
                readConnectionString, 
                "mp_UserLogins_SelectByUser", 
                2);

            sph.DefineSqlParameter("@UserId", SqlDbType.NVarChar, 128, ParameterDirection.Input, userId);
            sph.DefineSqlParameter("@SiteId", SqlDbType.Int, ParameterDirection.Input, siteId);
            return await sph.ExecuteReaderAsync();

        }



    }

}
