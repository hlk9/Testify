using Microsoft.EntityFrameworkCore;
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
        public async Task<List<UserPermission>> GetByUserIdPermission(Guid id)
        {
            var listUP =  _context.UserPermissions.Where(x => x.UserId == id).ToList();
            return listUP;
        }

        public bool AddListUserPermission(List<UserPermission> userPermission)
        {
            foreach (var item in userPermission)
            {
                _context.UserPermissions.Add(item);
            }
            return  _context.SaveChanges() >= userPermission.Count;
            
        }

        public bool RemoveListUserPermission(List<int> ids)
        {
            foreach (var item in ids)
            {
                var up= _context.UserPermissions.Find(item);
                if(up != null)
                    _context.UserPermissions.Remove(up);
            }
            return  _context.SaveChanges() >0;
        }

        public bool AddUserPermission(UserPermission userPermission)
        {
            _context.UserPermissions.Add(userPermission);
            return _context.SaveChanges() > 0;
        }
        public async Task<bool> HasPermissionAsync(Guid userId, string permission)
        {
            return await (from up in _context.UserPermissions
                          join p in _context.Permissions on up.PermissionId equals p.Id
                          where up.UserId == userId && p.Name == permission
                          select up).AnyAsync();
        }

        public bool DeleteUserPermission(int id)
        {
            var userPermission = _context.UserPermissions.Find(id);
            if (userPermission != null)
            {
                _context.UserPermissions.Remove(userPermission);
                return _context.SaveChanges() > 0;
            }
            return false;
        }
    }
}
