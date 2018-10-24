﻿using commercetools.Sdk.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace commercetools.Sdk.LinqToQueryPredicate.Tests
{
    public class SortTests
    {
        [Fact]
        public void SortCategoryParentTypeId()
        {
            Expression<Func<Category, string>> expression = c => c.Parent.TypeId;
            ComparablePropertyExpressionVisitor sortVisitor = new ComparablePropertyExpressionVisitor();
            string result = sortVisitor.Render(expression);
            Assert.Equal("parent.typeId", result);
        }

        [Fact]
        public void SortCategorySlug()
        {
            Expression<Func<Category, string>> expression = c => c.Slug["en"];
            ComparablePropertyExpressionVisitor sortVisitor = new ComparablePropertyExpressionVisitor();
            string result = sortVisitor.Render(expression);
            Assert.Equal("slug.en", result);
        }

        [Fact]
        public void SortProductName()
        {
            Expression<Func<Product, string>> expression = p => p.MasterData.Current.Name["en"];
            ComparablePropertyExpressionVisitor sortVisitor = new ComparablePropertyExpressionVisitor();
            string result = sortVisitor.Render(expression);
            Assert.Equal("masterData.current.name.en", result);
        }
    }
}
