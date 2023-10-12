using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JH_ECS
{
    public class UnityRigidBodySystem:ISystem
    {
        public void OnUpdate()
        {
            /*SystemsController.UpdateEntities<UnityRigidBodyComponent>(enity =>
            {
                UnityRigidBodyComponent rigidBodyComponent = enity.GetComponent<UnityRigidBodyComponent>();
                if(enity.GetComponent<UnityRigidBodyComponent>().rigidbody!=null) return;
                enity.GetComponent<UnityGameObjectComponent>().gameObject.AddComponent<Rigidbody>();
                rigidBodyComponent.rigidbody =
                    enity.GetComponent<UnityGameObjectComponent>().gameObject.GetComponent<Rigidbody>();
                rigidBodyComponent.rigidbody.useGravity = false;
            });*/
            /*SystemsController.UpdateEntitiesNotHasComponent(enity =>
            {
                if (enity.GetComponent<UnityGameObjectComponent>().gameObject.GetComponent<Rigidbody>())
                {
                    MonoBehaviour.Destroy(enity.GetComponent<UnityGameObjectComponent>().gameObject.GetComponent<Rigidbody>());
                }
            },typeof(UnityRigidBodyComponent),typeof(UnityGameObjectComponent));*/
        }

        public void OnInit()
        {
            
        }

        public int Priority { get; set; }
    }
}