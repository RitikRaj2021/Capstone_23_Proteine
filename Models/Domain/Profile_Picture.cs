using Capstone_23_Proteine.Data;
using Microsoft.AspNetCore.Mvc;

namespace Capstone_23_Proteine.Models.Domain
{
    public class Profile_Picture
    {
    
    

    }

    public class ApplicationUser : IdentityUser
    {
        // maybe be other code in this model
        public byte[] Profile_Picture { get; set; }
    }

    public FileContentResult Picture(string userId)
    {
        // get EF Database (maybe different way in your applicaiton)
        var db = HttpContext.GetOwinContext().Get<ApplicationDbContext>();

        // find the user. I am skipping validations and other checks.
        var user = db.Users.Where(x => x.Id == userId).FirstOrDefault();

        return new FileContentResult(user.Profile_Picture, "image/jpeg");
    }
}

