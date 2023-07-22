using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Users.Api.Interface;
using Users.Api.Model;

namespace Users.Api.Service
{
    public class UserService : IUser
    {
        private readonly UserDbContext _context;

        public UserService(UserDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {            
            return await _context.Users.ToListAsync();
        }

        public async Task<ActionResult<User>> GetUser(int id)
        {            
            var user = await _context.Users.FindAsync(id);            

            return user;
        }

        public async Task PutUser(int id, User user)
        {
            try
            {
                IsValidUser(user);
            }
            catch { throw; }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
              var result =  await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }            
           
        }

        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                IsValidUser(user);
            }
            catch { throw; }

            var result = _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return result.Entity;

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task DeleteUser(int id)
        {            
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return;
        }

        public bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }

        public static bool IsValidUser(User user)
        {
            if (!IsValidEmail(user.Email))
            {
                throw new Exception("Invalid email id..");
            }
            if (DateTime.Now.Year - user.DateOfBirth.Year < 18)
            {
                throw new Exception("User must be 18 years or older..");
            }

            return true;
        }
     
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
                      

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
