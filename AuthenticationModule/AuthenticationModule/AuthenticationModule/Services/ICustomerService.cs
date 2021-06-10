using AuthenticationModule.Models;

namespace AuthenticationModule.AuthenticationsRepository
{
    public interface ICustomerService
    {
        UserResponse CheckUser(UserRequest userRequest);
    }
}