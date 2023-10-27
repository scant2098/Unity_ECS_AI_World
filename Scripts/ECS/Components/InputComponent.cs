using System.Collections.Generic;
using UnityEngine;

namespace JH_ECS
{
    public class InputComponent : IComponent
    {
        public Vector2 mousePosition; // 存储鼠标位置
        public GameObject gameObject { get; set; }
        public IComponent Default { get; }
    }

}