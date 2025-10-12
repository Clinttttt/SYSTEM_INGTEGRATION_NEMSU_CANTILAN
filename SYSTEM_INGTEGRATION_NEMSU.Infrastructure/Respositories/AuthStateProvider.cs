using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;
using SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Respositories
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localstorage;
        private readonly IAuthApiServices _authApiServices;
        public AuthStateProvider(ProtectedLocalStorage localstorage, IAuthApiServices authApiServices)
        {
            _localstorage = localstorage;
            _authApiServices = authApiServices;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var results = await _localstorage.GetAsync<string>("AccessToken");
                var token = results.Success ? results.Value : null;
                if (string.IsNullOrWhiteSpace(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                }
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var claims = new List<Claim>();
                foreach (var claim in jwt.Claims)
                {
                    if(claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                    {

                        claims.Add(new Claim(ClaimTypes.Name, claim.Value));
                    }
                    else if(claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                    {
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, claim.Value));
                    }
                    else
                    {
                        claims.Add(new Claim(claim.Type, claim.Value));
                    }
                }
                var identity = new ClaimsIdentity(claims, "jwt", ClaimTypes.Name, ClaimTypes.NameIdentifier);
                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);

            }catch 
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        public async Task LogoutAsync()
        {
            try
            {

                var tokenResult = await _localstorage.GetAsync<string>("AccessToken");
                var token = tokenResult.Success ? tokenResult.Value : null;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    await _authApiServices.LogoutAsync(token);
                }


                await _localstorage.DeleteAsync("AccessToken");
                await _localstorage.DeleteAsync("RefreshToken");
            }
            catch
            {

                await _localstorage.DeleteAsync("AccessToken");
                await _localstorage.DeleteAsync("RefreshToken");
                NotifyUserChanged();
            }
        }

        public void NotifyUserChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

