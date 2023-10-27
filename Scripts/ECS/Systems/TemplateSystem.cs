using System;
namespace JH_ECS
{
    public class TemplateSystem : ISystem
    {
        /// On this System is Running every frame execute this
        public void OnUpdate()
        {
        }
        //On one Entity first add the component which bind by this system 
        public void OnSystemEnable()
        {
        }
        //On one Entity first add the component
        public void OnEntityAdd(Entity entity)
        {
        }
        public int Priority { get; set; }
        public Type BindComponentType
        {
            get { return typeof(TemplateComponent); }
        }
    }
}

