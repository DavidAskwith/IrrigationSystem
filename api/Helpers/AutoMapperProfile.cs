using AutoMapper;
using IrigationSystem.Entities;
using plants = IrigationSystem.Models.Plants;
using users = IrigationSystem.Models.Users;

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
            CreateMap<User, users.UserModel>();
            CreateMap<users.RegisterModel, User>();
            CreateMap<users.UpdateModel, User>();

            CreateMap<plants.CreateUpdateModel, Plant>();
            CreateMap<Plant, plants.PlantModel>();
        }
    }
}
