using System;
using UnityEditor;
using System.IO;
using UnityEngine;

namespace JH_ECS
{
    public class SystemScriptCreator : EditorWindow
    {
        private string templatePath = "Assets/Scripts/ECS/Systems/TemplateSystem.cs"; // 模板文件路径
        private string scriptName = "NewSystem"; // 新脚本的名称
        private string componentName = "NewComponent";

        [MenuItem("Assets/Create/ECS/System Script", false, 1)]
        public static void CreateSystemScript()
        {
            GetWindow<SystemScriptCreator>("Create System Script");
        }
        private void OnGUI()
        {
            GUILayout.Label("Create System Script", EditorStyles.boldLabel);
            scriptName = EditorGUILayout.TextField("Script Name:", scriptName);
            componentName = EditorGUILayout.TextField("BindComponent:", componentName);

            if (GUILayout.Button("Create"))
            {
                CreateScript();
                Close();
            }
        }

        private void CreateScript()
        {
            if (string.IsNullOrEmpty(scriptName))
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
            templateContent = templateContent.Replace("TemplateSystem", scriptName);
            templateContent = templateContent.Replace("TemplateComponent", componentName);
            string scriptPath = Path.Combine(selectedFolderPath, scriptName + ".cs");
            File.WriteAllText(scriptPath, templateContent);
            if (Type.GetType("JH_ECS." + componentName) == null)
            {
                ComponentScriptCreator.CreateScript(componentName);
            }
            AssetDatabase.Refresh();
        }
    }
}
