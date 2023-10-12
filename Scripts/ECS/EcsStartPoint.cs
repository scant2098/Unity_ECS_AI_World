using System;
using UnityEngine;
namespace JH_ECS
{
    public class EcsStartPoint:MonoBehaviour
    {
        private SystemsController _systemsController;
        private void Start()
        {
            EcsManager.CreateWorld("NewEcsWorld");
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
            }
        }
    }
}