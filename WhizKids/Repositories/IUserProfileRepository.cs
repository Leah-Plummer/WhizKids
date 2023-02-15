using System.Collections.Generic;
using WhizKids.Models;

namespace WhizKids.Repositories
{
    public interface IUserProfileRepository
    {
        List<UserProfile> GetAllUsersProfiles();
        UserProfile GetUserProfileById(int id);
        void AddUserProfile(UserProfile user, UserStudent userStudent);
        void UpdateUserProfile(UserProfile user);
        void DeleteUserProfile(int id);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetUserProfilesByStudentId(int studentId);

    }
}