using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testify.DAL.Context;
using Testify.DAL.Models;

namespace Testify.DAL.Reposiroties
{
    public class PermissionRepository
    {
        TestifyDbContext _context;
        public PermissionRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }
        public async Task<Permission> GetPermissionId(int id) 
        {
            return await _context.Permissions.FindAsync(id);
        }
        public async Task<Permission> CreatePermission(Permission permission) 
        {
            try
            {
                var create = _context.Permissions.Add(permission).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Permission> UpdatePermission(Permission permission)
        {
            try
            {
                var obj = await _context.Permissions.FindAsync(permission.Id);
                obj.Name = permission.Name;
                obj.Description = permission.Description;
                obj.Status = permission.Status;

                var update = _context.Permissions.Update(permission).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<Permission> DeletePermission(int id) 
        {
            try
            {
                var delete = await _context.Permissions.FindAsync(id);
                _context.Permissions.Remove(delete);
                await _context.SaveChangesAsync();
                return delete;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
