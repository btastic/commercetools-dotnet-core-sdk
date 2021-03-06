﻿using System.Collections.Concurrent;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace commercetools.Sdk.Serialization
{
    public class JsonSerializerSettingsFactory
    {
        private readonly DeserializationContractResolver deserializationContractResolver;
        private readonly SerializationContractResolver serializationContractResolver;
        private IDictionary<Type, JsonSerializerSettings> mapping = new ConcurrentDictionary<Type, JsonSerializerSettings>();

        public JsonSerializerSettingsFactory(DeserializationContractResolver deserializationContractResolver, SerializationContractResolver serializationContractResolver)
        {
            this.deserializationContractResolver = deserializationContractResolver;
            this.serializationContractResolver = serializationContractResolver;
        }

        public JsonSerializerSettings CreateDeserializationSettings(Type type)
        {
            try
            {
                if (!mapping.ContainsKey(type))
                {
                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.ContractResolver = this.deserializationContractResolver;

                    mapping[type] = settings;
                }

                return mapping[type];
            }
            catch (NullReferenceException)
            {
                return null;//default serialization settings will be used
            }
        }

        public JsonSerializerSettings CreateSerializationSettings(Type type)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            settings.ContractResolver = this.serializationContractResolver;
            return settings;
        }
    }
}
