using Microsoft.WindowsAzure.Storage.Table;
using NominateAndVote.DataModel.Common;
using NominateAndVote.DataTableStorage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NominateAndVote.DataTableStorage
{
    public static class TableNames
    {
        private const string TableNamePattern = "^[A-Za-z][A-Za-z0-9]{2,62}$";
        private static readonly Dictionary<Type, string> TableNamesDictionary = new Dictionary<Type, string>();

        static TableNames()
        {
            ResetToDefault();
        }

        public static void Clear()
        {
            TableNamesDictionary.Clear();
        }

        public static void ResetToDefault(string prefix = null)
        {
            if (prefix == null)
            {
                prefix = "";
            }

            Clear();

            SetTableName(typeof(AdministratorEntity), prefix + "administrator");
            SetTableName(typeof(NewsEntity), prefix + "news");
            SetTableName(typeof(NominationEntity), prefix + "nomination");
            SetTableName(typeof(PollEntity), prefix + "poll");
            SetTableName(typeof(PollSubjectEntity), prefix + "pollsubject");
            SetTableName(typeof(UserEntity), prefix + "user");
            SetTableName(typeof(VoteEntity), prefix + "vote");
        }

        public static void SetTableName(Type entityType, String tableName)
        {
            CheckType(entityType);
            CheckTableName(tableName);

            if (GetTableNames().Contains(tableName.ToLower()))
            {
                var otherEntityType = GetEntityType(tableName);
                if (otherEntityType != entityType)
                {
                    // not an update
                    throw new ArgumentException("The table name '" + tableName + "' is already taken by '" + otherEntityType.FullName + "'");
                }
            }

            // table names are case-insensitive, so make it lowercase
            tableName = tableName.ToLower();

            if (TableNamesDictionary.ContainsKey(entityType))
            {
                TableNamesDictionary[entityType] = tableName;
            }
            else
            {
                TableNamesDictionary.Add(entityType, tableName);
            }
        }

        public static Type GetEntityType(string tableName)
        {
            CheckTableName(tableName);

            var q = from entry in TableNamesDictionary
                    where entry.Value == tableName.ToLower()
                    select entry.Key;

            var entityType = q.SingleOrDefault();

            if (entityType == null)
            {
                throw new ArgumentException(
                    "No entity type is set for the given table name '" + tableName + "'", "tableName");
            }

            return entityType;
        }

        public static string GetTableName(Type entityType)
        {
            CheckType(entityType);

            string tableName;

            if (!TableNamesDictionary.TryGetValue(entityType, out tableName))
            {
                throw new ArgumentException("No table name is set for the given entity type '" + entityType.FullName + "'", "entityType");
            }
            return tableName;
        }

        public static List<Type> GetEntityTypes()
        {
            var q = from entityType in TableNamesDictionary.Keys
                    orderby entityType.FullName
                    select entityType;

            return q.ToSortedList(new TypeComparer());
        }

        public static List<string> GetTableNames()
        {
            var q = from tableName in TableNamesDictionary.Values
                    orderby tableName
                    select tableName;

            return q.ToList();
        }

        public static Dictionary<Type, string> GetDictionary()
        {
            return new Dictionary<Type, string>(TableNamesDictionary);
        }

        private static void CheckTableName(string tableName)
        {
            if (tableName == null)
            {
                throw new ArgumentNullException("tableName", "The table name must not be null");
            }
            if (!Regex.IsMatch(tableName, TableNamePattern))
            {
                throw new ArgumentException("The table name '" + tableName + "' must match the '" + TableNamePattern + "' regular expression ", "tableName");
            }
        }

        private static void CheckType(Type entityType)
        {
            if (entityType == null)
            {
                throw new ArgumentNullException("entityType", "The entity type must not be null");
            }
            if (!entityType.GetInterfaces().Contains(typeof(ITableEntity)))
            {
                throw new ArgumentException("The entity type '" + entityType.FullName + "' must implement the " + typeof(ITableEntity).FullName + " interface", "entityType");
            }
        }
    }
}