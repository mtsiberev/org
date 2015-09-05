using System;
using System.Collections.Generic;
using Organizations.Entity;
using Organizations.Helpers;

namespace Organizations.EntitiesLists
{
    public class UsersList
    {
        public List<User> Content { get; private set; }

        public void GetContent()
        {
            var resultList = new List<User>();

            var queryString = String.Format(
             "SELECT * FROM {0};", "Users");

            using (var reader = AdoHelper.GetDataTableReader(queryString))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                      //  var organizationDb = MapperDb.GetOrganizationDb(reader);
                     //   resultList.Add(MapperBm.GetOrganization(organizationDb));
                    }
                }
            }
            //return resultList;
        }
    }

    

}