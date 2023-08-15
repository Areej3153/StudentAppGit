using Api.Data;
using Api.DTOs.Account;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTService _jwtService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IFileService _uploadService;
        private readonly Context _context;
        private readonly IWebHostEnvironment environment;

        

        public AccountController(Context context, IWebHostEnvironment environment, JWTService jwtService, SignInManager<User> signInManager, UserManager<User> userManager, IFileService uploadService)
        {
            _jwtService = jwtService;
            _signInManager = signInManager;
            _userManager = userManager;
            _uploadService = uploadService;
            _context = context;
            this.environment = environment;
        }

        [HttpPost("register-avatar-n")]
        public async Task<IActionResult> RegisterAvatar(IFormFile fromFile, [FromForm] RegisterDto registerDto)
        {
            if (fromFile == null)
            {
                return BadRequest();
            }

           // APIResponse respoonse = new APIResponse()


            try
            {
                //var fileId = await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);
                string Filepath = this.environment.WebRootPath + "\\Upload";
                if(!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                string fileName = DateTime.Now.Ticks.ToString() + ".jpg";

                string imagepath = Filepath + "\\" + fileName;
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }

                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await fromFile.CopyToAsync(stream);
                }

                var userToAdd = new User
                {
                    FirstName = registerDto.FirstName.ToLower(),
                    LastName = registerDto.LastName.ToLower(),
                    UserName = registerDto.Email.ToLower(),
                    Email = registerDto.Email.ToLower(),
                    EmailConfirmed = true,
                    BirthDate = registerDto.BirthDate,
                    photoId = 47,
                    photoPath = fileName,
                    // Photo = model.Photo
                };

                var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);
                if (!result.Succeeded) {
                    return BadRequest(result.Errors);
                 }
                else
                {
                    var user = await _userManager.FindByEmailAsync(registerDto.Email);
                    return Ok(user.Id);
                }
                
               

            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("loginn")]
        private async Task<ActionResult<UserDto>> Loginn(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized("Invalid username or password");

            if (user.EmailConfirmed == false) return Unauthorized("Please confirm your email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid username or password");

            return await CreateApplicationUserDto(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null) return Unauthorized("Invalid username or password");

            if (user.EmailConfirmed == false) return Unauthorized("Please confirm your email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid username or password");

            return await CreateApplicationUserDto(user);
        }

        [HttpPost("register-avatar")]
        private async Task<ActionResult> RegisterAvatar([FromForm] FileUploadModel fileDetails, [FromForm] RegisterDto registerDto)
        {
            if(fileDetails == null)
            {
                return BadRequest();
            }

            try
            {
                var fileId = await _uploadService.PostFileAsync(fileDetails.FileDetails, fileDetails.FileType);

                var userToAdd = new User
                {
                    FirstName = registerDto.FirstName.ToLower(),
                    LastName = registerDto.LastName.ToLower(),
                    UserName = registerDto.Email.ToLower(),
                    Email = registerDto.Email.ToLower(),
                    EmailConfirmed = true,
                    BirthDate = registerDto.BirthDate,
                    photoId = fileId,
                    // Photo = model.Photo
                };

                var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);
                if (!result.Succeeded) return BadRequest(result.Errors);
                return Ok("sucess");

            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("register-avatar-no")]
        private async Task<ActionResult> RegisterAvatarNo([FromForm] RegisterDto registerDto)
        {
            
            try
            {
              

                var userToAdd = new User
                {
                    FirstName = registerDto.FirstName.ToLower(),
                    LastName = registerDto.LastName.ToLower(),
                    UserName = registerDto.Email.ToLower(),
                    Email = registerDto.Email.ToLower(),
                    EmailConfirmed = true,
                    BirthDate = registerDto.BirthDate,
                    photoId = 5,
                    // Photo = model.Photo
                };

                var result = await _userManager.CreateAsync(userToAdd, registerDto.Password);
                if (!result.Succeeded) return BadRequest(result.Errors);
                return Ok("sucess");

            }
            catch (Exception)
            { throw; }
        }

        [HttpPost("register")]
        private async Task<IActionResult> Register(RegisterDto model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An exsisting account is using {model.Email}, email address. Please try with another email address");

            }

            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true,
                BirthDate = model.BirthDate,
                //photoId = model.photoId,
               // Photo = model.Photo
            };

            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok("Your account has been created, you can login");
        }

        #region Private Helper Methods
        private async Task<UserDto> CreateApplicationUserDto(User user)
        {
            string Imageurl = string.Empty;
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = this.environment.WebRootPath + "\\Upload";
                string imagepath = Filepath + "\\" + user.photoPath;
                if (System.IO.File.Exists(imagepath))
                {
                    Imageurl = hosturl + "/Upload/" + user.photoPath ;
                }
               

            }
            catch(Exception ex)
            {
                
            }

            //var graduate = await _context.Graduate.FirstOrDefaultAsync(x => x.Id == user.id);
            var graduate = await _context.Graduate.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (graduate == null)
            {
                return null;
            }





            

            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                BirthDate = user.BirthDate.ToString("yyyy-MM-dd"),
                //photoId = user.photoId,
                photoPath = Imageurl,
                //Photo = _uploadService.GetAsync(user.photoId),
                JWT = _jwtService.CreateJWT(user),
                Graduate = graduate,
            };

        }

        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
        #endregion
    }
}
