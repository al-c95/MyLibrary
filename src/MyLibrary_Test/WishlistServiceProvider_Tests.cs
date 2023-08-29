using NUnit.Framework;
using MyLibrary;

namespace MyLibrary_Test
{
    [TestFixture]
    class WishlistServiceProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            WishlistServiceProvider provider = new WishlistServiceProvider();

            // act
            var result = provider.Get();

            // assert
            Assert.AreEqual(typeof(WishlistService), result.GetType());
        }
    }//class
}
