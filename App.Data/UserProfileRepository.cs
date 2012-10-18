using System.Linq;
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
