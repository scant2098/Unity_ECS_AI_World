using System;
using UnityEngine;

namespace JH_ECS
{
    public class UnityRigidBodySystem : ISystem
    {
        /// On this System is Running every frame execute this
        public void OnUpdate()
        {
        }
        //On one Entity first add the component which bind by this system 
        public void OnInit()
        {
        }
        //On one Entity first add the component
        public void OnEntityAdd(Entity entity)
        {
           var rigidbody= entity.gameObject.AddComponent<Rigidbody>();
           entity.UpdateComponent(new UnityRigidBodyComponent{Rigidbody = rigidbody});
        }

        public void OnEntityRemove(Entity entity)
        {
            MonoBehaviour.Destroy(entity.gameObject.GetComponent<Rigidbody>());
        }
        public void OnSystemDisable()
        {
        }

        public void OnSystemEnable()
        {
        }
        public int Priority { get; set; }
        public Type BindComponentType
        {
            get { return typeof(UnityRigidBodyComponent); }
        }
    }
}

