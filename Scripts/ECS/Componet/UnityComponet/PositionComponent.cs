using UnityEngine;

namespace JH_ECS
{
    public struct PositionComponent:IComponent
    {
       public Vector3 position;

       public PositionComponent(Vector3 vec)
       {
           position = vec;
       }
    }
}