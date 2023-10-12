using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class EcsWorld
    {
        public SystemsController SystemsController;
        public EntitysController EntitysController;
        public SpaceSplitHelper SpaceSplitHelper;
        public float WorldSize;
        public string WorldName;

        public EcsWorld(string worldName,float worldSize=100000)
        {
            // 自定义类的代码
            WorldSize = worldSize;
            WorldName = worldName;
            EcsManager.RegisterWorld(this);
            EntitysController = new EntitysController();
            for (int i = 0; i < 10000; i++)
            {
                Entity entity = new Entity();
                EntitysController.AddComponentToEntity<InputComponent>(entity,new InputComponent());
                EntitysController.AddComponentToEntity<PositionComponent>(entity,new PositionComponent());
                EntitysController.AddComponentToEntity<MoveComponent>(entity,new MoveComponent());
            }
            UnityEngine.Profiling.Profiler.EndSample();
            SpaceSplitHelper = new SpaceSplitHelper();
            SpaceSplitHelper.InitializeGrid();
            SystemsController = new SystemsController();
        }
    }
}