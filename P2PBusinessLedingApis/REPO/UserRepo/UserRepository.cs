using Microsoft.EntityFrameworkCore;
using P2PBusinessLedingApis.DAL.Models.Users;
using P2PBusinessLedingApis.DAL.Models.Users.RequestObj;
using P2PBusinessLedingApis.DAL.Models.Users.ResponseObj;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace P2PBusinessLedingApis.REPO.UserRepo
{
    public class UserRepository
    {


        public CreateUserResult CreateUser(CreateUserParam param, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            using (var _context = new P2PContext())
                try
                {
                    var userIpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
                    var hashedPassword = BCrypt.Net.BCrypt.HashPassword(param.Password);

                    var newUser = new tbl_users
                    {
                        UserId = param.UserId,
                        Username = param.Username,
                        Email = param.Email,
                        Password = hashedPassword,
                        PhoneNumber = param.PhoneNumber,
                        Roles = param.Roles,
                        BVN=param.BVN,
                        FirstName=param.FirstName,
                        LastName=param.LastName,
                        IPAddress= userIpAddress

                    };

                    if (!_context.tbl_users.Any(u => u.UserId == param.UserId))
                    {


                        _context.tbl_users.Add(newUser);
                        _context.SaveChanges();

                        return new CreateUserResult
                        {
                            IsSuccessful = true,
                            Code = "00",
                            Message = "User Created Successfully",
                        };
                    }
                    else
                    {
                        return new CreateUserResult
                        {
                            IsSuccessful = false,
                            Code = "11",
                            Message = "User Exists Already",
                        };
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return new CreateUserResult
                    {
                        IsSuccessful = false,
                        Code = "-1000",
                        Message = ex.Message,
                    };
                }
        }


        public GetAllUserData GetAllUsers()
        {
            using (var _context = new P2PContext())
            {
                GetAllUserData gtedata = new GetAllUserData();

                try
                {
                    var usersInDb = _context.tbl_users.ToList();

                    if (usersInDb != null && usersInDb.Any())
                    {
                        gtedata.IsSuccessful = true;
                        gtedata.Code = "00";
                        gtedata.Message = "User Retrieval successful";
                        gtedata.UsersData = usersInDb;
                    }
                    else
                    {
                        gtedata.IsSuccessful = false;
                        gtedata.Code = "-11";
                        gtedata.Message = "No Users Exist to be retrieved";
                    }
                }
                catch (Exception ex)
                {
                    gtedata.IsSuccessful = false;
                    gtedata.Code = "-1000";
                    gtedata.Message = ex.Message;
                }

                return gtedata;
            }
        }

        public ValidateLoginResult ValidateLogin(LoginRequestModel requestModel, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            using (var _context = new P2PContext())
            {
                try
                {
                    var userIpAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

                    var user = _context.tbl_users.SingleOrDefault(u => u.Username == requestModel.userId);

                    if (user != null && BCrypt.Net.BCrypt.Verify(requestModel.password, user.Password))
                    {
                        // Password is correct

                        // You can generate a JWT token here and return it in the result
                        var token = GenerateJwtToken(user);

                        return new ValidateLoginResult
                        {
                            IsSuccessful = true,
                            Code = "00",
                            Message = "Login Successful",
                            Token = token
                        };
                    }
                    else
                    {
                        // Invalid credentials
                        return new ValidateLoginResult
                        {
                            IsSuccessful = false,
                            Code = "11",
                            Message = "Username and password do not match",
                            Token = null
                        };
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return new ValidateLoginResult
                    {
                        IsSuccessful = false,
                        Code = "-1000",
                        Message = ex.Message,
                        Token = null
                    };
                }
            }
        }

        private string GenerateJwtToken(tbl_users user)
        {
            // Your JWT token generation logic here

            // Example using System.IdentityModel.Tokens.Jwt:
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new byte[32]; // 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId),
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
