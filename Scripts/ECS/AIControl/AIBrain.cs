namespace JH_ECS
{
    public static class AIBrain
    {
        public static ActionItem Think(PersonComponent personComponent)
        {
            var goal = personComponent.LifeGoals.Pop();
            return ActionLibrary.FindActionByName(ResolveGoal(goal));
        }
        private static string ResolveGoal(Goal goal)
        {
            return goal.Description;
        }
    }
}