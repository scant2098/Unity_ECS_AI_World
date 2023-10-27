using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using ZFramework.IO;

namespace JH_ECS
{
    public class WorldDeletionListener:AssetPostprocessor
    {
        // 在EcsWorld文件被删除时触发
        private static void OnPostprocessAllAssets(
            string[] importedAssets,
            string[] deletedAssets,
            string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            foreach (var deletedAsset in deletedAssets)
            {
                if (deletedAsset.EndsWith(".unity"))
                {
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(deletedAsset);
                    UnSceneDelete(sceneName);
                }
            }
        }
        private static void UnSceneDelete(string sceneName)
        {
            if (EcsManager.HasWorld(sceneName))
            {
                EcsManager.GetWorld(sceneName).Dispose();
                File.Delete(Path.Combine(DataStorageHelper.Path,sceneName+".json"));
                Debug.Log(Path.Combine(DataStorageHelper.Path,sceneName+".json"));
                AssetDatabase.Refresh();
            }
        }
    }
}