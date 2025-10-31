using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SYSTEM_INGTEGRATION_NEMSU.Application.External;

namespace SYSTEM_INGTEGRATION_NEMSU.Infrastructure.Services
{
    public class AuthHelper : IAuthHelper
    {
        private readonly HttpClient _http;
        private readonly ProtectedLocalStorage _localstorage;
        private readonly IAuthApiServices _authApi;
        public AuthHelper(HttpClient http, ProtectedLocalStorage localStorage, IAuthApiServices authApi)
        {
            _http = http;
            _localstorage = localStorage;
            _authApi = authApi;
        }
        public async Task SetAuthHeaderAsync(HttpClient client)
        {
            string? results = null;

            var token = await _localstorage.GetAsync<string>("AccessToken");
            results = token.Success ? token.Value : null;

            if (string.IsNullOrEmpty(results))
            {
                var refresh = await _authApi.TryRefreshTokenAsync();
                if (refresh)
                {
                    var set = await _localstorage.GetAsync<string>("AccessToken");
                    results = set.Success ? set.Value : null;
                }
            }
            if (!string.IsNullOrEmpty(results))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", results);
            }
        }


    }
}
