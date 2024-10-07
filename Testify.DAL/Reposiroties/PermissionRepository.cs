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
        private readonly TestifyDbContext _context;
        public PermissionRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }
        public async Task<Permission> GetByidPermission(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }
        public async Task<Permission> AddPermission(Permission permission)
        {
            try
            {
                var addPermission = _context.Permissions.Add(permission).Entity;
                await _context.SaveChangesAsync();
                return addPermission;
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
                var objUpdatePermission = await _context.Permissions.FindAsync(permission.Id);

                objUpdatePermission.Name = permission.Name;
                objUpdatePermission.Description = permission.Description;
                objUpdatePermission.Status = permission.Status;

                var updatePermission = _context.Permissions.Update(objUpdatePermission).Entity;
                await _context.SaveChangesAsync();
                return updatePermission;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Permission> DeletePermission(int id)
        {
            try
            {
                var objDeletePermission = await _context.Permissions.FindAsync(id);

                _context.Permissions.Remove(objDeletePermission);
                await _context.SaveChangesAsync();
                return objDeletePermission;
            }
            catch
            {
                return null;
            }
        }
    }
}
