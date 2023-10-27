using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JH_ECS
{
    [InitializeOnLoad]
    public class ECSOnCompilationListener
    {
        public static bool CanReload = true;
        static ECSOnCompilationListener()
        {
            AssemblyReloadEvents.afterAssemblyReload += OnEditorAssemblyReload; 
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }
        private static void OnEditorAssemblyReload()
        {
            if (!CanReload) return;
            UnityBridge.ReloadAllEntitiesOnScene();
        }
        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            /*if (state == PlayModeStateChange.EnteredPlayMode)
            {
                List<Entity> keysToRemove = new List<Entity>();
        
                foreach (var kvp in EcsManager.CurrentWorld.EntitysController.Entities)
                {
                    if (kvp.Value.gameObject == null)
                    {
                        keysToRemove.Add(kvp.Value);
                    }
                }
                foreach (var key in keysToRemove)
                {
                    EcsManager.CurrentWorld.EntitysController.DestroyEntity(key);
                }
            }*/

            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                EcsManager.CurrentWorld.OnQuitMode();
                UnityBridge.ReloadAllEntitiesOnScene();
            }
        }
    }
}