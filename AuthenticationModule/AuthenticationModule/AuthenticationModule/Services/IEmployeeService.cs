using AuthenticationModule.Models;

namespace AuthenticationModule.AuthenticationsRepository
{
    public interface IEmployeeService
    {
        UserResponse CheckUser(UserRequest userRequest);
    }
}