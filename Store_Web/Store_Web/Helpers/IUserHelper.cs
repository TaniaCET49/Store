using Microsoft.AspNetCore.Identity;
using Store_Web.Data.Enteties;
using Store_Web.Models;
using System.Threading.Tasks;

namespace Store_Web.Helpers
{
    public interface IUserHelper
    {

        Task<User> GetUserByEmailAsync(string email);



        Task<IdentityResult> AddUserAsync(User user, string password);


        Task<SignInResult> LoginAsync(LoginViewModel model);


        Task LogoutAsync();

        Task<IdentityResult> ChangePasswordAsync(User user);


        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);


        Task CheckRoleAsync(string roleName);

        
        Task AddUsertoRoleAsync(User user, string roleName);

        
        Task<bool>  IsUserInRoleAsync(User user, string roleName);
    }


}

