using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using ZFramework.IO;

namespace JH_ECS
{
    public static class EcsManager
    {
        private static Dictionary<string, EcsWorld> ecsWorlds;
        public static EcsWorld CurrentWorld
        {
            get
            {
                EcsWorld world;
                string currentSceneName = SceneManager.GetActiveScene().name;
                if (ecsWorlds == null||ecsWorlds.Count==0)
                {
                    ecsWorlds = GetAllWorldData();
                }
                ecsWorlds.TryGetValue(currentSceneName, out world);
                return world;
            }
        }
        private static Dictionary<string,EcsWorld> GetAllWorldData()
        {
            Dictionary<string,EcsWorld> worlds = new Dictionary<string,EcsWorld>();
            if (Directory.Exists(DataStorageHelper.Path))
            {
                string[] files = Directory.GetFiles(DataStorageHelper.Path, "*.json");
                foreach (string file in files)
                {
                    string fileName = Path.GetFileNameWithoutExtension(file);
                    var table = DataStorageHelper.ReadFromJsonFileByWorldName<StorageTable>(fileName);
                    EcsWorld world = new EcsWorld(table.WorldName,table);
                    worlds.Add(table.WorldName,world);
                }
            }
            return worlds;
        }

        public static bool HasWorld()
        {
            return ecsWorlds.Count > 0;
        }
        public static bool HasWorld(string worldName)
        {
            return ecsWorlds.ContainsKey(worldName); 
        }
        public static void CreateWorld(string worldname)
        {
            EcsWorld ecsWorld = new EcsWorld(worldname);
        }

        public static EcsWorld GetWorld(string worldName)
        {
            EcsWorld world;
            ecsWorlds.TryGetValue(worldName, out world);
            return world;
        }

        public static void CreateWorld()
        {
            UnityBridge.CreateScene("DefaultWorld");
        }

        public static void RegisterWorld(this EcsWorld ecsWorld)
        {
            if (!ecsWorlds.ContainsValue(ecsWorld))
            {
                ecsWorlds.Add(ecsWorld.WorldName,ecsWorld);
            }
        }
        public static void UnLoadWorld(this EcsWorld ecsWorld)
        {
            if (ecsWorlds.ContainsValue(ecsWorld))
            {
                ecsWorlds.Remove(ecsWorld.WorldName);
            }
            ZFramework.EventManager.Default.Dispose();
        }

     
    }
}