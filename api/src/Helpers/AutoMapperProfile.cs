using AutoMapper;
using Irrigation.Entities;
using plants = Irrigation.Models.Plants;
using users = Irrigation.Models.Users;

namespace Irrigation.Helpers
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
            CreateMap<User, users.UserModel>();
            CreateMap<users.RegisterModel, User>();
            CreateMap<users.UpdateModel, User>();

            CreateMap<plants.PlantModel, Plant>();
            CreateMap<Plant, plants.PlantModel>();
        }
    }
}
