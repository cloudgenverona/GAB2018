using System;
using Xamarin.UITest;

namespace GabDemo.Tests.Models
{
    public class Customer
    {
        private IApp app;

        public Customer(IApp app)
        {
            this.app = app;
        }

        public void GoToCustomersPage()
        {
            app.Tap(x => x.Text("Go to tests"));
            app.Screenshot("Customer list");
        }

        public void SearchCustomer(string customerName)
        {
            app.ScrollDownTo(x => x.Text(customerName));
            app.WaitForElement(x => x.Text(customerName),
                               timeoutMessage: "Cannot find the element",
                               timeout: TimeSpan.FromSeconds(120));
            
            app.Flash(x => x.Text(customerName));
            app.Screenshot("Customer selected");
        }

        public void SelectCustomer(string customerName)
        {
            app.Tap(x => x.Text(customerName));
        }

        public void ModifyCustomerDetails(string customerName)
        {
            app.Tap(x => x.Text(customerName));
            app.Tap(x => x.Button("Modify"));
            app.EnterText("FirstName", "Matteo");
            app.EnterText("LastName", "Tumiati");
            app.Tap(x => x.Button("Save"));
        }
    }
}