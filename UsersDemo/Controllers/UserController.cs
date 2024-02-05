using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersDemo.Application;
using UsersDemo.DTO.InternalApi.Request;

namespace UsersDemo.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UserController(IMapper mapper, IUserRepo userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }

        [HttpGet]
        public ActionResult<List<DTO.ExternalApi.Response.UserResponse>> GetAllUser()
        {
            try
            {
                var getUsers = _userRepo.GetAllUser();

                if (getUsers.Result)
                {
                    var mapUsersExternal = _mapper.Map<List<DTO.ExternalApi.Response.UserResponse>>(getUsers.Data);

                    return Ok(mapUsersExternal);
                }
                else
                {
                    return BadRequest(getUsers.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<DTO.ExternalApi.Response.UserResponse> GetUserByUsername(string username)
        {
            try
            {
                var findUser = _userRepo.GetUserByUsername(username);

                if (findUser.Result)
                {
                    var mapUserExternal = _mapper.Map<DTO.ExternalApi.Response.UserResponse>(findUser.Data);
                    return Ok(mapUserExternal);
                }
                else
                {
                    return BadRequest(findUser.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<DTO.ExternalApi.Response.UserResponse> AddUser(DTO.ExternalApi.Request.UserRequest userRequest)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var mapUserInternal = _mapper.Map<UserRequest>(userRequest);
                    var addResult = _userRepo.AddUser(mapUserInternal);

                    if (addResult.Result)
                    {
                        var mapUserExternal = _mapper.Map<DTO.ExternalApi.Response.UserResponse>(addResult.Data);

                        return Ok(mapUserExternal);
                    }
                    else
                    {
                        return BadRequest(addResult.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<DTO.ExternalApi.Response.UserResponse> UpdateUser(string username, DTO.ExternalApi.Request.UserRequest userRequest)
        {
            try
            {
                if (username == userRequest.Username)
                {
                    var mapUserInternal = _mapper.Map<UserRequest>(userRequest);
                    var updatedUser = _userRepo.UpdateUser(username, mapUserInternal);

                    if(updatedUser.Result)
                    {
                        var mapUserExternal = _mapper.Map<DTO.ExternalApi.Response.UserResponse>(updatedUser.Data);
                        return Ok(mapUserExternal);
                    }
                    else
                    {
                        return BadRequest(updatedUser.ErrorMessage);
                    }
                }
                else
                {
                    return BadRequest("The Username is not equal with Username field");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<DTO.ExternalApi.Response.UserResponse> DeleteUserByUsername(string username)
        {
            try
            {
                var delUser = _userRepo.DeleteUserByUsername(username);
                if (delUser.Result)
                {
                    var mapUserExternal = _mapper.Map<DTO.ExternalApi.Response.UserResponse>(delUser.Data);
                    return Ok(mapUserExternal);
                }
                else
                {
                    return BadRequest(delUser.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
