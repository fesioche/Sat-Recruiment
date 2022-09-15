using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Business;
using Sat.Recruitment.Api.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public partial class UsersController : ControllerBase
	{
		private IList<User> _users = new List<User>();
		private readonly IUserBusiness _userBusiness;
		public UsersController(IUserBusiness userBusiness)
		{
			_userBusiness = userBusiness;
		}

		[HttpPost]
		[Route("/create-user")]
		public Task<Result> Create(User newUser)
		{
			try
			{
				var errors = _userBusiness.ValidateErrors(newUser);

				if (errors != null && errors != "")
					return Task.FromResult(new Result()
					{
						IsSuccess = false,
						Errors = errors
					});

				_userBusiness.NormalizeUser(ref newUser);
				_userBusiness.FillUserLists(ref _users);
				var isDuplicated = _userBusiness.IsUserDuplicated(_users, newUser);

				return Task.FromResult(_userBusiness.GetResponse(isDuplicated));
			}
			catch(Exception ex)
			{
				return Task.FromResult(new Result()
				{
					IsSuccess = false,
					Errors = ex.Message
				});
			}
		}

		
	}
}
