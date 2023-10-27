using System;

namespace JH_ECS
{
    public class PersonActionSystem:ISystem
    {
        public void OnUpdate()
        {
            var Entities =EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<PersonComponent>();
            Entities.ForEach(entity =>
            {
                var actionItem = AIBrain.Think(entity.GetComponent<PersonComponent>());
                actionItem.Execute();
            });
        }
        public void OnEntityAdd(Entity entity)
        {
            
        }

        public int Priority { get; set; }
        public Type BindComponentType { get; set; }
    }
}