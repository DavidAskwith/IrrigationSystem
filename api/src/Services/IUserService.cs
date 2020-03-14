using System.Threading.Tasks;
using System.Collections.Generic;
using Irrigation.Entities;

namespace Irrigation.Services
{
    /// <summary>
    /// Defines the attributes a UserService must impliment.
    /// </summary>
    public interface IUserService
    {
        User Authenticate(string username, string password);

        User Create(User user, string password);

        void Update(User user, string password = null);

        // TODO: DELETE
        IEnumerable<User> GetAll();

        Task<User> GetByIdAsync(int userId);
    }
}
