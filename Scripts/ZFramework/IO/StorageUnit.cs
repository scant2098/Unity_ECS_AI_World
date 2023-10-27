using System;
using System.Collections.Generic;
using System.Linq;
using JH_ECS;
using Newtonsoft.Json;
using ZFramework.IO.CustomerConvert;

namespace ZFramework.IO
{
    [JsonConverter(typeof(StorageUnitConvert))]
    public struct StorageUnit:IEquatable<StorageUnit>
    { 
        public string Id;
        public Dictionary<Type,IComponent> StorageComponentData;
      //  public List<Type> Components;

        public StorageUnit(string id, Dictionary<Type, IComponent> components)
        {
            Id = id;
            //Components = components.Keys.ToList();
            StorageComponentData = components;
        }
        public bool Equals(StorageUnit other)
        {
            return Id == other.Id;
        }
        public static bool operator ==(StorageUnit a, StorageUnit b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(StorageUnit a, StorageUnit b)
        {
            return !a.Equals(b);
        }
        public static StorageUnit Null = new StorageUnit();
    }
}