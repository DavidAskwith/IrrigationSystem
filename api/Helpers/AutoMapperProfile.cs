using AutoMapper;
using IrigationSystem.Entities;
using IrigationSystem.Models.Users;

namespace IrigationSystem.Helpers
{
    /// <summary>
    /// User to map entities to DTOs.
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Instantiates the profile adds mappings.
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();

            CreateMap<RegisterModel, User>();

            CreateMap<UpdateModel, User>();
        }
    }
}
