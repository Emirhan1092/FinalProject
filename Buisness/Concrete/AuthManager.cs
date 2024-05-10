using Buisness.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using Core.Utilities.Security.JWT;
using Entities.DTOs;

namespace Buisness.Concrete
{
    namespace Business.Concrete
    {
        public class AuthManager : IAuthService
        {
            private IUserService _userService;
            private ITokenHelper _tokenHelper;

            public AuthManager(IUserService userService, ITokenHelper tokenHelper)
            {
                _userService = userService;
                _tokenHelper = tokenHelper;
            }

            public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    Email = userForRegisterDto.Email,
                    FirstName = userForRegisterDto.FirstName,
                    LastName = userForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                _userService.Add(user);
                return new SuccesDataResult<User>(user,"Kullanıcı Doğrulandı");
            }

            public IDataResult<User> Login(UserForLoginDto userForLoginDto)
            {
                var userToCheck = _userService.GetByMail(userForLoginDto.Email);
                if (userToCheck == null)
                {
                    return new ErrorDataResult<User>("Kullanıcı Bulunamadı");
                }

                if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
                {
                    return new ErrorDataResult<User>("Şifre Hatası");
                }

                return new SuccesDataResult<User>(userToCheck, "Giriş Başarılı");
            }
            public IResult UserExists(string email)
            {
                var existingUser = _userService.GetByMail(email);
                if (existingUser != null)
                {
                    return new ErrorResult("Kullanıcı Zaten Var");
                }
                return new SuccessResult();
            }



            public IDataResult<AccessToken> CreateAccessToken(User user)
            {
                var claims = _userService.GetClaims(user);
                var accessToken = _tokenHelper.CreateToken(user, claims);
                return new SuccesDataResult<AccessToken>(accessToken, "AccessToken Oluşturuldu");
            }

        }
}
}
