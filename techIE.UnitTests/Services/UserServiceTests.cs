namespace techIE.UnitTests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Data.Entities;
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
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var expectedUserName = "firstUser";

            var user = await userService.GetUserAsync(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUserName, user.UserName);
        }

        [Test]
        public async Task Test_GetUserAsync_InvalidId()
        {
            var userId = "invalid-user-id-guid-maaaan";

            var user = await userService.GetUserAsync(userId);

            Assert.IsNull(user);
        }

        [Test]
        public async Task Test_GetUserByProductIdAsync_ValidProduct()
        {
            var expectedUserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var productId = 1;

            var user = await userService.GetUserByProductIdAsync(productId);

            Assert.IsNotNull(user);
            Assert.AreEqual(expectedUserId, user.Id);
        }

        [Test]
        public async Task Test_GetUserByProductIdAsync_InvalidProduct()
        {
            var productId = 999;

            var user = await userService.GetUserByProductIdAsync(productId);

            Assert.IsNull(user);
        }
    }
}
