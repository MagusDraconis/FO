using FO.DataAccess;
using FO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI
{
    public class FriendRepository : IFriendRepository
    {
        readonly FoDbContext _Context;
        public FriendRepository(FoDbContext context)
        {
            this._Context = context;
        }
        public async Task<Friend?> GetByIdAsync(int friendId)
        {

            if (_Context == null || _Context.Friends == null)
                return null;

            return await _Context.Friends.SingleAsync(c => c.Id == friendId);
            


        }

        public bool HasChanges()
        {
            return _Context.ChangeTracker.HasChanges();
        }

        public async Task SaveAsync()
        {
            await _Context.SaveChangesAsync();

        }
    }
}
