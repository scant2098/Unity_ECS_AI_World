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
            
        }

        public int Priority { get; set; }
    }
}