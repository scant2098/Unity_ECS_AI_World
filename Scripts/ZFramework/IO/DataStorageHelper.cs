using System;
using System.Collections.Generic;
using JH_ECS;
using Newtonsoft.Json;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace ZFramework.IO
{
    public static class DataStorageHelper
    {
        public static readonly string Path = Application.dataPath + "/Data";

        public static void TestWrite()
        {
            StorageUnit storageUnit = new StorageUnit("E001",new Dictionary<Type, IComponent>());
            storageUnit.StorageComponentData.Add(typeof(PositionComponent),new PositionComponent(EVector3.one));
            storageUnit.StorageComponentData.Add(typeof(MoveComponent),new MoveComponent());
            string json = JsonConvert.SerializeObject(storageUnit, Formatting.Indented);
            System.IO.File.WriteAllText(Path, json);
        }

        public static void WriteToJsonFile(string filePath, StorageTable data)
        {
            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, json);
        }
        /// <summary>
        /// Write the Data By Path Application.dataPath + "/Entity.json"
        /// </summary>
        /// <param name="data"></param>
        public static void WriteToJsonFileByDefaultPath(StorageTable data)
        {
            WriteToJsonFile(Path,data); 
        }

        public static void WriteToJsonFileByWorldName(string worldName,StorageTable data)
        {
            string path = System.IO.Path.Combine(Path, worldName + ".json");
            WriteToJsonFile(path,data);
        }
        public static T ReadFromJsonFile<T>(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                if (json == String.Empty) return default(T);
                T result = JsonConvert.DeserializeObject<T>(json);
                if (result == null)
                {
                    // JSON 数据不完整或不符合预期，执行错误处理
                    Debug.LogError("JSON data is incomplete.");
                    return default(T); // 或者抛出异常，或者其他错误处理
                }
                return result;
            }
            else
            {
                return default(T); // 或者抛出异常，视情况而定
            }
        }

        public static T ReadFromJsonFileByDefaultPath<T>()
        {
            return ReadFromJsonFile<T>(Path);
        }

        public static T ReadFromJsonFileByWorldName<T>(string worldName)
        {
            string path = System.IO.Path.Combine(Path, worldName + ".json");
            return ReadFromJsonFile<T>(path);
        }
    }
}