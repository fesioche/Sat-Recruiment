using Sat.Recruitment.Api.Entity;
using Sat.Recruitment.Api.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Api.Business
{
	public class UserBusiness : IUserBusiness
	{
		public void NormalizeUser(ref User userToBeNormalized)
		{
			UpdateMoney(ref userToBeNormalized);
			userToBeNormalized.Email = Utilities.NormalizeEmail(userToBeNormalized.Email);
		}

		public void FillUserLists(ref IList<User> users)
		{
			using var reader = Utilities.GetFileStream(Directory.GetCurrentDirectory() + "/Files/Users.txt");

			while (reader.Peek() >= 0)
			{
				var line = reader.ReadLineAsync().Result;
				var user = new User
				{
					Name = line.Split(',')[0].ToString(),
					Email = line.Split(',')[1].ToString(),
					Phone = line.Split(',')[2].ToString(),
					Address = line.Split(',')[3].ToString(),
					UserType = line.Split(',')[4].ToString(),
					Money = decimal.Parse(line.Split(',')[5].ToString()),
				};
				users.Add(user);
			}
			reader.Close();
		}

		public Result GetResponse(bool isDuplicated)
		{
			if (!isDuplicated)
			{
				Debug.WriteLine("User Created");

				return new Result()
				{
					IsSuccess = true,
					Errors = "User Created"
				};
			}
			else
			{
				Debug.WriteLine("The user is duplicated");

				return new Result()
				{
					IsSuccess = false,
					Errors = "The user is duplicated"
				};
			}
		}

		public decimal CalculateMoney(decimal money, double param1)
		{
			var percentage = Convert.ToDecimal(param1);
			return money * percentage;
		}

		public bool IsUserDuplicated(IList<User> users, User newUser)
		{
			foreach (var user in users)
			{
				if (user.Email == newUser.Email
					||
					user.Phone == newUser.Phone)
				{
					return true;
				}
				else if (user.Name == newUser.Name && user.Address == newUser.Address)
				{
					return true;
				}
			}

			return false;
		}

		public void UpdateMoney(ref User newUser)
		{
			decimal moneyToAdd = 0;
			switch (newUser.UserType)
			{
				case "Normal":
					if (newUser.Money > 100)
					{
						moneyToAdd = CalculateMoney(newUser.Money, 0.12);
					}
					if (newUser.Money > 10 && newUser.Money < 100)
					{
						moneyToAdd = CalculateMoney(newUser.Money, 0.8);
					}
					break;
				case "SuperUser":
					if (newUser.Money > 100)
					{
						moneyToAdd = CalculateMoney(newUser.Money, 0.20);
					}
					break;
				case "Premium":
					if (newUser.Money > 100)
					{
						moneyToAdd = newUser.Money * 2;
					}
					break;
			}

			newUser.Money += moneyToAdd;
		}

		//Validate errors
		public string ValidateErrors(User user)
		{
			StringBuilder aux = new StringBuilder();
			if (user.Name == null)
				aux.Append("The name is required");
			if (user.Email == null)
				aux.Append(" The email is required");
			if (user.Address == null)
				aux.Append(" The address is required");
			if (user.Phone == null)
				aux.Append(" The phone is required");

			return aux.ToString();
		}

	}
}
