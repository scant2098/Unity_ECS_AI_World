using System;
using UnityEngine;

namespace JH_ECS
{
    public class PositionSystem:ISystem
    {
        public void OnUpdate()
        {
            EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<PositionComponent>().ForEach(entity =>
            {
                var positionComponent = entity.GetComponent<PositionComponent>();
                entity.gameObject.transform.position = new 
                    Vector3(positionComponent.position.x,positionComponent.position.y,positionComponent.position.z);
            });
        }

        public void OnInit()
        {
        }
        public void OnEntityAdd(Entity entity)
        {
            var positionComponent = entity.GetComponent<PositionComponent>();
           // EcsManager.CurrentWorld.SpaceSplitHelper.AddObjectToGrid(entity);
            /*positionComponent.position.Subscribe(_ =>
            {
                EcsManager.CurrentWorld.SpaceSplitHelper.RefreshGridEntityInfo(entity);
            });*/
        }
        public int Priority { get; set; }
        public Type BindComponentType
        {
            get { return typeof(PositionComponent); }
        }
    }
}