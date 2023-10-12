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
        public static GameObject CreateGameObject(string name, string transformName=null,string targetSceneName=null)
        {
            var obj=GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.name = name;
            //TODO:是否能把Find给优化
            if (!string.IsNullOrEmpty(transformName))
                obj.transform.SetParent(GameObject.Find(transformName).transform);
            else
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
    }
}