using Core.Domain.Entities;

namespace Core.DomainServices.Tests
{
    public class PackageServiceTests
    {
        //CREATE
        //:::::::::::::::::::::::::::::::

        [Fact]
        public void Create_When_There_Arent_Any_Products_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            var list = new List<string>();

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(null!, list, "1")).Result;

            //Assert
            Assert.True(result.Message == "Selecteer minimaal ??n product!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_PickUpTime_In_The_Past_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd moet in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_PickUpTime_More_Than_Two_Days_Away_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+3)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd mag niet meer dan 2 dagen in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_Latest_PickUpTime_Is_In_The_Past_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1),
                LatestPickUpTime = DateTime.Now.AddDays(-1)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De uiterlijke ophaaltijd moet in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_Latest_PickUpTime_Is_Before_The_First_PickUpTime_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+1)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De uiterlijke ophaaltijd moet na de ophaaltijd plaatsvinden!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_Latest_PickUpTime_Is_Not_On_The_Same_Day_As_The_First_PickUpTime_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1),
                LatestPickUpTime = DateTime.Now.AddDays(+2)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd en uiterlijke ophaaltijd moeten op dezelfde dag zijn!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_Canteen_Not_Allowed_To_Serve_Warm_Meals_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddMinutes(15),
                MealType = MealtypeEnum.WarmDinner
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = false
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddPackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "Jouw kantine serveert geen warme maaltijden!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Create_When_Package_Is_Valid_Return_True()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddMinutes(15),
                MealType = MealtypeEnum.WarmDinner
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            var product = new Product
            {
                ProductId = 1,
                Name = "Product1",
                ContainsAlcohol = false
            };

            var selectedItems = new List<String> { "Product1" };

            _productServiceMock.Setup(x => x.GetProductByNameAsync("Product1")).ReturnsAsync(product);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = sut.AddPackageAsync(package, selectedItems, "1").Result;

            //Assert
            Assert.True(result);
        }

        //Update
        //:::::::::::::::::::::::::::::::

        [Fact]
        public void Update_When_There_Arent_Any_Products_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            var list = new List<string>();

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(null!, list, "1")).Result;

            //Assert
            Assert.True(result.Message == "Selecteer minimaal ??n product!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_PickUpTime_In_The_Past_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd moet in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_PickUpTime_More_Than_Two_Days_Away_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+3)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd mag niet meer dan 2 dagen in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_Latest_PickUpTime_Is_In_The_Past_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1),
                LatestPickUpTime = DateTime.Now.AddDays(-1)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De uiterlijke ophaaltijd moet in de toekomst liggen!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_Latest_PickUpTime_Is_Before_The_First_PickUpTime_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+1)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De uiterlijke ophaaltijd moet na de ophaaltijd plaatsvinden!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_Latest_PickUpTime_Is_Not_On_The_Same_Day_As_The_First_PickUpTime_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1),
                LatestPickUpTime = DateTime.Now.AddDays(+2)
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "De ophaaltijd en uiterlijke ophaaltijd moeten op dezelfde dag zijn!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        [Fact]
        public void Update_When_Canteen_Not_Allowed_To_Serve_Warm_Meals_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddMinutes(15),
                MealType = MealtypeEnum.WarmDinner
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = false
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = Record.ExceptionAsync(() => sut.UpdatePackageAsync(package, null!, "1")).Result;

            //Assert
            Assert.True(result.Message == "Jouw kantine serveert geen warme maaltijden!");
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("1"), Times.Once);
            _canteenServiceMock.Verify(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA), Times.Once());
        }

        //DELETE
        //:::::::::::::::::::::::::::::::
        [Fact]
        public void Delete_When_Package_Is_Reserved_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+2),
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    Location = CanteenLocationEnum.LA
                },
                ReservedBy = student
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("12345")).ReturnsAsync(canteenEmployee);


            //Act
            var result = Record.ExceptionAsync(() => sut.DeletePackageAsync(2, "12345")).Result;

            //Assert
            Assert.True(result.Message == "De maaltijd is al gereserveerd en mag dan ook niet worden verwijderd!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("12345"), Times.Once);
        }

        [Fact]
        public void Delete_When_Package_is_From_Other_Canteen_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LD
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+2),
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    Location = CanteenLocationEnum.LA
                },
                ReservedBy = student
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("12345")).ReturnsAsync(canteenEmployee);


            //Act
            var result = Record.ExceptionAsync(() => sut.DeletePackageAsync(2, "12345")).Result;

            //Assert
            Assert.True(result.Message == "Het is niet toegestaan om pakketten van andere kantines te verwijderen!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("12345"), Times.Once);
        }

        [Fact]
        public void Delete_When_Package_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+2),
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    Location = CanteenLocationEnum.LA
                },
                ReservedBy = student
            };

            //Act
            var result = Record.ExceptionAsync(() => sut.DeletePackageAsync(3, "12345")).Result;

            //Assert
            Assert.True(result.Message == "Het pakket bestaat niet!");
        }

        [Fact]
        public void Delete_When_Package_Is_Found_And_Not_Reserve_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+2),
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    Location = CanteenLocationEnum.LA
                },
                ReservedBy = null
            };

            //Act
            var result = Record.ExceptionAsync(() => sut.DeletePackageAsync(2, "12345")).Result;

            //Assert
            Assert.True(result.Message == "Het pakket bestaat niet!");
        }

        [Fact]
        public void Delete_When_Package_Is_Found_And_Not_Reserved_Return_True()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                LatestPickUpTime = DateTime.Now.AddDays(+2),
                Canteen = new Canteen()
                {
                    CanteenId = 1,
                    Location = CanteenLocationEnum.LA
                },
                ReservedBy = null
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("12345")).ReturnsAsync(canteenEmployee);

            //Act
            var result = sut.DeletePackageAsync(2, "12345").Result;

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void Update_When_Package_Is_Valid_Return_True()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package
            {
                PackageId = 1,
                PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                LatestPickUpTime = DateTime.Now.AddDays(+1).AddMinutes(15),
                MealType = MealtypeEnum.WarmDinner
            };

            var canteen = new Canteen()
            {
                Location = CanteenLocationEnum.LA,
                ServesWarmMeals = true
            };

            var user = new CanteenEmployee()
            {
                EmployeeId = "1",
                Location = CanteenLocationEnum.LA,
            };

            var product = new Product
            {
                ProductId = 1,
                Name = "Product1",
                ContainsAlcohol = false
            };

            var selectedItems = new List<String> { "Product1" };

            _productServiceMock.Setup(x => x.GetProductByNameAsync("Product1")).ReturnsAsync(product);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("1")).ReturnsAsync(user);
            _canteenServiceMock.Setup(x => x.GetCanteenByLocationAsync(CanteenLocationEnum.LA)).ReturnsAsync(canteen);

            //Act
            var result = sut.UpdatePackageAsync(package, selectedItems, "1").Result;

            //Assert
            Assert.True(result);
        }

        //Reserve
        //:::::::::::::::::::::::::::::::

        [Fact]
        public void Reserve_When_Already_Ordered_A_Package_On_Reservation_Date_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var user = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+1),
                ReservedBy = null
            };

            var reservations = new List<Package>
            {
                new Package
                {
                    PackageId = 1,
                    PickUpTime = DateTime.Now.AddDays(+1),
                    ReservedBy = user
                },
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147")).ReturnsAsync(user);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync()).ReturnsAsync(reservations);

            //Act
            var result = Record.ExceptionAsync(() => sut.ReservePackageAsync(2, "2184147")).Result;

            //Assert
            Assert.True(result.Message == "Je hebt al een pakket gereserveerd op deze ophaaldag!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _studentServiceMock.Verify(x => x.GetStudentByIdAsync("2184147"), Times.AtLeastOnce);
            _packageRepositoryMock.Verify(x => x.GetAllPackagesAsync(), Times.Once());
        }

        [Fact]
        public void Reserve_When_Student_Is_Too_Young_To_Reserve_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var user = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                IsAdult = true,
                ReservedBy = null
            };

            var reservations = new List<Package>
            {
                new Package
                {
                    PackageId = 1,
                    PickUpTime = DateTime.Now.AddDays(+1),
                    ReservedBy = user
                },
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147")).ReturnsAsync(user);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync()).ReturnsAsync(reservations);

            //Act
            var result = Record.ExceptionAsync(() => sut.ReservePackageAsync(2, "2184147")).Result;

            //Assert
            Assert.True(result.Message == "Je bent nog geen 18 jaar oud en mag dus geen pakketten met alcoholische inhoud reserveren!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _studentServiceMock.Verify(x => x.GetStudentByIdAsync("2184147"), Times.AtLeastOnce);
        }

        [Fact]
        public void Reserve_When_Account_Has_Not_Been_Found_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var user = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+1),
                ReservedBy = null
            };

            var reservations = new List<Package>
            {
                new Package
                {
                    PackageId = 1,
                    PickUpTime = DateTime.Now.AddDays(+1),
                    ReservedBy = user
                },
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync()).ReturnsAsync(reservations);

            //Act
            var result = Record.ExceptionAsync(() => sut.ReservePackageAsync(2, "invalidID")).Result;

            //Assert
            Assert.True(result.Message == "Je account is niet geldig!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _studentServiceMock.Verify(x => x.GetStudentByIdAsync("invalidID"), Times.Once);
        }

        [Fact]
        public void Reserve_When_Package_Is_Already_Reserved_By_Someone_Else_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var user = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                ReservedBy = user
            };

            var reservations = new List<Package>
            {
                new Package
                {
                    PackageId = 1,
                    PickUpTime = DateTime.Now.AddDays(+1),
                    ReservedBy = user
                },
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147")).ReturnsAsync(user);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync()).ReturnsAsync(reservations);

            //Act
            var result = Record.ExceptionAsync(() => sut.ReservePackageAsync(2, "2184147")).Result;

            //Assert
            Assert.True(result.Message == "Het pakket bestaat niet of is al gereserveerd door iemand anders!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
        }

        [Fact]
        public void Reserve_When_Reservation_Is_Successful_Return_True()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var user = new Student()
            {
                StudentId = 1,
                StudentNumber = "2184147",
                DateOfBirth = DateTime.Now
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+2),
                ReservedBy = null
            };

            var reservations = new List<Package>
            {
                new Package
                {
                    PackageId = 1,
                    PickUpTime = DateTime.Now.AddDays(+1),
                    ReservedBy = user
                },
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147")).ReturnsAsync(user);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync()).ReturnsAsync(reservations);

            //Act
            var result = sut.ReservePackageAsync(2, "2184147").Result;

            //Assert
            Assert.True(result);
        }

        //Get Update
        //:::::::::::::::::::::::::::::::
        [Fact]
        public void ValidateGetPackage_When_Package_Is_Already_Reserved_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+1),
                Canteen = new Canteen
                {

                    Location = CanteenLocationEnum.LA,
                },
                ReservedBy = student
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("12345")).ReturnsAsync(canteenEmployee);

            //Act
            var result = Record.ExceptionAsync(() => sut.ValidateGetUpdatePackage(2, "12345")).Result;


            //Assert
            Assert.True(result.Message == "De maaltijd is al gereserveerd en mag dan ook niet worden bewerkt!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("12345"), Times.Once);
        }

        [Fact]
        public void ValidateGetPackage_When_Package_Isnt_From_Own_Canteen_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LD
            };

            var package = new Package
            {
                PackageId = 2,
                PickUpTime = DateTime.Now.AddDays(+1),
                Canteen = new Canteen
                {

                    Location = CanteenLocationEnum.LA,
                },
                ReservedBy = student
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(2)).ReturnsAsync(package);
            _canteenEmployeeServiceMock.Setup(x => x.GetCanteenEmployeeByIdAsync("12345")).ReturnsAsync(canteenEmployee);

            //Act
            var result = Record.ExceptionAsync(() => sut.ValidateGetUpdatePackage(2, "12345")).Result;

            //Assert
            Assert.True(result.Message == "Het is niet toegestaan om pakketten van andere kantines te bewerken!");
            _packageRepositoryMock.Verify(x => x.GetPackageByIdAsync(2), Times.Once);
            _canteenEmployeeServiceMock.Verify(x => x.GetCanteenEmployeeByIdAsync("12345"), Times.Once);
        }

        [Fact]
        public void ValidateGetPackage_When_Package_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student()
            {
                StudentId = 1,
                DateOfBirth = DateTime.Now
            };

            var canteenEmployee = new CanteenEmployee()
            {
                CanteenEmployeeId = 1,
                EmployeeId = "12345",
                Location = CanteenLocationEnum.LA
            };

            //Act
            var result = Record.ExceptionAsync(() => sut.ValidateGetUpdatePackage(3, "12345")).Result;


            //Assert
            Assert.True(result.Message == "Het pakket bestaat niet!");
        }

        //READ FUNCTIONS TESTS
        //:::::::::::::::::::::::::::::::

        //Get all packages | return 2 packages
        [Fact]
        public async Task Get_All_Packages_Should_Return_2_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>
            {
                new Package {PackageId = 1},
                new Package {PackageId = 2}
            };

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = await sut.GetAllPackagesAsync();

            //Assert
            Assert.Equal(packages, result);
            Assert.Equal(2, result.Count());
            Assert.Equal(1, result.First().PackageId);
        }

        //Get all packages | return 0 packages
        [Fact]
        public async Task Get_All_Packages_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>();

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = await sut.GetAllPackagesAsync();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Get_Package_By_Id_Should_Return_Null()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            //Act
            var result = sut.GetPackageByIdAsync(1).Result;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Get_Package_By_Id_Should_Return_Package()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var package = new Package()
            {
                PackageId = 1
            };

            _packageRepositoryMock.Setup(x => x.GetPackageByIdAsync(1)).ReturnsAsync(package);

            //Act
            var result = sut.GetPackageByIdAsync(1).Result;

            //Assert
            Assert.Equal(result, package);
        }

        //get all students reservations | return 3
        [Fact]
        public void Get_Reservations_From_Student_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddMinutes(10),
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    ReservedBy = student
                },
            };

            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147").Result).Returns(student);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllReservationsFromStudentAsync("2184147").Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all students reservations | return 0
        [Fact]
        public void Get_Reservations_From_Student_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>();

            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147").Result).Returns(student);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllReservationsFromStudentAsync("2184147").Result;

            //Assert
            Assert.Empty(result);
        }

        //get all students active reservations | return 3
        [Fact]
        public void Get_Active_Reservations_From_Student_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddMinutes(15),
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+1).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+1).AddMinutes(15),
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    ReservedBy = student
                },
            };

            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147").Result).Returns(student);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllActiveReservationsFromStudentAsync("2184147").Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all students active reservations | return 0
        [Fact]
        public void Get_Active_Reservations_From_Student_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>();

            _studentServiceMock.Setup(x => x.GetStudentByIdAsync("2184147").Result).Returns(student);
            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllActiveReservationsFromStudentAsync("2184147").Result;

            //Assert
            Assert.Empty(result);
        }

        //get all packages from canteen | return 3
        [Fact]
        public void Get_All_Packages_From_Canteen_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                },
            };

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllPackagesFromCanteenAsync(CanteenLocationEnum.LA).Result;
                
            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all packages from canteen | return 0
        [Fact]
        public void Get_All_Packages_From_Canteen_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>();

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllPackagesFromCanteenAsync(CanteenLocationEnum.LA).Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all packages from other canteens | return 3

        [Fact]
        public void Get_All_Packages_From_Other_Canteens_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LD
                    },
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LD
                    },
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LD
                    },
                },
            };

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllPackagesFromOtherCanteensAsync(CanteenLocationEnum.LA).Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all packages from other canteens | return 0
        [Fact]
        public void Get_All_Packages_From_Other_Canteens_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>();

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllPackagesFromOtherCanteensAsync(CanteenLocationEnum.LA).Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all canteens active reservations | return 3
        [Fact]
        public void Get_All_Active_Packages_From_Canteen_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                    ReservedBy = student
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    Canteen = new Canteen
                    {
                        Location = CanteenLocationEnum.LA
                    },
                    ReservedBy = student
                },
            };

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllActivePackagesFromCanteenAsync(CanteenLocationEnum.LA).Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all canteens active reservations | return 0
        [Fact]
        public void Get_All_Active_Packages_From_Canteen_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            var packages = new List<Package>();

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllActivePackagesFromCanteenAsync(CanteenLocationEnum.LA).Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all offered packages | return 3
        [Fact]
        public void Get_All_Offered_Packages_Should_Return_3_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>()
            {
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    ReservedBy = null
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    ReservedBy = null
                },
                new Package
                {
                    PickUpTime = DateTime.Now.AddDays(+2).AddMinutes(10),
                    LatestPickUpTime = DateTime.Now.AddDays(+2).AddMinutes(15),
                    ReservedBy = null
                },
            };

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllOfferedPackagesAsync().Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }

        //get all offered packages | return 0
        [Fact]
        public void Get_All_Offered_Packages_Should_Return_0_Packages()
        {
            //Arrange
            var _packageRepositoryMock = new Mock<IPackageRepository>();
            var _canteenEmployeeServiceMock = new Mock<ICanteenEmployeeService>();
            var _canteenServiceMock = new Mock<ICanteenService>();
            var _productServiceMock = new Mock<IProductService>();
            var _studentServiceMock = new Mock<IStudentService>();

            var sut = new PackageService(_packageRepositoryMock.Object, _canteenEmployeeServiceMock.Object, _canteenServiceMock.Object, _productServiceMock.Object, _studentServiceMock.Object);

            var packages = new List<Package>();

            _packageRepositoryMock.Setup(x => x.GetAllPackagesAsync().Result).Returns(packages);

            //Act
            var result = sut.GetAllOfferedPackagesAsync().Result;

            //Assert
            Assert.Equal(packages.Count, result.Count());
        }
    }
}