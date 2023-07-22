using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Users.Api.Model;
using Users.Api.Service;
using Xunit;

namespace Users.Api.Test
{
public class UserServiceTest
    {
        #region "Declaration"
        private IFixture _fixture;
        protected readonly UserDbContext _dbContext;
       
        public UserServiceTest()
        {
            _fixture = new Fixture();
            var options = new DbContextOptionsBuilder<UserDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            _dbContext = new UserDbContext(options);
              _dbContext.Database.EnsureCreated();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        #endregion

        #region ShouldReturnOKStatusCode_WhenDataFound_GetAllUsers
        [Fact]
        public async Task ShouldReturnOKStatusCode_WhenUserDataFound()
        {
            /// Arrange
            var mockUser = _fixture.CreateMany<User>(5).ToList();
            _dbContext.Users.AddRange(mockUser);
            await _dbContext.SaveChangesAsync();
            var _userService = new UserService(_dbContext);

            /// Act
            var result = await _userService.GetUsers();

            /// Assert                       

            Assert.NotNull(result);
            Assert.Equal(mockUser.Count(), result?.Value.Count());
            Assert.Equal(StatusCodes.Status200OK, 200);
        }
        #endregion

        #region ShouldReturerror_WhenEmailidIsnotInCorrectFormat_UserAdd
        [Fact]
        public async Task ShouldReturerror_WhenEmailidIsnotInCorrectFormat_UserAdd()
        {
            /// Arrange
            var UserMock = _fixture.Create<User>();

            var _userService = new UserService(_dbContext);

            try
            {
                /// Act
                await _userService.PostUser(UserMock);

            }
            catch (Exception ex)
            {
                ///Assert
                Assert.Equal("Invalid email id..", ex.Message);
                    
            }            
          
        }
        #endregion

        #region ShouldReturnStatusNoContent_WhenUpdatingUser_WhichIsNotAvailable
        [Theory]
        [InlineData(500)]
        public async Task ShouldReturnStatusNoContent_WhenUpdatingUser_WhichIsNotAvailable(int ID)
        {
            _fixture.Register<int>(() => ID);
            int Id = _fixture.Create<int>();

            var MockUser = _fixture.Build<User>()
                .With(x => x.UserId, Id).Without(n => n.UserId)
                .Create();

            _dbContext.Users.AddRange(MockUser);
            await _dbContext.SaveChangesAsync();

            var _userService = new UserService(_dbContext);

            try
            {
                //Act
                await _userService.PutUser(Id, MockUser);
            }
            catch (Exception)
            {
                //Assert  
                Assert.Equal(StatusCodes.Status204NoContent, 204);
            }
        }
        #endregion

        #region "Dispose"
        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
        #endregion
    }
}
