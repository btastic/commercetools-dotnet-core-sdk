﻿using commercetools.Sdk.Registration;
using commercetools.Sdk.Domain;

namespace commercetools.Sdk.Serialization
{
    internal class SuggestTokenizerDecoratorTypeRetriever : DecoratorTypeRetriever<SuggestTokenizer>
    {
        public SuggestTokenizerDecoratorTypeRetriever(ITypeRetriever typeRetriever) : base(typeRetriever)
        {
        }
    }
}