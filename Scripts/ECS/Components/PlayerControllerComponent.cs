namespace JH_ECS
{
    public class PlayerControllerComponent:IComponent
    {
        public IComponent Default
        {
            get { return new PlayerControllerComponent(); }
        }
    }
}