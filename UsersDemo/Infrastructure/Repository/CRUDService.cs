using UsersDemo.Extensions.ExtraClasses;
using UsersDemo.Infrastructure.Configuration;
using UsersDemo.Infrastructure.Entities;

namespace UsersDemo.Infrastructure.Repository
{
    public class CRUDService
    {
        private readonly UserDbContext _userDbContext;
        public CRUDService(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public ApiResponse<List<UserEntity>> GetAllUserOfDB()
        {
            try
            {
                var getUsers = _userDbContext.Users.ToList();

                return new ApiResponse<List<UserEntity>>
                {
                    Result = true,
                    Data = getUsers
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserEntity>>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<UserEntity> GetUserByUsernameInDB(string username)
        {
            try
            {
                var findUser = _userDbContext.Users.FirstOrDefault(x => x.Username == username);

                if(findUser!=null)
                {
                    return new ApiResponse<UserEntity>
                    {
                        Result = true,
                        Data = findUser
                    };
                }
                else
                {
                    return new ApiResponse<UserEntity>
                    {
                        Result = false,
                        ErrorMessage = "User Not Found"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public ApiResponse<UserEntity> AddUserInDB(UserEntity userEntity)
        {
            try
            {
                if(userEntity!=null)
                {
                    userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);
                    _userDbContext.Users.Add(userEntity);
                    _userDbContext.SaveChanges();

                    return new ApiResponse<UserEntity>
                    {
                        Result = true,
                        Data = userEntity
                    };
                }
                else
                {
                    return new ApiResponse<UserEntity>
                    {
                        Result = false,
                        ErrorMessage = "Error: The User is null!!!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<UserEntity> UpdateUserInDB(string userName, UserEntity userEntity)
        {
            try
            {
                var findUserInDB = _userDbContext.Users.FirstOrDefault(u => u.Username == userName);

                if (findUserInDB != null)
                {
                    findUserInDB.FirstName = userEntity.FirstName;
                    findUserInDB.LastName = userEntity.LastName;
                    findUserInDB.UserEmail = userEntity.UserEmail;
                    findUserInDB.Mobile = userEntity.Mobile;
                    findUserInDB.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);

                    if(userEntity.Image != null)
                    {
                        if(findUserInDB.Image != null)
                        {
                            //حذف تصویر قبلی
                            var imagePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/UserImages/" + findUserInDB.Image);
                            System.IO.File.Delete(imagePath);
                        }

                        //افزودن تصویر جدید
                        var base64 = userEntity.Image.Split(',')[1];
                        var bytes = System.Convert.FromBase64String(base64);
                        var randName = Guid.NewGuid().ToString();

                        findUserInDB.Image = randName + ".png";

                        var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/UserImages/" + randName + ".png"); // ~ => wwwroot
                        System.IO.File.WriteAllBytes(path, bytes);
                    }

                    _userDbContext.SaveChanges();

                    return new ApiResponse<UserEntity>
                    {
                        Result = true,
                        Data = findUserInDB
                    };
                }
                else
                {
                    return new ApiResponse<UserEntity>
                    {
                        Result = false,
                        ErrorMessage = "The username is not found!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }

        public ApiResponse<UserEntity> DeleteUserByUsernameOfDB(string username)
        {
            try
            {
                var delUser = _userDbContext.Users.FirstOrDefault(x => x.Username == username);
                if (delUser != null)
                {
                    _userDbContext.Users.Remove(delUser);
                    _userDbContext.SaveChanges();

                    return new ApiResponse<UserEntity>
                    {
                        Result = true,
                        Data = delUser
                    };
                }
                else
                {
                    return new ApiResponse<UserEntity>
                    {
                        Result = false,
                        ErrorMessage = "User Not Found"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserEntity>
                {
                    Result = false,
                    ErrorMessage = ex.Message,
                };
            }
        }
    }
}
