using AutoMapper;
using UsersDemo.DTO.InternalApi.Request;
using UsersDemo.DTO.InternalApi.Response;
using UsersDemo.Extensions.ExtraClasses;
using UsersDemo.Infrastructure.Entities;
using UsersDemo.Infrastructure.Repository;

namespace UsersDemo.Application
{
    public class UserRepo:IUserRepo
    {
        private readonly IMapper _mapper;
        private readonly CRUDService _crudService;
        public UserRepo(IMapper mapper, CRUDService crudService)
        {
            _mapper = mapper;
            _crudService = crudService;
        }

        public ApiResponse<List<UserResponse>> GetAllUser()
        {
            try
            {
                var getUsers = _crudService.GetAllUserOfDB();

                if (getUsers.Result)
                {
                    var mapUserResponse = _mapper.Map<List<UserResponse>>(getUsers.Data);

                    foreach(var user in mapUserResponse)
                    {
                        user.Image = "Images/UserImages/" + user.Image;
                    }
                    return new ApiResponse<List<UserResponse>>
                    {
                        Result = true,
                        Data = mapUserResponse
                    };
                }
                else
                {
                    return new ApiResponse<List<UserResponse>>
                    {
                        Result = getUsers.Result,
                        ErrorMessage = getUsers.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserResponse>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<UserResponse> GetUserByUsername(string username)
        {
            try
            {
                var findUser = _crudService.GetUserByUsernameInDB(username);

                if (findUser.Result)
                {
                    var mapUserResponse = _mapper.Map<UserResponse>(findUser.Data);
                    mapUserResponse.Image = "Images/UserImages/" + mapUserResponse.Image;

                    return new ApiResponse<UserResponse>
                    {
                        Result = true,
                        Data = mapUserResponse
                    };
                }
                else
                {
                    return new ApiResponse<UserResponse>
                    {
                        Result = findUser.Result,
                        ErrorMessage = findUser.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<UserResponse> AddUser(UserRequest userRequest)
        {
            try
            {
                //تبدیل base64 به تصویر و ذخیره آن روی سرور
                var base64 = userRequest.Image.Split(',')[1];
                var bytes = System.Convert.FromBase64String(base64);

                var randName = Guid.NewGuid().ToString();
                userRequest.Image = randName + ".png";

                var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/UserImages/" + randName + ".png"); // ~ => wwwroot
                System.IO.File.WriteAllBytes(path, bytes);


                var mapUserEntity = _mapper.Map<UserEntity>(userRequest);
                var addResult = _crudService.AddUserInDB(mapUserEntity);

                if (addResult.Result)
                {
                    var mapUserResponse = _mapper.Map<UserResponse>(addResult.Data);
                    mapUserResponse.Image = "Images/UserImages/" + mapUserResponse.Image;

                    return new ApiResponse<UserResponse>
                    {
                        Result = true,
                        Data = mapUserResponse
                    };
                }
                else
                {
                    return new ApiResponse<UserResponse>
                    {
                        Result = addResult.Result,
                        ErrorMessage = addResult.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }


        public ApiResponse<UserResponse> UpdateUser(string username, UserRequest userRequest)
        {
            try
            {
                var mapUserEntity = _mapper.Map<UserEntity>(userRequest);
                var updateUser = _crudService.UpdateUserInDB(username, mapUserEntity);

                if (updateUser.Result)
                {
                    var mapUserResponse = _mapper.Map<UserResponse>(updateUser.Data);

                    return new ApiResponse<UserResponse>
                    {
                        Result = true,
                        Data = mapUserResponse
                    };
                }
                else
                {
                    return new ApiResponse<UserResponse>
                    {
                        Result = updateUser.Result,
                        ErrorMessage = updateUser.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<UserResponse> DeleteUserByUsername(string username)
        {
            try
            {
                var delUser = _crudService.DeleteUserByUsernameOfDB(username);
                if (delUser.Result)
                {
                    var mapUserResponse = _mapper.Map<UserResponse>(delUser.Data);
                    mapUserResponse.Image = "Images/UserImages/" + mapUserResponse.Image;

                    var imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", mapUserResponse.Image);

                    if (File.Exists(imagePath))
                    {
                        // حذف فایل تصویر
                        System.IO.File.Delete(imagePath);
                    }

                    return new ApiResponse<UserResponse>
                    {
                        Result = true,
                        Data = mapUserResponse
                    };
                }
                else
                {
                    return new ApiResponse<UserResponse>
                    {
                        Result = delUser.Result,
                        ErrorMessage = delUser.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserResponse>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
