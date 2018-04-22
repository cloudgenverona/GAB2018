using System;
using System.Collections.Generic;
using GabDemo.Models;
using System.Linq;

using Xamarin.Forms;

namespace GabDemo
{
    public partial class TestPage : ContentPage
    {
        List<Customer> customerList;

        public TestPage()
        {
            InitializeComponent();

            if (customerList == null || !customerList.Any())
            {
                customerList = new List<Customer>();

                customerList.Add(new Customer { FirstName = "Melissa", LastName = "Cook" });
                customerList.Add(new Customer { FirstName = "Louise", LastName = "Garcia" });
                customerList.Add(new Customer { FirstName = "Kathryn", LastName = "Bailey" });
                customerList.Add(new Customer { FirstName = "Peter", LastName = "Washington" });
                customerList.Add(new Customer { FirstName = "Eugene", LastName = "Murphy" });
                customerList.Add(new Customer { FirstName = "Carolyn", LastName = "Sanders" });
                customerList.Add(new Customer { FirstName = "Ralph", LastName = "Reed" });
                customerList.Add(new Customer { FirstName = "Barbara", LastName = "Powell" });
                customerList.Add(new Customer { FirstName = "Nicole", LastName = "Brown" });
                customerList.Add(new Customer { FirstName = "Earl", LastName = "Gray" });
                customerList.Add(new Customer { FirstName = "Kathy", LastName = "Mitchell" });
                customerList.Add(new Customer { FirstName = "Sandra", LastName = "Stewart" });
                customerList.Add(new Customer { FirstName = "Charles", LastName = "Peterson" });
                customerList.Add(new Customer { FirstName = "Jeremy", LastName = "Young" });
                customerList.Add(new Customer { FirstName = "Kelly", LastName = "Ramirez" });
                customerList.Add(new Customer { FirstName = "Wanda", LastName = "Clark" });
                customerList.Add(new Customer { FirstName = "Raymond", LastName = "Kelly" });
                customerList.Add(new Customer { FirstName = "Norma", LastName = "Taylor" });
                customerList.Add(new Customer { FirstName = "Lisa", LastName = "White" });
                customerList.Add(new Customer { FirstName = "Matteo", LastName = "Tumiati" });
                customerList.Add(new Customer { FirstName = "Jerry", LastName = "Cox" });
                customerList.Add(new Customer { FirstName = "Harold", LastName = "Roberts" });
                customerList.Add(new Customer { FirstName = "Michael", LastName = "Thomas" });
                customerList.Add(new Customer { FirstName = "Wayne", LastName = "Jones" });
                customerList.Add(new Customer { FirstName = "Joe", LastName = " Torres" });
                customerList.Add(new Customer { FirstName = "Joseph", LastName = "Long" });
            }
        }

		protected override void OnAppearing()
		{
            CustomerList.ItemsSource = customerList;
		}
	}
}