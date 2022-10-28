namespace Core.Domain.Tests
{
    public class CanteenTests
    {
        [Fact]
        public void When_Canteen_City_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteen = new Canteen()
            {
                City = null,
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var result = TestHelper.ValidateModel(canteen).Any(x => x.ErrorMessage == "Geef aan in welke stad de kantine is!");

            Assert.True(result);
        }

        [Fact]
        public void When_Canteen_Location_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteen = new Canteen()
            {
                City = CityEnum.Breda,
                Location = null,
                ServesWarmMeals = true
            };

            var result = TestHelper.ValidateModel(canteen).Any(x => x.ErrorMessage == "Vul een locatie in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Canteen_ServesWarmMeals_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var canteen = new Canteen()
            {
                City = CityEnum.Breda,
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = null
            };

            var result = TestHelper.ValidateModel(canteen).Any(x => x.ErrorMessage == "Geef aan of de kantine warme maaltijden bereid!");

            Assert.True(result);
        }
    }
}