using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JH_ECS
{
    public class TransformPositionTrackerHelper
    {
        private static Dictionary<Transform, EVector3> lastPositions = new Dictionary<Transform, EVector3>();

        public static void TrackTransform(Transform transform)
        {
            if (!lastPositions.ContainsKey(transform))
            {
              //  lastPositions[transform] = transform.position;
            }
        }
        /*
        public static bool CheckPositionChanges(Entity entity)
        {
            var transformComponent = entity.GetComponent<UnityTransformComponent>();
            if (transformComponent!=UnityTransformComponent.Empty)
            {
                Transform transform = transformComponent.transform;

                if (transform != null)
                {
                    if (lastPositions.TryGetValue(transform, out Vector3 lastPosition))
                    {
                        Vector3 currentPosition = transform.position;

                        if (currentPosition != lastPosition)
                        {
                            lastPositions[transform] = currentPosition;
                            return true; // 位置发生变化，返回 true
                        }
                    }
                    else
                    {
                        // 如果字典中没有记录这个 Transform，可以进行初始化操作
                        lastPositions[transform] = transform.position;
                    }
                }
            }
            return false; // 位置未发生变化，返回 false
        }
        */
    }
}