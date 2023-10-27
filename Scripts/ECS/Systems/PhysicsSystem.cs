using System;
using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class PhysicsSystem:ISystem
    {
        public void OnUpdate()
        {
            /*SystemsController.UpdateEntities<UnityColliderComponent,UnityRigidBodyComponent>(entity =>
            {
                Profiler.BeginSample("PhysicsSystem");
                var transformComponent = entity.GetComponent<UnityTransformComponent>();
                var colliderComponent = entity.GetComponent<UnityColliderComponent>();
                var rigidbodyComponent = entity.GetComponent<UnityRigidBodyComponent>();
                var list = EcsManager.CurrentWorld.SpaceSplitHelper.GetNearlyEntity(entity);
                foreach (var item in list)
                {
                    var transform = await item.GetComponent<UnityTransformComponent>().transform;
                    if(!item.HasComponent<UnityColliderComponent>()) return;
                    //是否发生碰撞
                    if (CollisionHandlerHelper.CheckCollision(transformComponent.transform, transform))
                    {
                        //是否持续碰撞
                        var vec = (transform.transform.position - transformComponent.transform.position).normalized;
                        rigidbodyComponent.rigidbody.AddForce(-vec,ForceMode.Impulse);
                        if (!CollisionHandlerHelper.CheckEntitiesCollision(entity, item))
                        {
                            CollisionHandlerHelper.AddCollisionInfo(entity,item);
                            colliderComponent.OnColliderEnter(entity);
                        }
                        colliderComponent.onColliderUpdate(item);
                    }
                    else
                    {
                        if (CollisionHandlerHelper.CheckEntitiesCollision(entity, item))
                        {
                            //没有发生碰撞并且碰撞信息存在，说明退出了碰撞
                            colliderComponent.onColliderExit(item);
                            CollisionHandlerHelper.RemoveCollisionInfo(entity,item);
                        }
                    }
                }
                Profiler.EndSample();
            });*/
        }
        public void OnInit()
        {
            /*SystemsController.UpdateEntitiesNotHasComponent(entity =>
            {
                //EcsManager.CurrentWorld.SpaceSplitHelper.AddObjectToGrid(entity);
                /*var colliderComponent = entity.GetComponent<UnityColliderComponent>();
                colliderComponent.onColliderUpdate = colentity =>
                {
                    Debug.Log(entity._entityID+"与"+colentity._entityID+"正在持续发生碰撞");
                };
                colliderComponent.OnColliderEnter = colentity =>
                {
                    Debug.Log(entity._entityID+"与"+colentity._entityID+"发生了碰撞");
                };
                colliderComponent.onColliderExit = colentity =>
                {
                    Debug.Log(entity._entityID+"与"+colentity._entityID+"结束了碰撞");
                };#1#
            },typeof(UnityRigidBodyComponent),typeof(UnityColliderComponent));   */
        }

        public void OnEntityAdd(Entity entity)
        {
            throw new System.NotImplementedException();
        }
        public int Priority { get; set; }
        public Type BindComponentType { get; set; }
    }
}