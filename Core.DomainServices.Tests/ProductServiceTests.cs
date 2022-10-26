using Microsoft.AspNetCore.Mvc.Rendering;

namespace Core.DomainServices.Tests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetAllProducts_When_3_Products_Are_Found_Return_List_With_Three()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>(); 

            var sut = new ProductService(_productRepositoryMock.Object);

            var products = new List<Product>()
            {
                new Product { ProductId = 1, Name = "Product 1" },
                new Product { ProductId = 2, Name = "Product 2" },
                new Product { ProductId = 3, Name = "Product 3" }
            };

            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            //Act
            var result = sut.GetAllProductsAsync().Result;

            //Assert
            Assert.Equal(3, result.Count());
            _productRepositoryMock.Verify(x => x.GetAllProductsAsync(), Times.Once);
        }

        [Fact]
        public void GetAllProducts_When_No_Products_Are_Found_Return_Empty_List()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            var products = new List<Product>();

            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            //Act
            var result = sut.GetAllProductsAsync().Result;

            //Assert
            Assert.Empty(products);
            _productRepositoryMock.Verify(x => x.GetAllProductsAsync(), Times.Once);
        }

        [Fact]
        public void GetProductById_When_Product_Exists_Return_Product()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();
            
            var sut = new ProductService(_productRepositoryMock.Object);

            var product = new Product
            {
                ProductId = 1,
            };

            _productRepositoryMock.Setup(x => x.GetProductByIdAsync(1)).ReturnsAsync(product);

            //Act
            var result = sut.GetProductByIdAsync(1).Result;

            //Assert
            Assert.Equal(product, result);
            _productRepositoryMock.Verify(x => x.GetProductByIdAsync(1), Times.AtLeastOnce);
        }

        [Fact]
        public void GetProductById_When_Product_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.GetProductByIdAsync(0)).Result;

            //Assert
            Assert.True(result.Message == "Dit product bestaat niet!");
        }

        [Fact]
        public void GetProductByName_When_Product_Exists_Return_Product()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            var product = new Product
            {
                Name = "Product"
            };

            _productRepositoryMock.Setup(x => x.GetProductByNameAsync("Product")).ReturnsAsync(product);

            //Act
            var result = sut.GetProductByNameAsync("Product").Result;

            //Assert
            Assert.Equal(product, result);
            _productRepositoryMock.Verify(x => x.GetProductByNameAsync("Product"), Times.AtLeastOnce);
        }

        [Fact]
        public void GetProductByName_When_Product_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.GetProductByNameAsync("Product")).Result;

            //Assert
            Assert.True(result.Message == "Dit product bestaat niet!");
        }

        [Fact]
        public void GetAllSelectListItems_When_3_SelectItems_Are_Found_Return_List_With_Three()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            var products = new List<Product>()
            {
                new Product { ProductId = 1, Name = "Product 1" },
                new Product { ProductId = 2, Name = "Product 2" },
                new Product { ProductId = 3, Name = "Product 3" }
            };

            var SelectItems = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "Product 1" },
                new SelectListItem { Value = "2", Text = "Product 2" },
                new SelectListItem { Value = "3", Text = "Product 3" }
            };

            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            //Act
            var result = sut.GetAllSelectListItems().Result;

            //Assert
            Assert.Equal(SelectItems.Count, result.Count);
            Assert.Equal(SelectItems[0].Text, result[0].Text);
            Assert.Equal(SelectItems[1].Text, result[1].Text);
            Assert.Equal(SelectItems[2].Text, result[2].Text);
            _productRepositoryMock.Verify(x => x.GetAllProductsAsync(), Times.Once);
        }

        [Fact]
        public void GetAllSelectListItems_When_No_SelectItems_Are_Found_Return_Empty_List()
        {
            //Arrange
            var _productRepositoryMock = new Mock<IProductRepository>();

            var sut = new ProductService(_productRepositoryMock.Object);

            var products = new List<Product>();

            _productRepositoryMock.Setup(x => x.GetAllProductsAsync()).ReturnsAsync(products);

            //Act
            var result = sut.GetAllSelectListItems().Result;

            //Assert
            Assert.Empty(new List<SelectListItem>());
            _productRepositoryMock.Verify(x => x.GetAllProductsAsync(), Times.Once);
        }
    }
}