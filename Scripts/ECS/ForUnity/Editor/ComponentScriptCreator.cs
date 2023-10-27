using System.IO;
using UnityEditor;
using UnityEngine;

namespace JH_ECS
{
    public class ComponentScriptCreator:EditorWindow
    {
        private static string templatePath = "Assets/Scripts/ECS/Components/TemplateComponent.cs"; // 模板文件路径
        private string scriptName = "NewComponent"; // 新脚本的名称

        [MenuItem("Assets/Create/ECS/Component Script", false, 1)]
        public static void CreateSystemScript()
        {
            GetWindow<ComponentScriptCreator>("Create Component Script");
        }
        private void OnGUI()
        {
            GUILayout.Label("Create Component Script", EditorStyles.boldLabel);
            scriptName = EditorGUILayout.TextField("Script Name:", scriptName);
            if (GUILayout.Button("Create"))
            {
                CreateScript(scriptName);
                Close();
            }
        }
        public static void CreateScript(string componentName)
        {
            if (string.IsNullOrEmpty(componentName))
            {
                Debug.LogError("Script name cannot be empty.");
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
            string templateContent = File.ReadAllText(templatePath);
            templateContent = templateContent.Replace("TemplateComponent", componentName);
            string scriptPath = Path.Combine(selectedFolderPath, componentName + ".cs");
            File.WriteAllText(scriptPath, templateContent);
            AssetDatabase.Refresh();
        }
    }
}