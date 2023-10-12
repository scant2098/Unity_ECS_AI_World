using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class UnityRendererSystem:ISystem
    {
        public void OnUpdate()
        {
           
        }

        public void OnInit()
        {
            /*MeshHelper.Init();
            SystemsController.UpdateEntities<UnityRendererComponent>(entity =>
            {
                Profiler.BeginSample("RendererCube");
                var gameObject=entity.GetComponent<UnityGameObjectComponent>(). gameObject;
                var rendererComponent = entity.GetComponent<UnityRendererComponent>();
                gameObject.AddComponent<MeshRenderer>();
                gameObject.GetComponent<MeshRenderer>().material = rendererComponent.RenderMaterial;
                gameObject.AddComponent<MeshFilter>();
                gameObject.GetComponent<MeshFilter>().mesh = MeshHelper.GetMesh(rendererComponent.meshType);
                Profiler.EndSample();
            });*/
        }

        public int Priority { get; set; }
    }
}