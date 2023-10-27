using System;
using UnityEngine;
using ZFramework.IO;

namespace JH_ECS
{
    public class EcsStartPoint:MonoBehaviour
    {
        private void Start()
        {
            var gameobjects = GameObject.FindObjectsOfType<EntityBehaviour>();
            foreach (var entityBehaviour in gameobjects)
            {
                Destroy(entityBehaviour.gameObject);
            }
            EcsManager.CurrentWorld.OnPlayMode(); 
        }

        private void Update()
        {
        }

        private void OnApplicationQuit()
        {
          //  EcsManager.CurrentWorld.OnQuitMode();
        }
    }
}