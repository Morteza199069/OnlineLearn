using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using OnlineLearn.DataLayer.Entities.User;

namespace TopLearn.Core.DTOs
{
    public class UsersInAdminVM
    {
        public List<User> Users { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
