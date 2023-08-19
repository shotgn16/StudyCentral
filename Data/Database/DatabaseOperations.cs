using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyCentralV2.Data.Database.Models;

namespace StudyCentralV2.Data.Database
{
    public class DatabaseOperations
    {
        //public async Task<IdentityUser> GetUser(string userID)
        //{
        //    IdentityUser IUser;

        //    using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        //    {
        //        IUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userID);
        //    }
        //    return IUser;
        //}

        //public async Task<List<IdentityUser>> GetAllUsers()
        //{
        //    List<IdentityUser> IUsers = new List<IdentityUser>();

        //    using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        //    {
        //        IUsers = await context.Users.ToListAsync();
        //    }
        //    return IUsers;
        //}

        //public async Task CreateUser(IdentityUser NewUser)
        //{
        //    using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        //    {
        //        await context.Users.AddAsync(NewUser);
        //        await context.SaveChangesAsync();
        //    }
        //}

        //public async Task DeleteUser(string userID)
        //{
        //    using (var context = new ApplicationDbContext(new DbContextOptions<ApplicationDbContext>()))
        //    {
        //        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userID);

        //        if (userID != null)
        //        {
        //            context.Users.Remove(user);
        //            context.SaveChangesAsync();
        //        }
        //    }
        //}
    }
}
