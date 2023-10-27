namespace JH_ECS
{
    public struct TemplateComponent : IComponent
    {
        public IComponent Default
        {
            get { return new TemplateComponent(); }
        }
    }
}

