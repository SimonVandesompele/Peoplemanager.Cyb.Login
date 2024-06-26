﻿using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PeopleManager.RestApi.Settings;
using PeopleManager.Services.Model.Requests;
using Vives.Security.Model;
using Vives.Services.Model;
using Vives.Services.Model.Enums;

namespace PeopleManager.RestApi.Services
{
    public class IdentityService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityService(JwtSettings jwtSettings, UserManager<IdentityUser> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }

        public async Task<JwtAuthenticationResult> SignIn(UserSignInRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return LoginFailed();
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return LoginFailed();
            }

            if (string.IsNullOrWhiteSpace(_jwtSettings.Secret) 
                || !_jwtSettings.Expiry.HasValue)
            {
                return JwtConfigurationError();
            }
            var token = GenerateJwtToken(user, _jwtSettings.Secret, _jwtSettings.Expiry.Value);

            return new JwtAuthenticationResult()
            {
                Token = token
            };
        }

        private JwtAuthenticationResult LoginFailed()
        {
            return new JwtAuthenticationResult()
            {
                Messages = new List<ServiceMessage>()
                {
                    new ServiceMessage()
                    {
                        Code = "LoginFailed",
                        Message = "User/Password combination is incorrect.",
                        Type = ServiceMessageType.Error
                    }
                }
            };
        }

        private JwtAuthenticationResult JwtConfigurationError()
        {
            return new JwtAuthenticationResult()
            {
                Messages = new List<ServiceMessage>()
                {
                    new ServiceMessage()
                    {
                        Code = "JwtConfigurationError",
                        Message = "JWT Settings are not configured correctly",
                        Type = ServiceMessageType.Error
                    }
                }
            };
        }

        public async Task<JwtAuthenticationResult> Register(UserRegisterRequest request)
        {

        }

        private string GenerateJwtToken(IdentityUser user, string secret, TimeSpan expiry)
        {
            // Now its ime to define the jwt token which will be responsible for creating our tokens
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            
            // We get our secret from the AppSettings
            var key = Encoding.ASCII.GetBytes(secret);

            //Define claims
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                
                // the JTI is used for our refresh token which we will be converting in the next video
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Email));
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }

            // we define our token descriptor
            // We need to utilise claims which are properties in our token which gives information about the token
            // which belong to the specific user who it belongs to
            // ,so it could contain their id, name, email the good part is that this information was
            // generated by our server and identity framework which is valid and trusted
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // the life span of the token needs to be shorter and utilise refresh token to keep the user signedin
                // but since this is a demo app we can extend it to fit our current need
                Expires = DateTime.UtcNow.Add(expiry),
                // here we are adding the encryption algorithm information which will be used to decrypt our token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
