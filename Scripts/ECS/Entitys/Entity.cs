using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Profiling;
using Task = System.Threading.Tasks.Task;

namespace JH_ECS
{
    public class Entity
    {
        public string _entityID;
        public Dictionary<Type,IComponent> _components = new Dictionary<Type,IComponent>();
        public GameObject gameObject;

        public Entity()
        {
            EcsManager.CurrentWorld.EntitysController.RegistEnity(this);
        }
        public bool HasComponent<T>() where T : IComponent
        {
            Profiler.BeginSample("HasComponent");
            bool b = _components.ContainsKey(typeof(T));
            Profiler.EndSample();
            return b;
        }
        
        public bool HasComponent(Type componentType)
        {
            bool b = _components.Any(component => componentType.IsInstanceOfType(component));
            return b;
        }
        public void UpdateComponent<T>(T updatedComponent) where T : struct, IComponent
        {
            Type componentType = typeof(T);

            // 更新 _components 集合中的结构体实例
            _components[componentType] = updatedComponent;
        }
        public T GetComponent<T>() where T : struct, IComponent
        {
            Type componentType = typeof(T);
            T t = (T)_components[componentType]; // 强制类型转换为 T
            return t; // 或者采取其他适当的处理方式
        }
        public void AddComponent<T>() where T : IComponent, new()
        {
            _components.Add(typeof(T),new T());
        }

        public void AddComponent(IComponent component)
        {
            _components.Add(component.GetType(),component);
            // 获取组件类
        }
    }
}

