using UnityEditor;
using UnityEngine;
using ZFramework.IO;

namespace JH_ECS
{
    public class EntityHierarchyCreator:Editor
    {
        [MenuItem("GameObject/ECS/EmptyEntity", false, 1)]
        private static void CreateEmptyEntity()
        {
            var entity = EcsManager.CurrentWorld.EntitysController.CreateEntity();
            // 保留对象在场景切换时不被销毁
            entity.CreateStorageUnit();
            entity.AddComponent<PositionComponent>(); 
            EcsManager.CurrentWorld.StorageData();
        }
        [MenuItem("GameObject/ECS/3DEntity/Cube", false, 1)]
        private static void CreateCube()
        {
            Debug.Log("Custom menu item clicked!");
        }
        [MenuItem("GameObject/ECS/3DEntity/Sphere", false, 2)]
        private static void CreateSphere()
        {
            Debug.Log("Custom menu item clicked!");
        }   [MenuItem("GameObject/ECS/3DEntity/Plane", false, 3)]
        private static void CreatePlane()
        {
            Debug.Log("Custom menu item clicked!");
        }
    }
}