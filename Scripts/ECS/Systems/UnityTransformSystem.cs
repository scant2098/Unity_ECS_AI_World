using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class UnityTransformSystem:ISystem
    {
        public void OnUpdate()
        {
            /*SystemsController.UpdateEntities(entity =>
            {
                Profiler.BeginSample("UnityTransformSystem");
                var transformComponent = entity.GetComponent<UnityTransformComponent>();
                //绑定position变化事件
                if (TransformPositionTrackerHelper.CheckPositionChanges(entity))
                {
                    EcsManager.CurrentWorld.SpaceSplitHelper.RefreshGridEntityInfo(entity);
                }
                Profiler.EndSample();
            },typeof(UnityGameObjectComponent),typeof(UnityTransformComponent));*/
            EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<PositionComponent>().ForEach(entity =>
            {
                var positionComponent = entity.GetComponent<PositionComponent>();
                entity.gameObject.transform.position = positionComponent.position;
            });
        }

        public void OnInit()
        {
            EcsManager.CurrentWorld.EntitysController.GetEntitiesWithComponent<PositionComponent>().ForEach(entity =>
            {
                var positionComponent = entity.GetComponent<PositionComponent>();
                //TransformPositionTrackerHelper.TrackTransform(entity.gameObject.transform);//加入监听列表
                int x = Random.Range(0, 10);
                int y = Random.Range(0, 10);
                int z = Random.Range(0, 10);
                entity.gameObject.transform.localScale /= 10;
                Vector3 vec = new Vector3(x, y, z);
                positionComponent.position = vec;
                entity.UpdateComponent<PositionComponent>(positionComponent);
            });
        }

        public int Priority { get; set; }
    }
}