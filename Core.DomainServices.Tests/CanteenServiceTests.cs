namespace Core.DomainServices.Tests
{
    public class CanteenServiceTests
    {
        [Fact]
        public void GetCanteenByLocation_When_Canteen_Exists_Return_Canteen()
        {
            //Arrange
            var _canteenRepositoryMock = new Mock<ICanteenRepository>(); 

            var sut = new CanteenService(_canteenRepositoryMock.Object);

            var canteen = new Canteen
            {
                CanteenId = 1,
                Location = CanteenLocationEnum.LA,
            };

            _canteenRepositoryMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);
            
            //Act
            var result = sut.GetCanteenByLocationAsync(CanteenLocationEnum.LA).Result;
            
            //Assert
            Assert.Equal(canteen, result);
            _canteenRepositoryMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once);
        }

        //hierna product

        [Fact]
        public void GetCanteenByLocation_When_Canteen_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _canteenRepositoryMock = new Mock<ICanteenRepository>();

            var sut = new CanteenService(_canteenRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).Result;

            //Assert
            Assert.True(result.Message == "Deze kantine bestaat niet!");
        }
    }
}