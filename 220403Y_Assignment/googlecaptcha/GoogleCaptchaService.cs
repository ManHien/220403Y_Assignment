﻿using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace _220403Y_Assignment.googlecaptcha
{
	public class GoogleCaptchaService
	{
		private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;

		public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config)
		{
			_config = config;
		}

		public async Task<bool> ValidateToken(string token)
		{
			try
			{
				var url = $"https://www.google.com/recaptcha/api/siteverify?secret={_config.CurrentValue.SecretKey}&response={token}";
				using (var client = new HttpClient())
				{
					var httpResult = await client.GetAsync(url);
					if (httpResult.StatusCode != HttpStatusCode.OK)
					{
						return false;
					}
					var responseString = await httpResult.Content.ReadAsStringAsync();
					var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);
					return googleResult.success && googleResult.score >= 0.5;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
