using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH_ECS
{
    public static class UnityBridge
    {
        public static string CreateScene(string newSceneName)
        {
            // 判断新场景名称是否已经存在，如果存在则不再创建
            if (!SceneExists(newSceneName))
            {
                // 创建新场景
                Scene newScene = SceneManager.CreateScene(newSceneName);
                if (newScene.IsValid())
                {
                    return newSceneName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                Debug.LogWarning("场景已存在：" + newSceneName);
                return "";
            }
        }
        public static GameObject CreateGameObject(string name,string targetSceneName=null)
        {
            var obj=GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = name; 
            TryMoveGameObjectToSence(targetSceneName,obj);
            return obj;
        }
     
        private static void TryMoveGameObjectToSence(string targetSceneName,GameObject obj)
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                Scene targetScene = SceneManager.GetSceneByName(targetSceneName);
                if (targetScene.IsValid())
                    SceneManager.MoveGameObjectToScene(obj, targetScene);
            }
        }
        private static bool SceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
        public static void ReloadAllEntitiesOnScene()
        {
            var scene = SceneManager.GetActiveScene();
            EntityBehaviour[] scriptInstances = GameObject.FindObjectsOfType<EntityBehaviour>();
            foreach (EntityBehaviour scriptInstance in scriptInstances) 
            {
                GameObject gameObject = scriptInstance.gameObject;
                if (gameObject.scene == scene)
                { 
                    var units = EcsManager.CurrentWorld.StorageTableForRead.StorageUnits;
                    var unit=units.Find(unit => unit.Id == scriptInstance.EntityID);
                    var entity = EcsManager.CurrentWorld.EntitysController.CreateEntityByUnPlay(unit);
                    entity.gameObject = gameObject;
                    scriptInstance.InitSelf(entity);
                }
            }
        }

        public static bool CheckEntityInScene(string entityID)
        {
            EntityBehaviour[] scriptInstances = GameObject.FindObjectsOfType<EntityBehaviour>();
            foreach (EntityBehaviour scriptInstance in scriptInstances)
            { 
                if (scriptInstance.Entity._entityID == entityID)
                    return true;
            }
            return false;
        }
    }
}