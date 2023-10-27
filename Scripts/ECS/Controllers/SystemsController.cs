using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace JH_ECS
{
    public class SystemsController
    {
        public List<ISystem> Systems = new List<ISystem>();

        public SystemsController()
        {
            LoadSystems();
            ExcuteSystemInitAction();
            Observable.EveryUpdate().Subscribe(_ =>
            {
                InputHelper.CheckInput();
                ExcuteSystemUpdateAction();
            });
           ZFramework.EventManager.Default.Receive<EntityEvent.OnEntityAddComponent>(entity =>
           {
              Systems.Find(_=>_.BindComponentType==entity.ComponentType).OnEntityAdd(entity.Entity);
           });
           ZFramework.EventManager.Default.Receive<EntityEvent.OnEntityRemoveComponent>(entity =>
           {
               Systems.Find(_=>_.BindComponentType==entity.ComponentType).OnEntityRemove(entity.Entity);
           });
        }
        public void RegisterSystem<T>() where T:ISystem,new()
        {
            Systems.Add(new T());
        }
        public void RegisterSystem(ISystem system)
        {
            Systems.Add(system);
        }
        private void LoadSystems()
        {
            ForAllSystemsDoAction(systemInstance =>
            {
                RegisterSystem(systemInstance);
            });
            Systems.ForEach(system =>
            {
                for (int i = 0; i < EcsSetting.SystemOrder.Count; i++)
                {
                    if (system.GetType().Name == EcsSetting.SystemOrder[i])
                    {
                        system.Priority = i;
                        break;
                    }
                }
            });
            Systems.Sort((system1, system2) => system1.Priority.CompareTo(system2.Priority));
        }
        private static void ForAllSystemsDoAction(Action<ISystem> callback)
        {
            var list = new List<ISystem>();
            var typesInJHECS = Assembly.GetAssembly(typeof(ISystem)).GetTypes()
                .Where(t => t.Namespace == "JH_ECS");
            var systemType = typesInJHECS
                .Where(t => typeof(ISystem).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
            foreach (var system in systemType)
            {
                var systemInstance = Activator.CreateInstance(system) as ISystem;
                if (systemInstance != null)
                {
                    callback(systemInstance);
                }
            }
        }
        public static List<string> GetAllSystemName()
        {
            var list = new List<string>();
            ForAllSystemsDoAction(system =>
            {
                list.Add(system.GetType().Name);
            });
            return list;
        }
        private void ExcuteSystemUpdateAction()
        {
           Systems.ForEach(_ =>
           {
               _.OnUpdate();
           });
        }
        private void ExcuteSystemInitAction()
        {
            Systems.ForEach(_ =>
            {
                _.OnSystemEnable();
            });
        }
        /// <summary>
        /// 对所有包含componentTypes所对应的组件的实体进行处理
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="componentTypes"></param>
        public static void UpdateEntities<T>(Action<Entity> callback) where T : IComponent
        {
            var filteredEntities = EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<T>();
            foreach (var entity in filteredEntities)
            {
                callback(entity);
            }
        }
    }
}

