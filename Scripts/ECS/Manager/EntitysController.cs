using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class EntitysController
    {
        public static int IDCount;
        private Dictionary<Type, List<Entity>> entitiesByComponentType = new Dictionary<Type, List<Entity>>();
        private DoubleKeyDictionary<Type, Type, List<Entity>> entitiesByDoubleComponentType =
            new DoubleKeyDictionary<Type, Type, List<Entity>>();
        public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        public EntitysController()
        {
            UnityBridge.CreateGameObject("Enitys","",EcsManager.CurrentWorld.WorldName);
        }

        public void DestroyEntity(string entityID)
        {
            if (Entities.ContainsKey(entityID))
            {
                Entities.Remove(entityID);
            }
        }
        private string AllocateID()
        {
            IDCount++;
            return "E00" + IDCount;
        }
        public void RegistEnity(Entity entity)
        {
            //防止其余地方恶行调用导致一个实体对应两个ID
            if (string.IsNullOrEmpty(entity._entityID))
                entity._entityID = AllocateID();
            entity.gameObject = UnityBridge.CreateGameObject(entity._entityID,"Enitys",EcsManager.CurrentWorld.WorldName);
            entity.gameObject.AddComponent<EntityBehaviour>();
            entity.gameObject.GetComponent<EntityBehaviour>().InitSelf(entity);
            var susccess=Entities.TryAdd(entity._entityID, entity);
            //对实体进行基础组件加载
         
        }

        public Entity CreateEnity()
        {
            return new Entity();
        }
        public void AddComponentToEntity<T>(Entity entity, T component) where T : IComponent
        {
            // 添加组件到实体
            entity.AddComponent(component);
            // 获取组件类型
            Type componentType = typeof(T);
            //加入单独字典
            if (!entitiesByComponentType.TryGetValue(componentType, out List<Entity> entityList))
            {
                entityList = new List<Entity>();
                entitiesByComponentType[componentType] = entityList;
            }
            entityList.Add(entity);
        }
        public List<Entity> GetEntitiesWithComponent<T>() where T : IComponent
        {
            Type componentType = typeof(T);

            if (entitiesByComponentType.TryGetValue(componentType, out List<Entity> entityList))
            {
                return entityList;
            }

            // 如果没有该组件类型的实体，返回一个空列表
            return new List<Entity>();
        }
        public List<Entity> GetEntitiesWithComponent<T1, T2>() where T1 : IComponent where T2 : IComponent
        {
            Profiler.BeginSample("GetEntities");
            Type componentType1 = typeof(T1);
            Type componentType2 = typeof(T2);
            if (entitiesByDoubleComponentType.TryGetValue(componentType1,componentType2, out List<Entity> entityList))
            {
                return entityList;
            }
            Profiler.EndSample();
            return null;
        }
    }
}

