using UnityEngine;

namespace JH_ECS
{
    public class MeshHelper
    {
        private static Mesh sphereMesh;
        private static Mesh cubeMesh;
        private static Mesh cylinderMesh;
        // 可以添加其他类型的Mesh变量

        public static void Init()
        {
            // 初始化时创建不同类型的几何体Mesh
            sphereMesh = CreatePrimitiveMesh(PrimitiveType.Sphere);
            cubeMesh = CreatePrimitiveMesh(PrimitiveType.Cube);
            cylinderMesh = CreatePrimitiveMesh(PrimitiveType.Cylinder);
            // 可以为其他几何体类型创建Mesh
        }
        public static Mesh GetMesh(MeshType primitiveType)
        {
            switch (primitiveType)
            {
                case MeshType.Sphere:
                    return sphereMesh;
                case MeshType.Cube:
                    return cubeMesh;
                case MeshType.Cylinder:
                    return cylinderMesh;
                default:
                    return null; // 如果没有匹配的类型，返回null或抛出异常
            }
        }
        private static Mesh CreatePrimitiveMesh(PrimitiveType primitiveType)
        {
            GameObject primitive = GameObject.CreatePrimitive(primitiveType);
            Mesh primitiveMesh = primitive.GetComponent<MeshFilter>().sharedMesh;
            MonoBehaviour.Destroy(primitive); // 销毁临时创建的游戏对象
            return primitiveMesh;
        }
    }
}