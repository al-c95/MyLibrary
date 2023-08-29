using NUnit.Framework;
using MyLibrary;
using MyLibrary.Presenters.ServiceProviders;

namespace MyLibrary_Test.Presenters_Tests.ServiceProviders_Tests
{
    [TestFixture]
    public class AuthorServiceProvider_Tests
    {
        [Test]
        public void Get_Test()
        {
            // arrange
            AuthorServiceProvider provider = new AuthorServiceProvider();

            // act
            var result = provider.Get();

            // assert
            Assert.IsTrue(result.GetType() == typeof(AuthorService));
        }
    }//class
}
