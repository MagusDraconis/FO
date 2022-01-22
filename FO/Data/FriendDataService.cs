using FO.DataAccess;
using FO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FO.UI
{
    public class FriendDataService : IFriendDataService
    {
        readonly Func<FoDbContext> _FoDbContextCreator;
        public FriendDataService(Func<FoDbContext> foDbContextCreator)
        {
            this._FoDbContextCreator = foDbContextCreator;
        }
        public async Task<Friend?> GetByIdAsync(int id)
        {
            using (var context = _FoDbContextCreator())
            {
                if (context == null || context.Friends == null)
                    return null;

                return await context.Friends.SingleAsync(c => c.Id == id);
            }


        }

        public async Task SaveAsync(Friend friend)
        {
            using (var context = _FoDbContextCreator())
            {
                context.Friends?.Attach(friend);
                context.Entry(friend).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
