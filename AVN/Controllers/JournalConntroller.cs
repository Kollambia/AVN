﻿using AVN.Business;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Controllers
{
    public class JournalController : Controller
    {
        private readonly IGroupService groupService;

        public JournalController(IGroupService groupService)
        {
            this.groupService = groupService;

        }
    }
}
