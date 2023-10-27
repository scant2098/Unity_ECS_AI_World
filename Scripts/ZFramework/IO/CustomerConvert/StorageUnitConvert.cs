using System;
using System.Collections.Generic;
using JH_ECS;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace ZFramework.IO.CustomerConvert
{
    public class StorageUnitConvert : JsonConverter<StorageUnit>
    {
        public override void WriteJson(JsonWriter writer, StorageUnit value, JsonSerializer serializer)
        {
            if (value == StorageUnit.Null)
            {
                writer.WriteNull();
                return;
            }
            var jsonObject = new JObject();
            jsonObject.Add("Id", value.Id);
            foreach (var kvp in value.StorageComponentData)
            {
                var componentType = kvp.Key;
                var component = kvp.Value;
                var typeName = componentType.Name;
                if (component is IStorage)
                {
                    var componentJson = JsonConvert.SerializeObject(component, Formatting.None, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                    jsonObject[typeName] = JToken.Parse(componentJson);
                }
                else
                    jsonObject[typeName] = "NullData";
            }
            jsonObject.WriteTo(writer);
        }
        public override StorageUnit ReadJson(JsonReader reader, Type objectType, StorageUnit existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return StorageUnit.Null;
            }

            if (reader.TokenType != JsonToken.StartObject)
            {
                throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
            }
            var jsonObject = JObject.Load(reader);

            var id = jsonObject["Id"].Value<string>();
            var storageComponentData = new Dictionary<Type, IComponent>();
            foreach (var property in jsonObject.Properties())
            {
                if (property.Name != "Id")
                {
                    var typeName = property.Name;
                    var componentType = Type.GetType("JH_ECS."+typeName);
                    if (componentType != null)
                    {
                        var componentJson = property.Value.ToString();
                        if (componentJson != "NullData")
                        {
                            var component = JsonConvert.DeserializeObject(componentJson, componentType, new JsonSerializerSettings
                            {
                                TypeNameHandling = TypeNameHandling.Auto
                            });

                            if (component is IComponent)
                            {
                                storageComponentData[componentType] = (IComponent)component;
                            }
                        }
                        else
                        {
                            storageComponentData[componentType] = (IComponent)Activator.CreateInstance(componentType);
                        }
                    }
                }
            }

            return new StorageUnit(id, storageComponentData);
        }

    }

}