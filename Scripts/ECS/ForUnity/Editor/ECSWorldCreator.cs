using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH_ECS
{
    public class ECSWorldCreator:EditorWindow
    {
        private string newSceneName = "NewWorld";

        [MenuItem("Assets/Create/ECS/ECSWorld")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ECSWorldCreator), false, "Create New ECSWorld");
        }

        private void OnGUI()
        {
            GUILayout.Label("Create New ECSWorld", EditorStyles.boldLabel);
            newSceneName = EditorGUILayout.TextField("World Name:", newSceneName);

            if (GUILayout.Button("Create"))
            {
                CreateNewScene(newSceneName);
            }
        }

        private void CreateNewScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError("World name cannot be empty.");
                return;
            }

            string selectedFolderPath = "Assets"; // 默认为Assets文件夹
            UnityEngine.Object[] selectedAssets = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
            if (selectedAssets.Length == 1)
            {
                string selectedPath = AssetDatabase.GetAssetPath(selectedAssets[0]);
                if (AssetDatabase.IsValidFolder(selectedPath))
                {
                    selectedFolderPath = selectedPath;
                }
            }

            string scenePath = Path.Combine(selectedFolderPath, sceneName + ".unity");

            // 创建新场景
            Scene newScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            // 将 GameObject 添加到新场景
            GameObject newGameObject = new GameObject("Entities");
            SceneManager.MoveGameObjectToScene(newGameObject, newScene);
            GameObject EcsStartPoint = new GameObject("EcsStartPoint");
            EcsStartPoint.AddComponent<EcsStartPoint>();
            SceneManager.MoveGameObjectToScene(EcsStartPoint, newScene);
            EditorSceneManager.SaveScene(newScene, scenePath);
            EcsManager.CreateWorld(sceneName);
            AssetDatabase.Refresh();
            Close();
        }
    }
}