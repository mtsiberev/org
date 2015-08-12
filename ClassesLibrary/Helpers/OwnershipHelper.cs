using System;
using System.Collections.Generic;
using WebMatrix.WebData;

namespace Organizations.Helpers
{
    public static class OwnershipHelper
    {
        private const string c_ownersDb = "Owners";
        private const string c_lastUpdatedDatabaseName = "Organizations";

        private static int GetLastAddedEntityId()
        {
            var ownerId = -1;
            var queryString = String.Format("SELECT IDENT_CURRENT('{0}')", c_lastUpdatedDatabaseName);
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var decimalResult = (Decimal)reader.GetValue(0);
                        ownerId = Decimal.ToInt32(decimalResult);
                    }
                }
            }
            return ownerId;
        }

        private static int GetCurrentUserId()
        {
            return WebSecurity.CurrentUserId;
        }

        public static void WriteOwner()
        {
            var currentUserId = GetCurrentUserId();
            var addedEntityId = GetLastAddedEntityId();

            var queryString = String.Format("INSERT INTO {0} (OwnerId, OrganizationId) VALUES ('{1}', '{2}');",
                c_ownersDb, currentUserId, addedEntityId);
            AdoHelper.ExecCommand(queryString);
        }

        public static List<KeyValuePair<int, int>> GetOwnersListForCurrentUser()
        {
            var resultList = new List<KeyValuePair<int, int>>();
            var queryString = String.Format("SELECT * FROM {0} WHERE OwnerId = {1};", c_ownersDb, GetCurrentUserId());
            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var userId = (int)reader.GetValue(0);
                        var entityId = (int)reader.GetValue(1);
                        resultList.Add(new KeyValuePair<int, int>(userId, entityId));
                    }
                }
            }
            return resultList;
        }
        
        public static bool IsCurrentUserOwner(int entityId, List<KeyValuePair<int, int>> list)
        {
            var userId = GetCurrentUserId();
            foreach(var pair in list)
            {
                if (pair.Key == userId && pair.Value == entityId)
                    return true;
            }
            return false;
        }
    }

}
