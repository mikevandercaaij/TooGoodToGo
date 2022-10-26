using Moq;

namespace Core.DomainServices.Tests
{
    public class CanteenEmployeeServiceTests
    {
        [Fact]
        public void AddCanteenEmployee_When_CanteenEmployee_Object_Is_Valid_Return_True()
        {
            //Arrange
            var _canteenEmployeeeRepositoryMock = new Mock<ICanteenEmployeeRepository>(); 

            var sut = new CanteenEmployeeService(_canteenEmployeeeRepositoryMock.Object);

            var canteenEmployee = new CanteenEmployee
            {
                EmployeeId = "12345",
            };
            
            //Act
            var result = sut.AddCanteenEmployeeAsync(canteenEmployee).Result;

            //Assert
            Assert.True(result);
            _canteenEmployeeeRepositoryMock.Verify(x => x.AddCanteenEmployeeAsync(canteenEmployee), Times.Once);
        }

        [Fact]
        public void AddCanteenEmployee_When_CanteenEmployee_Object_Is_Not_Valid_Throw_Corresponding_Exception()
        {
            //Arrange
            var _canteenEmployeeeRepositoryMock = new Mock<ICanteenEmployeeRepository>(); 

            var sut = new CanteenEmployeeService(_canteenEmployeeeRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddCanteenEmployeeAsync(null!)).Result;

            //Assert
            Assert.True(result.Message == "Deze kantinemedewerker bestaat niet!");
        }

        [Fact]
        public void GetCanteenEmployeeById_When_CanteenEmployee_Exists_Return_CanteenEmployee()
        {
            //Arrange
            var _canteenEmployeeeRepositoryMock = new Mock<ICanteenEmployeeRepository>(); 

            var sut = new CanteenEmployeeService(_canteenEmployeeeRepositoryMock.Object);

            var canteenEmployee = new CanteenEmployee
            {
                EmployeeId = "12345",
            };

            _canteenEmployeeeRepositoryMock.Setup(x => x.GetCanteenEmployeeByIdAsync(canteenEmployee.EmployeeId)).ReturnsAsync(canteenEmployee);

            //Act
            var result = sut.GetCanteenEmployeeByIdAsync("12345").Result;
            
            //Assert
            Assert.Equal(canteenEmployee, result);
            _canteenEmployeeeRepositoryMock.Verify(x => x.GetCanteenEmployeeByIdAsync("12345"), Times.AtLeastOnce);
        }

        [Fact]
        public void GetCanteenEmployeeById_When_CanteenEmployee_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _canteenEmployeeeRepositoryMock = new Mock<ICanteenEmployeeRepository>(); 

            var sut = new CanteenEmployeeService(_canteenEmployeeeRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.GetCanteenEmployeeByIdAsync("invalidId")).Result;

            //Assert
            Assert.True(result.Message == "Deze kantinemedewerker bestaat niet!");
        }
    }
}