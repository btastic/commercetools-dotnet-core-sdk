﻿﻿using System;
using System.Collections.Generic;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.ProductDiscounts;
using commercetools.Sdk.HttpApi.IntegrationTests.Products;
using Xunit.Abstractions;

namespace commercetools.Sdk.HttpApi.IntegrationTests.ProductDiscounts
{
    public class ProductDiscountsFixture : ClientFixture, IDisposable
    {
        private readonly ProductTypeFixture productTypeFixture;
        private readonly ProductFixture productFixture;
        public List<ProductDiscount> ProductDiscountsToDelete { get; }

        public ProductDiscountsFixture(ServiceProviderFixture serviceProviderFixture) : base(serviceProviderFixture)
        {
            this.ProductDiscountsToDelete = new List<ProductDiscount>();
            this.productTypeFixture = new ProductTypeFixture(serviceProviderFixture);
            this.productFixture = new ProductFixture(serviceProviderFixture);
        }

        public void Dispose()
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            this.ProductDiscountsToDelete.Reverse();
            foreach (ProductDiscount productDiscount in this.ProductDiscountsToDelete)
            {
                var deletedType = this.TryDeleteResource(productDiscount).Result;
            }
            this.productFixture.Dispose();
            this.productTypeFixture.Dispose();
        }

        /// <summary>
        /// Create Product Discount for specific product - put the product Id inside predicate of the product discount
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ProductDiscountDraft GetProductDiscountDraft(string productId)
        {
            string predicate = this.GetProductDiscountPredicateBasedonProduct(productId);
            ProductDiscountDraft productDiscountDraft = new ProductDiscountDraft();
            productDiscountDraft.Name = new LocalizedString() {{"en", TestingUtility.RandomString(10)}};
            productDiscountDraft.Description = new LocalizedString() {{"en", TestingUtility.RandomString(20)}};
            productDiscountDraft.Value = GetProductDiscountValueAsAbsolute();
            productDiscountDraft.Predicate = predicate;
            productDiscountDraft.SortOrder = TestingUtility.RandomSortOrder();
            productDiscountDraft.ValidFrom = DateTime.Today;
            productDiscountDraft.ValidUntil = DateTime.Today.AddMonths(1);
            productDiscountDraft.IsActive = true;
            productDiscountDraft.SetPredicate(product => product.ProductId() == productId);

            return productDiscountDraft;
        }

        /// <summary>
        /// Create Product Discount for specific Product, so Create Product first and Create Product Discount based on this product
        /// </summary>
        /// <returns>Product Discount</returns>
        public ProductDiscount CreateProductDiscount()
        {
            Product product = this.productFixture.CreateProduct();
            this.productFixture.ProductsToDelete.Add(product);
            return this.CreateProductDiscount(this.GetProductDiscountDraft(product.Id));
        }

        /// <summary>
        /// Create Product Discount for the passed product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public ProductDiscount CreateProductDiscount(Product product)
        {
            return this.CreateProductDiscount(this.GetProductDiscountDraft(product.Id));
        }

        public ProductDiscount CreateProductDiscount(ProductDiscountDraft productDiscountDraft)
        {
            IClient commerceToolsClient = this.GetService<IClient>();
            ProductDiscount productDiscount = commerceToolsClient
                .ExecuteAsync(new CreateCommand<ProductDiscount>(productDiscountDraft)).Result;
            return productDiscount;
        }

        /// <summary>
        /// Return Relative Product Discount
        /// </summary>
        /// <returns></returns>
        public RelativeProductDiscountValue GetProductDiscountValueAsRelative()
        {
            var productDiscountValue = new RelativeProductDiscountValue()
            {
                Permyriad = TestingUtility.RandomInt(1, 30)
            };
            return productDiscountValue;
        }

        /// <summary>
        /// Return Absolute Product Discount
        /// </summary>
        /// <returns></returns>
        public AbsoluteProductDiscountValue GetProductDiscountValueAsAbsolute()
        {
            var money = new Money()
            {
                CurrencyCode = "EUR",
                CentAmount = TestingUtility.RandomInt(1, 10000)
            };
            var productDiscountValue = new AbsoluteProductDiscountValue()
            {
                Money = new List<Money>() {money}
            };
            return productDiscountValue;
        }

        /// <summary>
        /// Create predicate string based on product
        /// </summary>
        /// <returns></returns>
        public string GetProductDiscountPredicateBasedonProduct(string productId)
        {
            string predicate = $"product.id = \"{productId}\"";
            return predicate;
        }

        /// <summary>
        /// Create Product with a product variant
        /// </summary>
        /// <returns></returns>
        public Product CreateProductWithVariant()
        {
            Product product = this.productFixture.CreateProduct(true);
            this.productFixture.ProductsToDelete.Add(product);
            return product;
        }
    }
}
