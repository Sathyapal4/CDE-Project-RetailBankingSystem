using AuthenticationModule.Models;

namespace AuthenticationModule.AuthenticationsRepository
{
    public interface ILoginRepository
    {
        UserResponse Login(UserRequest userRequest);
    }
}