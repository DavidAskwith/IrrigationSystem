namespace Irrigation.Models.Users
{
    /// <summary>
    /// DTO used for defining the parameter of a user authentication post.
    /// </summary>
    public class UpdateModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
