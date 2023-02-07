using System.Collections.Generic;
using WhizKids.Models;

namespace WhizKids.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAllUsersProfiles();
        UserProfile GetUserProfileById(int id);
        void AddUserProfile(UserProfile user);
        void UpdateUserProfile(UserProfile user);
        void DeleteUserProfile(int id);
    }
}