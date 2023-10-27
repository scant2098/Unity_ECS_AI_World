using System.Collections.Generic;

namespace JH_ECS
{
    public struct Goal
    {
        public string Description;
    }
    public struct PersonComponent : IComponent
    {
        public Stack<Goal> LifeGoals;
        public IComponent Default { get; }
    }

}