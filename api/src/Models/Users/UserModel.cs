namespace Irrigation.Models.Users
{
    /// <summary>
    /// DTO for returning a user.
    /// </summary>
    public class UserModel
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
