﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Type = System.Type;

namespace commercetools.Sdk.Serialization
{
    internal abstract class SetMapperTypeRetriever<T> : MapperTypeRetriever<T>
    {
        protected abstract Type SetType { get; }

        public SetMapperTypeRetriever(IEnumerable<ICustomJsonMapper<T>> customJsonMappers) : base(customJsonMappers)
        {
        }

        public override Type GetTypeForToken(JToken token)
        {
            Type type;
            if (IsSetAttribute(token))
            {
                if (token.HasValues)
                {
                    Type firstAttributeType = base.GetTypeForToken(token[0]);
                    type = this.SetType.MakeGenericType(firstAttributeType);
                }
                else
                {
                    // If the array contains no elements
                    type = this.SetType.MakeGenericType(typeof(object));
                }
            }
            else
            {
                type = base.GetTypeForToken(token);
            }
            return type;
        }

        private bool IsSetAttribute(JToken valueProperty)
        {
            return valueProperty != null && valueProperty.Type == JTokenType.Array;
        }
    }
}