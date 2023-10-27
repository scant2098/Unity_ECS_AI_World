using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using ZFramework.IO;

namespace JH_ECS
{
    public class Entity
    {
         public string _entityID;
         public Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
         public GameObject gameObject;
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
               T t = (T)_components[componentType];
              return t; 
          }
                 
          public void AddComponent<T>() where T : IComponent, new()
          {
              // 获取组件类型
              Type componentType = typeof(T);
              T t = new T();
              t = (T)t.Default;
              _components.TryAdd(typeof(T), t);
              //将组件添加进去之后才能进行注册
              if(!EcsManager.HasWorld()) return;
              EcsManager.CurrentWorld.EntitysController.RegistComponent(this,componentType);
          }
          public void AddComponent(IComponent component)
          {
              //添加进来存在数据的Component不需要Init
              //component.Init();
              _components.TryAdd(component.GetType(), component);
              if(!EcsManager.HasWorld()) return;
              EcsManager.CurrentWorld.EntitysController.RegistComponent(this,component.GetType());
          }

          public void RemoveComponent<T>() where T:IComponent,new()
          {
              _components.Remove(typeof(T));
              if(!EcsManager.HasWorld()) return;
              EcsManager.CurrentWorld.EntitysController.UnloadComponent(this,typeof(T));

          }
          public void RemoveComponent(IComponent component)
          {
              _components.Remove(component.GetType());
              if(!EcsManager.HasWorld()) return;
              EcsManager.CurrentWorld.EntitysController.UnloadComponent(this,component.GetType());
          }
          public void CreateStorageUnit() 
          {
              StorageUnit storageUnit = new StorageUnit(_entityID,_components);
              EcsManager.CurrentWorld.AddStorageUnit(storageUnit);
          }
    }
}

