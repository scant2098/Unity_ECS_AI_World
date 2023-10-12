using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using Unity.Burst;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class MoveSystem : ISystem
    {
        public void OnUpdate()
        {
            var Entities =EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<MoveComponent>();
            Profiler.BeginSample("MoveSystem");
            // 使用 Job 来更新位置
            NativeArray<Vector3> positions = new NativeArray<Vector3>(Entities.Count,Allocator.Persistent);
            for (int i = 0; i < Entities.Count; i++)
            {
                positions[i] = Entities[i].GetComponent<PositionComponent>().position;
            }
            /*int forward = InputHelper.keyPresseds.Contains(KeyCode.W) ? 1 : 0;
            int backforward=InputHelper.keyPresseds.Contains(KeyCode.S)?-1:0;
            int left = InputHelper.keyPresseds.Contains(KeyCode.A)?1:0;
            int right = InputHelper.keyPresseds.Contains(KeyCode.D) ? -1 : 0;
            int vertical = forward + backforward;
            int horizontal = left + right;
            Vector3 vec=new Vector3(vertical, 0, horizontal);*/
            var job = new MoveJob
            {
                positions = positions,
                rotationSpeed = 3,
                riseSpeed = 4f,
                deltaTime = Time.deltaTime
            };
            JobHandle jobHandle = job.Schedule(Entities.Count, 128); // 并行度为64
            jobHandle.Complete(); // 等待Job完成
            for (int i = 0; i < positions.Length; i++)
            {
                Entities[i].UpdateComponent(new PositionComponent(positions[i]));
            }
            positions.Dispose();
            Profiler.EndSample();
        }
        public void OnInit()
        {
            
        }
        public int Priority { get; set; }
    } 
    [BurstCompile]
    public struct MoveJob : IJobParallelFor
    {
        public NativeArray<Vector3> positions;
        public float riseSpeed;
        public float rotationSpeed;
        public float deltaTime;
        public void Execute(int index)
        {
            for (int i = 0; i < 100; i++)
            {
                Vector3 currentPosition = positions[index];

                // 计算上升高度
                float riseAmount = riseSpeed * deltaTime;

                // 计算旋转角度
                float rotationAmount = rotationSpeed * deltaTime;

                // 更新物体的高度
                currentPosition.y += riseAmount;

                // 计算螺旋旋转
                float angle = Mathf.Atan2(currentPosition.y, currentPosition.x) + rotationAmount;

                // 计算新位置
                float radius = Mathf.Sqrt(currentPosition.x * currentPosition.x + currentPosition.z * currentPosition.z);
                Vector3 newPosition = new Vector3(radius * Mathf.Cos(angle), currentPosition.y, radius * Mathf.Sin(angle));

                // 更新物体位置
                positions[index] = newPosition;
            }
            
        }
    }

}

