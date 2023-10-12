using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UniRx;
using UnityEngine;
using UnityEngine.Profiling;

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
            Profiler.BeginSample("ExcuteSystemUpdateAction");
           Systems.ForEach(_ =>
           {
               _.OnUpdate();
           });
           Profiler.EndSample();
        }
        private void ExcuteSystemInitAction()
        {
            Systems.ForEach(_ =>
            {
                _.OnInit();
            });
        }
        /// <summary>
        /// 对所有包含componentTypes所对应的组件的实体进行处理
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="componentTypes"></param>
        public static void UpdateEntities(Action<Entity> callback,params Type[] componentTypes)
        {
            var filteredEntities = EcsManager.CurrentWorld.EntitysController.Entities
                .Where(entity =>
                    componentTypes.All(type => entity.Value.HasComponent(type)));
            foreach (var entity in filteredEntities)
            {
                callback(entity.Value);
            }
            /*filteredEntities.ForEach(entity =>
            {
                callback(entity);
            });*/
        }
        /*public static void UpdateEntities<T>(Action<Entity> callback) where T:IComponent
        {
            Profiler.BeginSample("SelectEntities");
            var filteredEntities = EcsManager.CurrentWorld.EntitysController.Entities
                .Where(entity => entity.Value.HasComponent<T>())
                .Select(entity => entity.Value)
                .ToList();
            Profiler.EndSample();
            filteredEntities.ForEach(entity =>
            {
                callback(entity);
            });
        }
        public static void UpdateEntitiesNotHasComponent(Action<Entity> callback,params Type[] componentTypes)
        {
            List<Entity> filteredEntities = EcsManager.CurrentWorld.EntitysController.Entities
                .Where(entity =>
                    componentTypes.All(type => entity.Value.HasComponent(type)))
                .Select(entity => entity.Value)
                .ToList();
            var notInlist = EcsManager.CurrentWorld.EntitysController.Entities
                .Where(item => !filteredEntities.Contains(item.Value))
                .Select(entity => entity.Value).ToList();
            notInlist.ForEach(entity =>
            {
                callback(entity);
            });
        }*/
    }
}

