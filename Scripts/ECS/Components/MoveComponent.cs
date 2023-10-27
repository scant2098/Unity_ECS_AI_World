using ZFramework.IO;

namespace JH_ECS
{
    public struct MoveComponent:IComponent
    {
        public EVector3 _direction;
        public float _moveSpeed;

        public IComponent Default
        {
            get { return new MoveComponent(); }
        }
    }
}