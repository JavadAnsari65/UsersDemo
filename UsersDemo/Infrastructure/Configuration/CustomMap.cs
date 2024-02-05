using AutoMapper;
using UsersDemo.DTO.InternalApi.Request;
using UsersDemo.DTO.InternalApi.Response;
using UsersDemo.Infrastructure.Entities;

namespace UsersDemo.Infrastructure.Configuration
{
    public class CustomMap:Profile
    {
        public CustomMap()
        {
            CreateMap<UserRequest, DTO.ExternalApi.Request.UserRequest>().ReverseMap();
            CreateMap<UserRequest, UserEntity>().ReverseMap();

            CreateMap<UserResponse, DTO.ExternalApi.Response.UserResponse>().ReverseMap();
            CreateMap<UserResponse, UserEntity>().ReverseMap();
        }
    }
}
