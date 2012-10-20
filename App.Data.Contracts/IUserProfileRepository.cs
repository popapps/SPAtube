using App.Model;

namespace App.Data.Contracts
{
    public interface IUserProfileRepository: IRepository<UserProfile>
    {

        UserProfile GetByUserName(string userName);
    }
}
