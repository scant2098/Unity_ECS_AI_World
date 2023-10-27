using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
namespace JH_ECS
{
    public class MoveSystem : ISystem
    {
        public void OnUpdate()
        {
            var Entities = EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<MoveComponent>();
            NativeArray<EVector3> directions = new NativeArray<EVector3>(Entities.Count, Allocator.Persistent);
            NativeArray<float> moveSpeeds = new NativeArray<float>(Entities.Count, Allocator.Persistent);
            NativeArray<EVector3> positions = new NativeArray<EVector3>(Entities.Count, Allocator.Persistent);
            for (int i = 0; i < Entities.Count; i++)
            {
                var moveComponent = Entities[i].GetComponent<MoveComponent>();
                directions[i] = moveComponent._direction;
                moveSpeeds[i] = moveComponent._moveSpeed;
                positions[i] = Entities[i].GetComponent<PositionComponent>().position;
            }
            var job = new MoveJob
            {
                positions = positions,
                moveSpeed = moveSpeeds,
                direction = directions,
                deltaTime = Time.deltaTime
            };

            JobHandle jobHandle = job.Schedule(Entities.Count, 128); 
            jobHandle.Complete(); 
            // 更新实体的 PositionComponent
            for (int i = 0; i < positions.Length; i++)
            {
                var positionComponent = Entities[i].GetComponent<PositionComponent>();
                positionComponent.position = positions[i];
                Entities[i].UpdateComponent(positionComponent);
            }
            positions.Dispose();
            directions.Dispose();
            moveSpeeds.Dispose();
        }
        public void OnEntityAdd(Entity entity)
        {
            /*int x = UnityEngine.Random.Range(0, 2);
            int y =UnityEngine.Random.Range(0, 2);
            int z = UnityEngine.Random.Range(0, 2);
            var moveComponent = entity.GetComponent<MoveComponent>();
            moveComponent._direction = new EVector3(x, y, z);
            moveComponent._moveSpeed = 1;
            entity.UpdateComponent(new MoveComponent{_moveSpeed = moveComponent._moveSpeed,_direction = moveComponent._direction});*/
        }
        public int Priority { get; set; }
        public Type BindComponentType
        {
            get { return typeof(MoveComponent); }
        }
    }
    [BurstCompile]
    public struct MoveJob : IJobParallelFor
    {
        public NativeArray<EVector3> positions;
        public NativeArray<float> moveSpeed;
        public NativeArray<EVector3> direction;
        public float deltaTime;

        public void Execute(int index)
        {
            positions[index] += moveSpeed[index] * deltaTime * direction[index];
        }
    }

}

