using System;
using UnityEngine;

namespace JH_ECS
{
    public class PlayerControllerSystem:ISystem
    {
        public void OnUpdate()
        {
           SystemsController.UpdateEntities<PlayerControllerComponent>(entity =>
           {
               int forward = InputHelper.GetKey(KeyCode.W) ? 1 : 0;
               int backward = InputHelper.GetKey(KeyCode.S) ? -1 : 0;
               int right = InputHelper.GetKey(KeyCode.A) ? -1 : 0;
               int left = InputHelper.GetKey(KeyCode.D) ? 1 : 0;
               entity.UpdateComponent(new MoveComponent{_moveSpeed = 1,_direction = new EVector3(right+left,0,forward+backward)});
           });
            
        }

        public void OnInit()
        {
        }

        public void OnEntityAdd(Entity entity)
        {
        }

        public int Priority { get; set; }
        public Type BindComponentType
        {
            get { return typeof(PlayerControllerComponent); }
        }
    }
}