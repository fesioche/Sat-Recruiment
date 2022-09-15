using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Entity;
using Xunit;
using Sat.Recruitment.Api.Business;

namespace Sat.Recruitment.Test
{
	[CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserControllerTest
    {
        private readonly UsersController _userController;

        //Use it if need to mock. Install Moq Nuget
        //For this technical test I do not UnitTest, only Integration Test
        //private readonly Mock<IUserBusiness> _userBusiness;

        public UserControllerTest()
        {
            _userController = new UsersController(new UserBusiness());
            //_userBusiness = new Mock<IUserBusiness>();
            //_userController = new UsersController(_userBusiness.Object);
        }

        [Trait("Category", "UserDuplicated" )]
        [Fact]
        public void UserIsNotDuplicatedWhenTryToCreate_ResultIsSuccess()
        {
            //arrange
            User userTest = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);
            
            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.True(result.IsSuccess);
        }

        [Trait("Category", "UserDuplicated")]
        [Fact]
        public void UserIsNotDuplicatedWhenTryToCreate_MeesageReturned()
        {
            //arrange
            User userTest = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal("User Created", result.Errors);
        }

        [Trait("Category", "UserDuplicated")]
        [Fact]
        public void CheckUserIsDuplicatedWhenTryToCreate_ResultIsFalse()
        {
            //arrange
            User userTest = new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.False(result.IsSuccess);
        }

        [Trait("Category", "UserDuplicated")]
        [Fact]
        public void CheckUserIsDuplicatedWhenTryToCreate_MessageReturned()
        {
            //arrange
            User userTest = new User("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Trait("Category", "UserDuplicated")]
        [Fact]
        public void CheckUserIsDuplicatedWhenTryToCreate_ByNameAndAddress()
        {
            //arrange
            User userTest = new User("Franco", "farnquito@gmail.com", "Alvear y Colombres", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal("The user is duplicated", result.Errors);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_NameIsNULL_ResultIsFalse()
        {
            //arrange
            User userTest = new User(null, "farnquito@gmail.com", "Alvear y Colombres", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.False(result.IsSuccess);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_NameIsNULL_MessageReturned()
        {
            //arrange
            User userTest = new User(null, "farnquito@gmail.com", "Alvear y Colombres", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal("The name is required", result.Errors);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_EmailIsNULL_ResultIsFalse()
        {
            //arrange
            User userTest = new User("Franco", null, "Alvear y Colombres", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.False(result.IsSuccess);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_EmailIsNULL_MessageReturned()
        {
            //arrange
            User userTest = new User("Franco", null, "Alvear y Colombres", "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal(" The email is required", result.Errors);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_AddressIsNULL_MessageReturned()
        {
            //arrange
            User userTest = new User("Franco", "farnquito@gmail.com", null, "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal(" The address is required", result.Errors);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_AddressIsNULL_ResultIsFalse()
        {
            //arrange
            User userTest = new User("Franco", "farnquito@gmail.com", null, "+349 1122354215", "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.False(result.IsSuccess);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_PhoneIsNULL_ResultIsFalse()
        {
            //arrange
            User userTest = new User("Franco", "farnquito@gmail.com", "Alvear y Colombres", null, "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.False(result.IsSuccess);
        }

        [Trait("Category", "ValidateInputWhenTryToCreate")]
        [Fact]
        public void ValidateInputWhenTryToCreate_PhoneIsNULL_MessageReturned()
        {
            //arrange
            User userTest = new User("Franco", "farnquito@gmail.com", "Alvear y Colombres", null, "Normal", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.Equal(" The phone is required", result.Errors);
        }


        [Trait("Category", "Money")]
        [Fact]
        public void MoneyBetween10And100AndUserNormal_ReturnSuccess()
        {
            //arrange
            User userTest = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", 85);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.True(result.IsSuccess);
        }

        [Trait("Category", "Money")]
        [Fact]
        public void MoneyGreatherThan100AndUserSuperUser_ReturnSuccess()
        {
            //arrange
            User userTest = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "SuperUser", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.True(result.IsSuccess);
        }

        [Trait("Category", "Money")]
        [Fact]
        public void MoneyGreatherThan100AndUserPremium_ReturnSuccess()
        {
            //arrange
            User userTest = new User("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Premium", 124);

            //act
            var result = _userController.Create(userTest).Result;

            // assert
            Assert.True(result.IsSuccess);
        }
    }
}
