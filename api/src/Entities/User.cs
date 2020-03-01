using System.Collections.Generic;

namespace Irrigation.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public ICollection<Plant> Plants { get; set; }

        public Settings Settings { get; set; }
    }
}
