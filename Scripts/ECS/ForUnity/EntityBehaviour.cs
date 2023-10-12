using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JH_ECS
{
    public class EntityBehaviour:MonoBehaviour
    {
        public Entity Entity;
        public List<IComponent> Components;

        public void InitSelf(Entity entity)
        {
            Entity = entity;
            Components = entity._components.Values.ToList();
        }
        public void DestroySelf()
        {
            EcsManager.CurrentWorld.EntitysController.DestroyEntity(Entity._entityID);
            Destroy(gameObject);
        }
    }
}