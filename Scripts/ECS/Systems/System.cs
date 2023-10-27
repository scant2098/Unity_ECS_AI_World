using System;
namespace JH_ECS
{
    public interface ISystem
    {
        public void OnUpdate();
        public void OnEntityAdd(Entity entity){}
        public void OnEntityRemove(Entity entity){}
        public void OnSystemDisable(){}
        public void OnSystemEnable(){}
        public int Priority
        {
            get;
            set;
        }
        public Type BindComponentType { get;}
    }
}


