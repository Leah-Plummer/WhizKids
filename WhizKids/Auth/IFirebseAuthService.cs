using System.Threading.Tasks;
using WhizKids.Auth.Models;

namespace WhizKids.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}