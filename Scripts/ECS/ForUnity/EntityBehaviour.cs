using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

namespace JH_ECS
{
    public class EntityBehaviour:MonoBehaviour
    {
        public Entity Entity
        {
            get
            {
                return EcsManager.CurrentWorld.EntitysController.Entities[EntityID];
            }
        }
        public string EntityID;
        public void InitSelf(Entity entity) 
        {
            EntityID = entity._entityID;
        }
        public void DestroySelf() 
        {
            EcsManager.CurrentWorld.EntitysController.DestroyEntity(Entity);
            DestroyImmediate(gameObject);
        }

    }
}