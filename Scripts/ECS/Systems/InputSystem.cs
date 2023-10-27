using System;
using UniRx;
using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class InputSystem:ISystem
    {
        public void OnUpdate()
        {
          
        }
        public void OnInit()
        {
            BindComponentType = typeof(InputComponent);
        }

        public void OnEntityAdd(Entity entity)
        {
            
        }
        public void OnEntityRemove(){}

        public int Priority { get; set; }
        public Type BindComponentType { get; set; }
    }
}