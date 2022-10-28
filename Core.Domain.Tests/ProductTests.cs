using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Core.Domain.Tests
{
    public class ProductTests
    {
        [Fact]
        public void When_Product_Name_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var product = new Product()
            {
                Name = null,
                Picture = new byte[1],
            };

            var result = TestHelper.ValidateModel(product).Any(x => x.ErrorMessage == "Vul een naam voor het pakket in!");

            Assert.True(result);
        }

        [Fact]
        public void When_Product_Picture_Is_Not_Filled_In_Throw_Corresponding_Exception()
        {
            var product = new Product()
            {
                Name = "Product",
                Picture = null
            };

            var result = TestHelper.ValidateModel(product).Any(x => x.ErrorMessage == "Je moet een foto van het product meegeven!");

            Assert.True(result);
        }
    }
}