using UnityEngine;

namespace JH_ECS
{
    public enum MeshType{Cube,Sphere,Cylinder,CustomerModel}
    public class UnityRendererComponent:IComponent
    {
        public MeshType meshType;
        public int RendererOrder;
        public Material RenderMaterial=new Material(Shader.Find("Standard"));
        public GameObject gameObject { get; set; }

        public IComponent Default { get; }
    }
}