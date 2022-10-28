using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Core.Domain.Tests
{
    public class PackageTests
    {
        [Fact]
        public void When_Package_Name_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var packageModel = new Package()
            {
                Name = null,
                PickUpTime = DateTime.Now.AddDays(+1).AddHours(+1),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddHours(+1).AddMinutes(+5),
                Price = 20,
                MealType = MealtypeEnum.Breakfast,
            };

            var result = TestHelper.ValidateModel(packageModel).Any(x => x.ErrorMessage == "Vul een naam voor het pakket in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Package_PickUpTime_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var packageModel = new Package()
            {
                Name = "Pakket",
                PickUpTime = null,
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddHours(+1).AddMinutes(+5),
                Price = 20,
                MealType = MealtypeEnum.Breakfast,
            };

            var result = TestHelper.ValidateModel(packageModel).Any(x => x.ErrorMessage == "Geef aan wanneer het pakket opgehaald moet worden!");

            Assert.True(result);
        }

        [Fact]
        public void When_Package_LatestPickUpTime_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var packageModel = new Package()
            {
                Name = "Pakket",
                PickUpTime = DateTime.Now.AddDays(+1).AddHours(+1),
                LatestPickUpTime = null,
                Price = 20,
                MealType = MealtypeEnum.Breakfast,
            };

            var result = TestHelper.ValidateModel(packageModel).Any(x => x.ErrorMessage == "Geef aan tot wanneer het pakket opgehaald mag worden!");

            Assert.True(result);
        }

        [Fact]
        public void When_Package_Price_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var packageModel = new Package()
            {
                Name = "Pakket",
                PickUpTime = DateTime.Now.AddDays(+1).AddHours(+1),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddHours(+1).AddMinutes(+5),
                Price = null,
                MealType = MealtypeEnum.Breakfast,
            };

            var result = TestHelper.ValidateModel(packageModel).Any(x => x.ErrorMessage == "Geef aan hoe duur het pakket moet worden!");

            Assert.True(result);
        }

        [Fact]
        public void When_Package_MealType_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var packageModel = new Package()
            {
                Name = "Pakket",
                PickUpTime = DateTime.Now.AddDays(+1).AddHours(+1),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddHours(+1).AddMinutes(+5),
                Price = 20,
                MealType = null
            };

            var result = TestHelper.ValidateModel(packageModel).Any(x => x.ErrorMessage == "Geef het type pakket aan!");

            Assert.True(result);
        }
    }
}