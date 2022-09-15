using Sat.Recruitment.Api.Entity;
using System.Collections.Generic;

namespace Sat.Recruitment.Api.Business
{
	public interface IUserBusiness
	{
		public void NormalizeUser(ref User userToBeNormalized);

		public void FillUserLists(ref IList<User> users);

		public Result GetResponse(bool isDuplicated);

		public decimal CalculateMoney(decimal money, double param1);

		public bool IsUserDuplicated(IList<User> users, User newUser);

		public void UpdateMoney(ref User newUser);

		public string ValidateErrors(User user);
	}
}
