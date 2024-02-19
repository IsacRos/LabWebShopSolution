using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.EntityFrameworkCore;

namespace LabWebShop.Models
{
    [Collection("User")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {

    }
}
