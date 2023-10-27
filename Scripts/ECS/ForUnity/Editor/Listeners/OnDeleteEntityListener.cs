using UnityEditor;
using UnityEngine;

namespace JH_ECS
{
    [InitializeOnLoad]
    public static class OnDeleteEntityListener
    {
        static OnDeleteEntityListener()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyChanged()
        {
            GameObject[] deletedObjects = Selection.gameObjects;
            foreach (GameObject deletedObject in deletedObjects)
            {
                if (deletedObject.GetComponent<EntityBehaviour>() != null)
                {
                    HandleDeletion(deletedObject);
                }
                
            }
        }

        private static void HandleDeletion(GameObject deletedObject)
        {
            Debug.Log("Deleted object with YourComponent: " + deletedObject.name);
        }
    }
}