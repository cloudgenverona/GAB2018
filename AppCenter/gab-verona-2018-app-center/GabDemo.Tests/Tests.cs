using GabDemo.Tests.Models;
using NUnit.Framework;
using Xamarin.UITest;

namespace GabDemo.Tests
{
    [TestFixture(Platform.Android)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void FindCustomerInList()
        {
            var customer = new Customer(app);

            customer.GoToCustomersPage();
            customer.SearchCustomer("Matteo Tumiati");
            customer.SelectCustomer("Matteo Tumiati");
        }

        [Test]
        public void ModifyCustomerDetails()
        {
            var customer = new Customer(app);

            customer.GoToCustomersPage();
            customer.SearchCustomer("Matteo Tumiati");
            customer.ModifyCustomerDetails("Matteo Tumiati");
        }
    }
}