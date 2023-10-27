using System;

namespace JH_ECS
{
    public class EntityEvent
    {
        public class OnEntityAddComponent
        {
            public Entity Entity;
            public Type ComponentType;
        }
        public class OnEntityRemoveComponent
        {
            public Entity Entity;
            public Type ComponentType;
        }
    }
}