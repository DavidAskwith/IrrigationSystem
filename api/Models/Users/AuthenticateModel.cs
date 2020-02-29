using System.ComponentModel.DataAnnotations;

namespace IrigationSystem.Models.Users
{
    /// <summary>
    /// DTO used for defining the parameter of a user authentication post.
    /// </summary>
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
