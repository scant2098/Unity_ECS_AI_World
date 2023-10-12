using UnityEngine;

namespace JH_ECS
{
    public struct UnityRigidBodyComponent:IComponent
    {
        public Rigidbody rigidbody;
        public GameObject gameObject { get; set; }
    }
}