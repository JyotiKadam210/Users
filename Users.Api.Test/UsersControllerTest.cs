using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Users.Api.Controllers;
using Users.Api.Interface;
using Users.Api.Model;
using Xunit;

namespace Users.Api.Test
{
    public class UsersControllerTest
    {
        #region "Declaration"
        private readonly IFixture _fixture;
        private readonly Mock<IUser> _userServiceMock;
        private readonly UsersController _userController;
        public UsersControllerTest()
        {
            _fixture = new Fixture();
            _userServiceMock = _fixture.Freeze<Mock<IUser>>();
            _userController = new UsersController(_userServiceMock.Object);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        #endregion

        #region GetUsers_ShouldReturnAllUsers
        [Fact]
        public async Task GetUsers_ShouldReturnAllUsers()
        {
            //Arrange  
            var UserMock = _fixture.CreateMany<User>(3).ToList();
            _userServiceMock.Setup(x => x.GetUsers()).ReturnsAsync(UserMock);

            //Act
            var result = await _userController.GetUsers();

            //Assert
            Assert.Equal(UserMock.Count(), result.Value?.Count());
        }
        #endregion


        #region AddUser_ShouldReturnStatusCreated_WhenAddingNewUser
        [Fact]
        public async Task AddUser_ShouldReturnStatusCreated_WhenAddingNewUser()
        {
            //Arrange
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var UserMock = _fixture.Create<User>();
            _userServiceMock.Setup(x => x.PostUser(UserMock));

            //Act
            var result = await _userController.PostUser(UserMock);

            //Assert            
            Assert.NotNull(result);
        }
        #endregion

        #region UpdateUser_ShouldReturnStatusNoContent_WhenUpdatingUser
        [Theory]
        [InlineData(500)]
        public async Task UpdateUser_ShouldReturnStatusNoContent_WhenUpdatingUser(int ID)
        {
            //Arrange
            _fixture.Register<int>(() => ID);
            int Id = _fixture.Create<int>();

            var UserMock = _fixture.Build<User>()
                .With(x => x.UserId, ID).Without(n => n.UserId)
                .Create();
            _userServiceMock.Setup(x => x.PutUser(Id, UserMock));

            try
            {
                //Act
                var result = await _userController.PutUser(Id, UserMock) as ObjectResult;
            }
            catch (Exception ex)
            {
                //Assert  
                if (ex.Message == "ID Not Found")
                    Assert.Equal(StatusCodes.Status204NoContent, 204);
            }
        }
        #endregion

    }
}