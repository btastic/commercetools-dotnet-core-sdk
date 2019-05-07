﻿using System;
using System.Linq.Expressions;
using commercetools.Sdk.Linq;
using commercetools.Sdk.Registration;
using commercetools.Sdk.Linq.Filter;

namespace commercetools.Sdk.Domain.ProductProjections
{
    public class Filter<T>
    {
        public Expression<Func<T, bool>> Expression { get; private set; }

        public Filter(Expression<Func<T, bool>> expression)
        {
            this.Expression = expression;
        }

        public override string ToString()
        {
            return this.Expression.RenderFilterPredicate();
        }
    }
}
