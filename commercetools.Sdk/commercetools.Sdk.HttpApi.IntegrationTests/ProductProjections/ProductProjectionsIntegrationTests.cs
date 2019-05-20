using System;
using System.Collections.Generic;
using System.Linq;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.Domain.Categories;
using commercetools.Sdk.Domain.Predicates;
using commercetools.Sdk.Domain.ProductProjections;
using commercetools.Sdk.Domain.Query;
using Xunit;

namespace commercetools.Sdk.HttpApi.IntegrationTests.ProductProjections
{
    [Collection("Integration Tests")]
    public class ProductProjectionsIntegrationTests : IClassFixture<ProductProjectionsFixture>
    {
        private readonly ProductProjectionsFixture productProjectionsFixture;

        public ProductProjectionsIntegrationTests(ProductProjectionsFixture productProjectionsFixture)
        {
            this.productProjectionsFixture = productProjectionsFixture;
        }

        [Fact]
        public void GetProductProjectionById()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();
            var stagedProduct = this.productProjectionsFixture.productFixture.CreateProduct(true);
            var publishedProduct = this.productProjectionsFixture.productFixture.CreateProduct(withVariants:true, publish:true);
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;

            //Act
            var stagedProductProjection = commerceToolsClient.ExecuteAsync(new GetByIdCommand<ProductProjection>(new Guid(stagedProduct.Id), stagedAdditionalParameters)).Result;
            var publishedProductProjection = commerceToolsClient.ExecuteAsync(new GetByIdCommand<ProductProjection>(new Guid(publishedProduct.Id))).Result;

            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(stagedProduct);
            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(publishedProduct);

            //Assert
            Assert.Equal(stagedProduct.Id, stagedProductProjection.Id);
            Assert.True(stagedProductProjection.Published == false);
            Assert.Equal(publishedProduct.Id, publishedProductProjection.Id);
            Assert.True(publishedProductProjection.Published);
        }

        [Fact]
        public void GetProductProjectionByKey()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();
            var stagedProduct = this.productProjectionsFixture.productFixture.CreateProduct(true);
            var publishedProduct = this.productProjectionsFixture.productFixture.CreateProduct(withVariants:true, publish:true);
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;

            //Act
            var stagedProductProjection = commerceToolsClient.ExecuteAsync(new GetByKeyCommand<ProductProjection>(stagedProduct.Key, stagedAdditionalParameters)).Result;
            var publishedProductProjection = commerceToolsClient.ExecuteAsync(new GetByKeyCommand<ProductProjection>(publishedProduct.Key)).Result;

            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(stagedProduct);
            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(publishedProduct);

            //Assert
            Assert.Equal(stagedProduct.Key, stagedProductProjection.Key);
            Assert.True(stagedProductProjection.Published == false);
            Assert.Equal(publishedProduct.Key, publishedProductProjection.Key);
            Assert.True(publishedProductProjection.Published);
        }

        [Fact]
        public void QueryCurrentProductProjections()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();
            var publishedProduct = this.productProjectionsFixture.productFixture.CreateProduct(withVariants:true, publish:true);

            //Act
            QueryCommand<ProductProjection> queryCommand = new QueryCommand<ProductProjection>();
            queryCommand.Where(productProjection => productProjection.Key == publishedProduct.Key.valueOf());
            PagedQueryResult<ProductProjection> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;

            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(publishedProduct);

            //Assert
            Assert.NotNull(returnedSet.Results);
            Assert.Contains(returnedSet.Results, pp => pp.Id == publishedProduct.Id);
        }

        [Fact]
        public void QueryAndOffsetStagedProductProjections()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();

            ProductType productType = this.productProjectionsFixture.productFixture.CreateNewProductType();

            for (int i = 0; i < 3; i++)
            {
                Product stagedProduct = this.productProjectionsFixture.productFixture.CreateProduct(productType, true);
                this.productProjectionsFixture.productFixture.ProductsToDelete.Add(stagedProduct);
            }

            //Act
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;

            //Retrieve Products with created productType
            QueryCommand<ProductProjection> queryCommand = new QueryCommand<ProductProjection>(stagedAdditionalParameters);
            queryCommand.Where(productProjection => productProjection.ProductType.Id == productType.Id.valueOf());
            queryCommand.Offset(2);
            queryCommand.WithTotal(true);
            PagedQueryResult<ProductProjection> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;

            //Assert
            Assert.Single(returnedSet.Results);
            Assert.Equal(3, returnedSet.Total);
        }

        [Fact]
        public void QueryAndLimitStagedProductProjections()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();

            ProductType productType = this.productProjectionsFixture.productFixture.CreateNewProductType();

            for (int i = 0; i < 3; i++)
            {
                Product stagedProduct = this.productProjectionsFixture.productFixture.CreateProduct(productType, true);
                this.productProjectionsFixture.productFixture.ProductsToDelete.Add(stagedProduct);
            }

            //Act
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;

            //Retrieve Products with created productType limited by 2
            QueryCommand<ProductProjection> queryCommand = new QueryCommand<ProductProjection>(stagedAdditionalParameters);
            queryCommand.Where(productProjection => productProjection.ProductType.Id == productType.Id.valueOf());
            queryCommand.Limit(2);
            queryCommand.WithTotal(true);
            PagedQueryResult<ProductProjection> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;

            //Assert
            Assert.Equal(2, returnedSet.Results.Count);
            Assert.Equal(3, returnedSet.Total);
        }

        [Fact]
        public void QueryAndSortStagedProductProjections()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();

            ProductType productType = this.productProjectionsFixture.productFixture.CreateNewProductType();

            for (int i = 0; i < 3; i++)
            {
                Product stagedProduct = this.productProjectionsFixture.productFixture.CreateProduct(productType, true);
                this.productProjectionsFixture.productFixture.ProductsToDelete.Add(stagedProduct);
            }

            //Act
            ProductProjectionAdditionalParameters stagedAdditionalParameters = new ProductProjectionAdditionalParameters();
            stagedAdditionalParameters.Staged = true;

            //Retrieve Products with created productType sorted by name
            QueryCommand<ProductProjection> queryCommand = new QueryCommand<ProductProjection>(stagedAdditionalParameters);
            queryCommand.Where(productProjection => productProjection.ProductType.Id == productType.Id.valueOf());
            queryCommand.Sort(p=>p.Name["en"]);
            PagedQueryResult<ProductProjection> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;
            var sortedList = returnedSet.Results.OrderBy(p => p.Name["en"]);
            //Assert

            Assert.True(sortedList.SequenceEqual(returnedSet.Results));
        }

        /// <summary>
        /// Expand ProductType and Categories
        /// </summary>
        [Fact]
        public void QueryAndExpandParents()
        {
            //Arrange
            IClient commerceToolsClient = this.productProjectionsFixture.GetService<IClient>();
            ProductType productType = this.productProjectionsFixture.productFixture.CreateNewProductType();
            var publishedProduct =
                this.productProjectionsFixture.productFixture.CreateProduct(productType, true, true);

            //Act
            QueryCommand<ProductProjection> queryCommand = new QueryCommand<ProductProjection>();
            queryCommand.Where(productProjection => productProjection.ProductType.Id == productType.Id.valueOf());
            queryCommand.Expand(p => p.ProductType).Expand(p => p.Categories.ExpandAll());
            PagedQueryResult<ProductProjection> returnedSet = commerceToolsClient.ExecuteAsync(queryCommand).Result;

            this.productProjectionsFixture.productFixture.ProductsToDelete.Add(publishedProduct);

            //Assert
            Assert.NotNull(returnedSet.Results);
            Assert.Contains(returnedSet.Results,
                pp => pp.Id == publishedProduct.Id && pp.ProductType.Obj != null && pp.Categories.Count == 1 &&
                      pp.Categories[0].Obj != null);
        }
    }
}
