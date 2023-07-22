namespace Users.Api.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            { 
                return DateTime.Now.Year - DateOfBirth.Year; 
            }
        }
    }
}
