using Core.Domain.Enums;
using Portal.Models.AccountModels;
using System.ComponentModel.DataAnnotations;

namespace Portal.Tests
{
    public class LoginModelTests
    {
        [Fact]
        public void When_Login_UserId_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var loginModel = new LoginModel()
            {
                UserId = null,
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(loginModel).Any(x => x.ErrorMessage == "Studenten/Personeelsnummer is verplicht!");

            Assert.True(result);
        }

        [Fact]
        public void When_Login_Password_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var loginModel = new LoginModel()
            {
                UserId = "2184147",
                Password = null
            };

            var result = TestHelper.ValidateModel(loginModel).Any(x => x.ErrorMessage == "Wachtwoord is verplicht!");

            Assert.True(result);
        }
    }
}