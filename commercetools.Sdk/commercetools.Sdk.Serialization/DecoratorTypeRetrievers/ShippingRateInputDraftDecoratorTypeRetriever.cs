﻿using commercetools.Sdk.Domain.Carts;
using commercetools.Sdk.Registration;

namespace commercetools.Sdk.Serialization
{
    internal class ShippingRateInputDraftDecoratorTypeRetriever : DecoratorTypeRetriever<IShippingRateInputDraft>
    {
        public ShippingRateInputDraftDecoratorTypeRetriever(ITypeRetriever typeRetriever) : base(typeRetriever)
        {
        }
    }
}