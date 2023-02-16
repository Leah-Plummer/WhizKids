using System.Collections.Generic;
using WhizKids.Models;

namespace WhizKids.Repositories
{
    public interface IUserStudentRepository
    {
        List<UserStudent> GetAllUserStudents();
        UserStudent GetUserStudentById(int id);
        void AddUserStudent(int studentId, int userProfileId);
        //void UpdateUserStudent(UserStudent userStudent);
        void DeleteUserStudent(int id);
    }
}
