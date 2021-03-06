﻿﻿using commercetools.Sdk.Client;
using commercetools.Sdk.Domain.Customers;
using System;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace commercetools.Sdk.HttpApi.IntegrationTests.Customers
{
    public class CustomerFixture : ClientFixture, IDisposable
    {
        public static readonly string Password = "1234";

        public CustomerFixture(ServiceProviderFixture serviceProviderFixture) : base(serviceProviderFixture)
        {
            this.CustomersToDelete = new List<Customer>();
        }

        public List<Customer> CustomersToDelete { get; private set; }

        public void Dispose()
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            this.CustomersToDelete.Reverse();
            foreach (Customer customer in this.CustomersToDelete)
            {
                var deletedType = this.TryDeleteResource(customer).Result;
            }
        }

        public CustomerDraft GetCustomerDraft()
        {
            CustomerDraft customerDraft = new CustomerDraft();
            customerDraft.Email = $"{TestingUtility.RandomString(10)}@email.com";
            customerDraft.Password = Password;
            return customerDraft;
        }

        public Customer CreateCustomer()
        {
            return this.CreateCustomer(this.GetCustomerDraft());
        }

        public Customer CreateCustomer(CustomerDraft customerDraft)
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            CustomerSignInResult customerSignInResult = commerceToolsClient.ExecuteAsync(new SignUpCustomerCommand(customerDraft)).Result as CustomerSignInResult;
            return customerSignInResult.Customer;
        }
    }
}
