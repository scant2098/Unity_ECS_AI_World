using UnityEngine;

namespace JH_ECS
{
    public struct UnityRigidBodyComponent : IComponent
    {
        public Rigidbody Rigidbody;
        public IComponent Default
        {
            get { return new UnityRigidBodyComponent(); }
        }
    }
}

