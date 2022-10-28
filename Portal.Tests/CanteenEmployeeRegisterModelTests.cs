using Core.Domain.Enums;
using Portal.Models.StudentModels;
using Portal.Models.CanteenModels;
using System.ComponentModel.DataAnnotations;

namespace Portal.Tests
{
    public class CanteenEmployeeRegisterModelTests
    {
        [Fact]
        public void When_CanteenEmployee_FirstName_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteenEmployeeRegisterModel = new CanteenEmployeeRegisterModel()
            {
                FirstName = null,
                LastName = "van der Caaij",
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA,
                Password = "Geheim123"
            };
            
            var result = TestHelper.ValidateModel(canteenEmployeeRegisterModel).Any(x => x.ErrorMessage == "Vul je voornaam in!");

            Assert.True(result);
        }

        [Fact]
        public void When_CanteenEmployee_LastName_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
 
            var canteenEmployeeRegisterModel = new CanteenEmployeeRegisterModel()
            {
                FirstName = "Mike",
                LastName = null,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA,
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(canteenEmployeeRegisterModel).Any(x => x.ErrorMessage == "Vul je achternaam in!");

            Assert.True(result);
        }

        [Fact]
        public void When_CanteenEmployee_EmployeeId_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteenEmployeeRegisterModel = new CanteenEmployeeRegisterModel()
            {
                FirstName = "Mike",
                LastName = "van der Caaij",
                EmployeeId = null,
                Location = CanteenLocationEnum.LA,
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(canteenEmployeeRegisterModel).Any(x => x.ErrorMessage == "Vul je personeelsnummer in!");

            Assert.True(result);
        }

        [Fact]
        public void When_CanteenEmployee_Location_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteenEmployeeRegisterModel = new CanteenEmployeeRegisterModel()
            {
                FirstName = "Mike",
                LastName = "van der Caaij",
                EmployeeId = "12345",
                Location = null,
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(canteenEmployeeRegisterModel).Any(x => x.ErrorMessage == "Vul een locatie in!");

            Assert.True(result);
        }
        
        [Fact]
        public void When_CanteenEmployee_Password_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteenEmployeeRegisterModel = new CanteenEmployeeRegisterModel()
            {
                FirstName = "Mike",
                LastName = "van der Caaij",
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA,
                Password = null
            };

            var result = TestHelper.ValidateModel(canteenEmployeeRegisterModel).Any(x => x.ErrorMessage == "Vul je wachtwoord in!");

            Assert.True(result);
        }
    }
}