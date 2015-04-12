﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations
{
    public class Department : IEntity
    {
        private readonly Organization m_parentOrganization;
        private readonly int m_id;
        public Department(int id, Organization parentOrganization)
        {
            m_id = id;
            m_parentOrganization = parentOrganization;
        }

        public int Id
        {
            get
            {
                return m_id;
            }
        }

        public Organization ParentOrganization
        {
            get
            {
                return m_parentOrganization;
            }
        }

        public string Name { get; set; }

        public new int GetEntityCode()
        {
            return 1;
        }

    }
}