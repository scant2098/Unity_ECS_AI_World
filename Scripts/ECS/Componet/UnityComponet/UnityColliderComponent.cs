using System;
using UnityEngine;

namespace JH_ECS
{
    public struct UnityColliderComponent:IComponent
    {
        public Action<Entity> OnColliderEnter;
        public Action<Entity> onColliderUpdate;
        public Action<Entity> onColliderExit;
        public Collider Collider;
        public float Height;
        public float Width;
        public float Length;
        public GameObject gameObject { get; set; }
    }
}