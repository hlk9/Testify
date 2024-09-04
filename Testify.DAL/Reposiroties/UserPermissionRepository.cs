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
    public class UserPermissionRepository
    {
        TestifyDbContext _context;
        public UserPermissionRepository()
        {
            _context = new TestifyDbContext();
        }
        public async Task<List<UserPermission>> GetAllUserPermission()
        {
            return await _context.UserPermissions.ToListAsync();
        }
        public async Task<UserPermission> GetUserPermissionId(int id)
        {
            return await _context.UserPermissions.FindAsync(id);
        }
        public async Task<UserPermission> CreateUserPermission(UserPermission userPermission)
        {
            try
            {
                var create = _context.UserPermissions.Add(userPermission).Entity;
                await _context.SaveChangesAsync();
                return create;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<UserPermission> UpdateUserPermission(UserPermission userPermission)
        {
            try
            {
                var obj = await _context.UserPermissions.FindAsync(userPermission.Id);
                obj.PermissionId = userPermission.Id;
                obj.UserId = userPermission.UserId;

                var update = _context.UserPermissions.Update(obj).Entity;
                await _context.SaveChangesAsync();
                return update;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<UserPermission> DeleteUserPermission(int id)
        {
            try
            {
                var delete = await _context.UserPermissions.FindAsync(id);
                _context.UserPermissions.Remove(delete);
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
