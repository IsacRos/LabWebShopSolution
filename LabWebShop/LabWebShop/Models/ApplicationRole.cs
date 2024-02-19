using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.EntityFrameworkCore;

namespace LabWebShop.Models
{
    [Collection("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {

    }
}
