using FO.DataAccess;
using FO.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FO.UI.Data.Looups
{
    public class LookupDataService : IFriendLookupDataService
    {
        readonly Func<FoDbContext> _FoDbContextCreator;
        public LookupDataService(Func<FoDbContext> foDbContextCreator)
        {
            this._FoDbContextCreator = foDbContextCreator;
        }

        public async Task<IEnumerable<LookupItem>?> GetFriendLookupAsync()
        {
            using var context = _FoDbContextCreator();
            if(context == null || context.Friends == null)
                return null;

            return await context.Friends.AsNoTracking()
                .Select(f => new LookupItem
                {
                    Id = f.Id,
                    DisplayMember = $"{f.FirstName} {f.LastName}"
                })
                .ToListAsync();
        }

    }
}
