using Microsoft.AspNetCore.Mvc;
using P2PBusinessLedingApis.DAL.Models.Users.RequestObj;
using P2PBusinessLedingApis.DAL.Models.Users.ResponseObj;
using P2PBusinessLedingApis.REPO.UserRepo;

namespace P2PBusinessLedingApis.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly P2PContext _context;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IConfiguration config, P2PContext context, ILogger<UsersController> logger)
        {
            _config = config;
            _context = context;
            _logger = logger;
        }

        UserRepository userCrud = new UserRepository();  // Create an instance of UserCrud

        [HttpGet("Users/GetAllUsers")]
        [Produces("application/json")]
        public GetAllUsersResponse GetAllUsers()
        {
            GetAllUserData gtdrmncy = userCrud.GetAllUsers();
            return new GetAllUsersResponse { ResponseCode = gtdrmncy.Code, ResponseDescription = gtdrmncy.Message, Users = gtdrmncy.UsersData };
            
        }

        [HttpPost("Users/CreateUser")]
        [Produces("application/json")]
        public CreateDeleteUserRessponse CreateUser([FromBody] CreateUserParam param, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            CreateUserResult crtdrmncy = userCrud.CreateUser(param,httpContextAccessor);
            return new CreateDeleteUserRessponse { ResponseCode = crtdrmncy.Code, ResponseDescription = crtdrmncy.Message };
        }

        [HttpPost("login")]
        public ActionResult<ValidateLoginResult> ValidateLogin(LoginRequestModel loginModel, [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                // Call your validation method in the repository
                var validationResult = userCrud.ValidateLogin(loginModel, httpContextAccessor);

                // Check the validation result and return the appropriate response
                if (validationResult.IsSuccessful)
                {
                    return Ok(validationResult); // Include token in response
                }
                else
                {
                    return BadRequest(validationResult);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, new ValidateLoginResult
                {
                    IsSuccessful = false,
                    Code = "-1000",
                    Message = ex.Message
                });
            }
        }
    }
}

