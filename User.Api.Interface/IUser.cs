using Microsoft.AspNetCore.Mvc;
using Users.Api.Model;

namespace Users.Api.Interface
{
    public interface IUser
    {
        Task<ActionResult<IEnumerable<User>>> GetUsers();
        Task<ActionResult<User>> GetUser(int id);        
        Task PutUser(int id, User user);
        Task<ActionResult<User>> PostUser(User user);
        Task DeleteUser(int id);
        bool UserExists(int id);
    }
}
