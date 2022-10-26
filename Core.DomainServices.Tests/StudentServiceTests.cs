namespace Core.DomainServices.Tests
{
    public class StudentServiceTests
    {
        [Fact]
        public void AddStudent_When_Student_Object_Is_Valid_Return_True()
        {
            //Arrange
            var _studentRepositoryMock = new Mock<IStudentRepository>(); 

            var sut = new StudentService(_studentRepositoryMock.Object);

            var student = new Student
            {
                StudentId = 1,
                StudentNumber = "2184147",
            };
            
            //Act
            var result = sut.AddStudentAsync(student).Result;

            //Assert
            Assert.True(result);
            _studentRepositoryMock.Verify(x => x.AddStudentAsync(student), Times.Once);
        }

        [Fact]
        public void AddStudent_When_Student_Object_Is_Not_Valid_Throw_Corresponding_Exception()
        {
            //Arrange
            var _studentRepositoryMock = new Mock<IStudentRepository>(); 

            var sut = new StudentService(_studentRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.AddStudentAsync(null!)).Result;

            //Assert
            Assert.True(result.Message == "Deze student bestaat niet!");
        }

        [Fact]
        public void GetStudentById_When_Student_Exists_Return_Student()
        {
            //Arrange
            var _studentRepositoryMock = new Mock<IStudentRepository>(); 

            var sut = new StudentService(_studentRepositoryMock.Object);

            var student = new Student
            {
                StudentNumber = "2184147",
            };

            _studentRepositoryMock.Setup(x => x.GetStudentByIdAsync(student.StudentNumber)).ReturnsAsync(student);

            //Act
            var result = sut.GetStudentByIdAsync("2184147").Result;
            
            //Assert
            Assert.Equal(student, result);
            _studentRepositoryMock.Verify(x => x.GetStudentByIdAsync("2184147"), Times.AtLeastOnce);
        }

        [Fact]
        public void GetStudentById_When_Student_Doesnt_Exist_Throw_Corresponding_Exception()
        {
            //Arrange
            var _studentRepositoryMock = new Mock<IStudentRepository>(); 

            var sut = new StudentService(_studentRepositoryMock.Object);

            //Act
            var result = Record.ExceptionAsync(() => sut.GetStudentByIdAsync("invalidId")).Result;

            //Assert
            Assert.True(result.Message == "Deze student bestaat niet!");
        }
    }
}