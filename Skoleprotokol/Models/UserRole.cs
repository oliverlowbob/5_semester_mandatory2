﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace Skoleprotokol.Models
{
    public partial class UserRole
    {
        public int UserIduser { get; set; }
        public int RoleIdrole { get; set; }

        public virtual Role RoleIdroleNavigation { get; set; }
        public virtual User UserIduserNavigation { get; set; }
    }
}