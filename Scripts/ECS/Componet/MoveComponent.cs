using UnityEngine;

namespace JH_ECS
{
    public struct MoveComponent:IComponent
    {
        public Vector3 _direction;
        public float _moveSpeed;
    }
}