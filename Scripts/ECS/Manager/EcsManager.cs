using System.Collections.Generic;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public static class EcsManager
    {
        private static List<EcsWorld> ecsWorlds=new List<EcsWorld>();

        public static EcsWorld CurrentWorld
        {
            get
            {
                return ecsWorlds[0];
            }
        }
        public static EcsWorld GetCurrentWorld()
        {
            return ecsWorlds[0];
        }

        public static void CreateWorld(string worldname)
        {
            Profiler.BeginSample("CreateScene");
            UnityBridge.CreateScene(worldname);
            Profiler.EndSample();
            EcsWorld ecsWorld = new EcsWorld(worldname);
            
        }

        public static void CreateWorld()
        {
            UnityBridge.CreateScene("DefaultWorld");
        }

        public static void RegisterWorld(EcsWorld ecsWorld)
        {
            if (!ecsWorlds.Contains(ecsWorld))
            {
                ecsWorlds.Add(ecsWorld);
            }
        }
    }
}