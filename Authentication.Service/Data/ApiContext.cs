using System;
using Authentication.Service.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Service.Data
{
	public class ApiContext : IdentityDbContext<User, IdentityRole<int>,int>
	{
		public ApiContext()
		{

		}
		public ApiContext(DbContextOptions<ApiContext> option) : base(option)
		{
		}


	}
}

