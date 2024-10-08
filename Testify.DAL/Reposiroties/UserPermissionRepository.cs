﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class UserPermissionRepository
    {
        private readonly TestifyDbContext _context;
        public UserPermissionRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<UserPermission>> GetAllUserPermissions()
        {
            return await _context.UserPermissions.ToListAsync();
        }
        public async Task<UserPermission> GetByidUserPermission(int id)
        {
            return await _context.UserPermissions.FindAsync(id);
        }
    }
}
