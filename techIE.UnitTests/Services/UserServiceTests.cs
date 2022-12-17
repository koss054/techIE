#nullable disable
namespace techIE.UnitTests.Services
{
    using System.Threading.Tasks;

    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Services;

    using techIE.UnitTests.Data;

    [TestFixture]
    public class UserServiceTests
    {
        private AppDbContext context;
        private IUserService userService;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();

            userService = new UserService(context);
        }
        
        [Test]
        public async Task Test_GetUserAsync_ValidId()
        {
            // Arrange
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var expectedUserName = "firstUser";

            // Act
            var user = await userService.GetUserAsync(userId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUserName, user.UserName);
        }

        [Test]
        public async Task Test_GetUserAsync_InvalidId()
        {
            // Arrange
            var userId = "invalid-user-id-guid-maaaan";

            // Act
            var user = await userService.GetUserAsync(userId);

            // Assert
            Assert.IsNull(user);
        }

        [Test]
        public async Task Test_GetUserByProductIdAsync_ValidProduct()
        {
            // Arrange
            var expectedUserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var productId = 1;

            // Act
            var user = await userService.GetUserByProductIdAsync(productId);

            // Assert
            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUserId, user.Id);
        }

        [Test]
        public async Task Test_GetUserByProductIdAsync_InvalidProduct()
        {
            // Arrange
            var productId = 999;

            // Act
            var user = await userService.GetUserByProductIdAsync(productId);

            // Assert
            Assert.IsNull(user);
        }
    }
}
