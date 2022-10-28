using Core.Domain.Enums;
using Portal.ExtensionMethods;
using Portal.Models.StudentModels;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Xml.Linq;
using Xunit.Sdk;

namespace Portal.Tests
{
    public class StudentRegisterModelTests
    {
        [Fact]
        public void When_Student_FirstName_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = null,
                LastName = "van der Caaij",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je voornaam in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_LastName_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = null,
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je achternaam in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_DateOfBirth_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = null,
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je geboortedatum in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_Is_Younger_Than_16_Years_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "van der Caaij",
                DateOfBirth = DateTime.Now.AddYears(-15),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Je moet minimaal 16 jaar zijn om te registeren als student!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_StudentNumber_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = null,
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je studentennummer in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_EmailAddress_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = null,
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je email in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_StudyCity_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = null,
                PhoneNumber = "0638719633",
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Geef aan in welke stad je studeert!");

            Assert.True(result);
        }
        
        [Fact]
        public void When_Student_PhoneNumber_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = null,
                Password = "Geheim123"
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je telefoonnummer in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Student_Password_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var studentRegisterModel = new StudentRegisterModel()
            {
                FirstName = "Mike",
                LastName = "",
                DateOfBirth = new DateTime(2000, 7, 18),
                StudentNumber = "2184147",
                EmailAddress = "m.vandercaaij@student.avans.nl",
                StudyCity = CityEnum.Breda,
                PhoneNumber = "0638719633",
                Password = null
            };

            var result = TestHelper.ValidateModel(studentRegisterModel).Any(x => x.ErrorMessage == "Vul je wachtwoord in!");

            Assert.True(result);
        }
    }
}