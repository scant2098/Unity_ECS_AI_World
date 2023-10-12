namespace JH_ECS
{
    public interface ISystem
    {
        public void OnUpdate();
        public void OnInit();

        public int Priority
        {
            get;
            set;
        }
    }
}


