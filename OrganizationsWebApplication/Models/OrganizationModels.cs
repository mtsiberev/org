﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Organizations;

namespace OrganizationsWebApplication.Models
{
    public class SimpleOrganization
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }

    public class OrganizationModels
    {
        public List<SimpleOrganization> Organizations {  get;  private set; }

        public OrganizationModels(List<SimpleOrganization> organizationsList)
        {
            Organizations = organizationsList;
        }
        
    }
}
