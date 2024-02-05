using Microsoft.AspNetCore.Mvc;
using UsersDemo.DTO.InternalApi.Request;
using UsersDemo.DTO.InternalApi.Response;
using UsersDemo.Extensions.ExtraClasses;

namespace UsersDemo.Application
{
    public interface IUserRepo
    {
        public ApiResponse<List<UserResponse>> GetAllUser();
        public ApiResponse<UserResponse> GetUserByUsername(string username);
        public ApiResponse<UserResponse> AddUser(UserRequest userRequest);
        public ApiResponse<UserResponse> UpdateUser(string username, UserRequest userRequest);
        public ApiResponse<UserResponse> DeleteUserByUsername(string username);
    }
}
