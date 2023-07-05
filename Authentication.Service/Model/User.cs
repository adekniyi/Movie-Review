using System;
using Microsoft.AspNetCore.Identity;

namespace Authentication.Service.Model
{
	public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public DateTime CreatedBy { get; set; }
        public DateTime UpdatedBy { get; set; }
    }
}

