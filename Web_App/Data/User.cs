﻿using Microsoft.AspNetCore.Identity;

namespace Web_App.Data
{
    public class User : IdentityUser
    {
        public string Department { get; set; }
        public string Position { get; set; }
    }
}
