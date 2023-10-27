using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZFramework.IO;

namespace JH_ECS
{
    public class EntitysController
    {
        public int IDCount;
        public Dictionary<Type, List<Entity>> entitiesByComponentType = new Dictionary<Type, List<Entity>>();
        public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();

        public EntitysController()
        {
        }

        public void DestroyEntity(Entity entity)
        {
            if (Entities.ContainsKey(entity._entityID))
            {
                Entities.Remove(entity._entityID);
            }
            foreach (var kvp in entitiesByComponentType)
            {
                if (entity._components.ContainsKey(kvp.Key))
                {
                    kvp.Value.Remove(entity);
                }
            }
        }
        private string AllocateID() 
        {
            Debug.Log(IDCount);
            IDCount++;
            return "E00" + IDCount;
        }
        //对未存储的实体进行注册
        public void RegistEnity(Entity entity)
        {
            //防止其余地方恶行调用导致一个实体对应两个ID
            if (string.IsNullOrEmpty(entity._entityID))
                entity._entityID = AllocateID();
            if (Entities.ContainsKey(entity._entityID))
            {
               EcsManager.CurrentWorld.EntitysController.DestroyEntity(Entities[entity._entityID]); 
            }
            var susccess=Entities.TryAdd(entity._entityID, entity);
            //对实体进行基础组件加载
        }
        public void CreateGameObject(Entity entity)
        {
            entity.gameObject = UnityBridge.CreateGameObject(entity._entityID,EcsManager.CurrentWorld.WorldName);
            entity.gameObject.AddComponent<EntityBehaviour>();
            entity.gameObject.GetComponent<EntityBehaviour>().InitSelf(entity);
        }

        public void RegistComponent(Entity entity,Type componentType)
        {
            //加入单独字典
            if (!entitiesByComponentType.TryGetValue(componentType,
                    out List<Entity> entityList))
            {
                entityList = new List<Entity>();
                EcsManager.CurrentWorld.EntitysController.entitiesByComponentType[componentType] = entityList;
            }
            entityList.Add(entity);
            ZFramework.EventManager.Default.Publish(new EntityEvent.OnEntityAddComponent{Entity = entity,ComponentType = componentType});
        }

        public void UnloadComponent(Entity entity, Type commponentType)
        {
            List<Entity> list;
            if (entitiesByComponentType.TryGetValue(commponentType, out list))
            {
                list.Remove(entity);
            }
            ZFramework.EventManager.Default.Publish(new EntityEvent.OnEntityRemoveComponent{Entity = entity,ComponentType = commponentType});
        }
        /// <summary>
        ///提供给Editor的创建实体方法
        /// </summary>
        /// <returns></returns>
        public Entity CreateEntity()
        {
            Entity entity = new Entity();
            RegistEnity(entity);
            CreateGameObject(entity);
            return entity;
        }
        /// <summary>
        /// 使用Unit进行创建
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public Entity CreateEntity(StorageUnit unit,bool initgameobject=true)
        {
            var entity = new Entity();
            entity._entityID = unit.Id;
            //先创建GameObject再添加组件
            if(initgameobject)
              CreateGameObject(entity);
            RegistEnity(entity);
            unit.StorageComponentData.Values.ToList().ForEach(component =>
            {
                entity.AddComponent(component);
            });
            return entity;
        }

        public Entity CreateEntityByUnPlay(StorageUnit unit)
        {
            return CreateEntity(unit, false);
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

      
    }
}

