﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

public class User  
{
	public Guid Id { get; set; }
	public string FirstName { get; set; }

	public string LastName { get; set; }

	public string? Position { get; set; }
	public string Email { get; set; }

	public string PasswordHash { get; set; }
	public DateTime DateOfBirth { get; set; }
}

