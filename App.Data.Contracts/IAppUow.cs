using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Model;

namespace App.Data.Contracts
{
    public interface IAppUow
    {
        void Commit();


        IUserProfileRepository UserProfiles { get; }

    }
}
