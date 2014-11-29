using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataTableStorage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NominateAndVote.DataTableStorage
{
    public static class TableNames
    {
        private static string TABLE_NAME_PATTERN = "^[A-Za-z][A-Za-z0-9]{2,62}$";
        private static Dictionary<Type, string> tableNames = new Dictionary<Type, string>();

        static TableNames()
        {
            SetTableName(typeof(AdministratorEntity), "administrator");
            SetTableName(typeof(NewsEntity), "news");
            SetTableName(typeof(NominationEntity), "nomination");
            SetTableName(typeof(PollEntity), "poll");
            SetTableName(typeof(PollSubjectEntity), "pollsubject");
            SetTableName(typeof(UserEntity), "user");
            SetTableName(typeof(VoteEntity), "vote");
        }

        public static void SetTableName(Type entityType, String tableName)
        {
            checkType(entityType);
            checkTableName(tableName);

            // table names are case-insensitive, so make it lowercase
            tableName = tableName.ToLower();

            if (tableNames.ContainsKey(entityType))
            {
                tableNames[entityType] = tableName;
            }
            else
            {
                tableNames.Add(entityType, tableName);
            }
        }

        public static string GetTableName(Type entityType)
        {
            checkType(entityType);

            if (tableNames.ContainsKey(entityType))
            {
                return tableNames[entityType];
            }
            else
            {
                throw new ArgumentException("No table name is set for the given entity type '" + entityType.FullName + "'", "entityType");
            }
        }

        public static List<Type> GetEntityTypes()
        {
            return (from entityType in tableNames.Keys
                    orderby entityType.FullName
                    select entityType).ToList();
        }

        public static List<string> GetTableNames()
        {
            return (from tableName in tableNames.Values
                    orderby tableName
                    select tableName).ToList();
        }

        public static Dictionary<Type, string> GetDictonary()
        {
            return new Dictionary<Type, string>(tableNames);
        }

        private static void checkTableName(String tableName)
        {
            if (tableName == null)
            {
                throw new ArgumentNullException("The table name must not be null", "tableName");
            }
            else if (!Regex.IsMatch(tableName, TABLE_NAME_PATTERN))
            {
                throw new ArgumentException("The table name '" + tableName + "' must match the '" + TABLE_NAME_PATTERN + "' regular expression ", "tableName");
            }
        }

        private static void checkType(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("The entity type must not be null", "entityType");
            }
            else if (!entityType.GetInterfaces().Contains(typeof(ITableEntity)))
            {
                throw new ArgumentException("The entity type '" + entityType.FullName + "' must implement the " + typeof(ITableEntity).FullName + " interface", "entityType");
            }
        }
    }
}