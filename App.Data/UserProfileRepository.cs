using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Data.Contracts;
using App.Model;

namespace App.Data
{
    public class UserProfileRepository : BaseRepository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }

        public UserProfile GetByUserName(string userName)
        {
            return DbSet.FirstOrDefault(t => t.UserName.ToLower() == userName.ToLower());
        }
    }
}
