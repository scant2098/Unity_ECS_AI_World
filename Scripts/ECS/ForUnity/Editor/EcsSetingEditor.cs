using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace JH_ECS
{
   

    public class EcsSetingEditor : EditorWindow
    {
        [SerializeField]
        private ReorderableList draggableItemsList;
        private SerializedObject serializedObject;
        public List<string> SystemOrder;

        [MenuItem("ECS/ECS Settings")]
        public static void OpenCustomWindow()
        {
            EcsSetingEditor window = GetWindow<EcsSetingEditor>();
            window.titleContent = new GUIContent("ECS Settings");
            window.Show();
        }

        private void OnEnable()
        {
            SystemOrder = EcsSetting.SystemOrder;
            serializedObject = new SerializedObject(this); // 初始化 serializedObject

            // 初始化可拖拽的列表
            draggableItemsList = new ReorderableList(serializedObject, serializedObject.FindProperty("SystemOrder"), true, true, true, true);

            // 隐藏自带的 "+" 和 "-" 按钮
            draggableItemsList.displayAdd = false;
            draggableItemsList.displayRemove = false;

            draggableItemsList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "系统执行顺序");
            draggableItemsList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = draggableItemsList.serializedProperty.GetArrayElementAtIndex(index);
                // 绘制不可编辑的文本标签
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element.stringValue);
            };

            // 监听拖拽操作
            // 监听拖拽操作
            draggableItemsList.onReorderCallback = list =>
            {
                List<string> newOrder = new List<string>();
                var systemOrderProperty = serializedObject.FindProperty("SystemOrder");

                for (int i = 0; i < list.serializedProperty.arraySize; i++)
                {
                    newOrder.Add(systemOrderProperty.GetArrayElementAtIndex(list.index).stringValue);
                }
                EcsSetting.SystemOrder = newOrder;
                Debug.Log(EcsSetting.SystemOrder[0]);
            };

        }

        private void OnGUI()
        {
            GUILayout.Label("ECS Settings", EditorStyles.boldLabel);

            // 更新SerializedObject
            serializedObject.Update();

            // 显示可拖拽的列表
            draggableItemsList.DoLayoutList();

            // 应用修改
            serializedObject.ApplyModifiedProperties();

            GUILayout.Space(100);
        }

        private void OnInspectorUpdate()
        {
            Repaint(); // 刷新Inspector
        }
    }
}
