﻿using commercetools.Sdk.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Type = System.Type;

namespace commercetools.Sdk.Serialization
{
    public class AttributeConverter : JsonConverter
    {
        private readonly IMapperTypeRetriever<Domain.Attribute> mapperTypeRetriever;

        public AttributeConverter(IMapperTypeRetriever<Domain.Attribute> mapperTypeRetriever)
        {
            this.mapperTypeRetriever = mapperTypeRetriever;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Domain.Attribute))
            {
                return true;
            }
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            JToken valueProperty = jsonObject["value"];
            Type attributeType;
            if (IsSetAttribute(valueProperty))
            {
                Type firstAttributeType = this.mapperTypeRetriever.GetTypeForToken(valueProperty[0]);
                Type genericType = firstAttributeType.BaseType.GenericTypeArguments[0];
                Type setType = typeof(SetAttribute<>);
                // TODO Maybe find the non generic type inheriting from the generic type, e.g. SetTextAtrribute instead of SetAttribute<string>
                attributeType = setType.MakeGenericType(genericType);
            }
            else
            { 
                attributeType = this.mapperTypeRetriever.GetTypeForToken(valueProperty);
            }

            if (attributeType == null)
            {
                // TODO Move this message to a localizable resource and add more information to the exception
                throw new JsonSerializationException("Attribute type cannot be determined.");
            }

            return jsonObject.ToObject(attributeType, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private bool IsSetAttribute(JToken valueProperty)
        {
            if (valueProperty != null)
            {
                return valueProperty.HasValues && valueProperty.Type == JTokenType.Array;
            }
            return false;
        }
    }
}