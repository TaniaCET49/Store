using Microsoft.AspNetCore.Identity;

namespace Store_Web.Data.Enteties
{
    public class User : IdentityUser
    {
        public string FristName { get; set; }

        public string LastName { get; set; }
    }
}
