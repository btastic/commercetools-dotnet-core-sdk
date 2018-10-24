﻿namespace commercetools.Sdk.LinqToQueryPredicate.Tests
{
    using commercetools.Sdk.Domain;
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Xunit;

    public class FacetTests
    {
        [Fact]
        public void TermFacetCategoryId()
        {
            Expression<Func<ProductProjection, string>> expression = p => p.Categories.Select(c => c.Id).FirstOrDefault();
            ITermFacetExpressionVisitor facetExpressionVisitor = new ComparablePropertyExpressionVisitor();
            string result = facetExpressionVisitor.Render(expression);
            Assert.Equal("categories.id", result);
        }

        [Fact]
        public void TermFacetAverageRating()
        {
            Expression<Func<ProductProjection, double>> expression = p => p.ReviewRatingStatistics.AverageRating;
            ITermFacetExpressionVisitor facetExpressionVisitor = new ComparablePropertyExpressionVisitor();
            string result = facetExpressionVisitor.Render(expression);
            Assert.Equal("reviewRatingStatistics.averageRating", result);
        }

        [Fact]
        public void RangeFacetCentAmount()
        {
            Expression<Func<ProductProjection, bool>> expression = p => p.Variants.Any(v => v.Price.Value.CentAmount.Range(1, 30));
            IFilterExpressionVisitor filterExpressionVisitor = new FilterExpressionVisitor();
            string result = filterExpressionVisitor.Render(expression);
            Assert.Equal("variants.price.centAmount:range (1 to 30)", result);
        }

        [Fact]
        public void FilterFacetCategoryId()
        {
            Expression<Func<ProductProjection, bool>> expression = p => p.Categories.Any(c => c.Id == "34940e9b-0752-4ffa-8e6e-4f2417995a3e");
            IFilterExpressionVisitor filterExpressionVisitor = new FilterExpressionVisitor();
            string result = filterExpressionVisitor.Render(expression);
            Assert.Equal("categories.id:\"34940e9b-0752-4ffa-8e6e-4f2417995a3e\"", result);
        }

        [Fact]
        public void FilterFacetCategoryAllSubtrees()
        {
            Expression<Func<ProductProjection, bool>> expression = p => p.Categories.Any(c => c.Id.Subtree("*"));
            IFilterExpressionVisitor filterExpressionVisitor = new FilterExpressionVisitor();
            string result = filterExpressionVisitor.Render(expression);
            Assert.Equal("categories.id: subtree(\"*\")", result);
        }

        [Fact]
        public void FilterFacetAttributeIn()
        {
            Expression<Func<ProductProjection, bool>> expression = p => p.Variants.Any(v => v.Attributes.Any(a => a.Name == "color" && ((TextAttribute)a).Value.In("red", "green")));
            IFilterExpressionVisitor filterExpressionVisitor = new FilterExpressionVisitor();
            string result = filterExpressionVisitor.Render(expression);
            Assert.Equal("variants.attributes.color:\"red\",\"green\"", result);
        }
    }
}
