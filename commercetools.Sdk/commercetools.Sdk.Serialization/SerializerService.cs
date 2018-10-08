﻿namespace commercetools.Sdk.Serialization
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;

    public class SerializerService : ISerializerService
    {
        private JsonSerializerSettingsFactory jsonSerializerSettingsFactory;

        public SerializerService(JsonSerializerSettingsFactory jsonSerializerSettingsFactory)
        {
            this.jsonSerializerSettingsFactory = jsonSerializerSettingsFactory;
        }

        public T Deserialize<T>(string input)
        {
            var settings = this.jsonSerializerSettingsFactory.Create(typeof(T));

            return JsonConvert.DeserializeObject<T>(input, settings);
        }

        public string Serialize<T>(T input)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            return JsonConvert.SerializeObject(input, serializerSettings);
        }
    }
}