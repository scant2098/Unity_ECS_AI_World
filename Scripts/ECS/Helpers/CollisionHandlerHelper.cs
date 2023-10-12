using System;
using System.Collections.Generic;
using UnityEngine;
namespace JH_ECS
{
    public class DoubleKeyDictionary<TKey1, TKey2, TValue>
    {
        private Dictionary<Tuple<TKey1, TKey2>, TValue> dictionary = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        public void Add(TKey1 key1, TKey2 key2, TValue value)
        {
            Tuple<TKey1, TKey2> combinedKey = Tuple.Create(key1, key2);
            dictionary[combinedKey] = value;
        }

        public bool TryGetValue(TKey1 key1, TKey2 key2, out TValue value)
        {
            Tuple<TKey1, TKey2> combinedKey = Tuple.Create(key1, key2);
            return dictionary.TryGetValue(combinedKey, out value);
        }

        public bool Contains(TKey1 key1, TKey2 key2)
        {
            Tuple<TKey1, TKey2> combinedKey = Tuple.Create(key1, key2);
            return dictionary.ContainsKey(combinedKey);
        }

        public bool Remove(TKey1 key1, TKey2 key2)
        {
            Tuple<TKey1, TKey2> combinedKey = Tuple.Create(key1, key2);
            return dictionary.Remove(combinedKey);
        }
    }
    public class CollisionHandlerHelper
    {
        private static DoubleKeyDictionary<Entity, Entity,bool> EntitiesCollisionInfo = new DoubleKeyDictionary<Entity, Entity,bool>();
        public static void AddCollisionInfo(Entity entity1, Entity entity2)
        {
            if (!EntitiesCollisionInfo.Contains(entity1, entity2))
            {
                EntitiesCollisionInfo.Add(entity1,entity2,true);
            }
        }
        public static void RemoveCollisionInfo(Entity entity1,Entity entity2)
        {
            if (EntitiesCollisionInfo.Contains(entity1, entity2))
            {
                EntitiesCollisionInfo.Remove(entity1,entity2);
            }
        }
        /// <summary>
        /// 检查两个物体已经产生了碰撞并且是否正在持续碰撞
        /// </summary>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <returns></returns>
        public static bool CheckEntitiesCollision(Entity entity1, Entity entity2)
        {
            return EntitiesCollisionInfo.Contains(entity1,entity2); 
        }
        public static bool CheckCollision(Transform transform1, Transform transform2)
        {
            // 获取两个物体的位置和尺寸信息
            Vector3 position1 = transform1.position;
            Vector3 position2 = transform2.position;

            Vector3 size1 = transform1.localScale;
            Vector3 size2 = transform2.localScale;

            // 计算两个物体的边界框（AABB）
            Vector3 min1 = position1 - size1 * 0.5f;
            Vector3 max1 = position1 + size1 * 0.5f;
            Vector3 min2 = position2 - size2 * 0.5f;
            Vector3 max2 = position2 + size2 * 0.5f;

            // 检测重叠
            if (max1.x > min2.x && min1.x < max2.x &&
                max1.y > min2.y && min1.y < max2.y &&
                max1.z > min2.z && min1.z < max2.z)
            {
                // 碰撞发生
                return true;
            }

            // 没有碰撞
            return false;
        }

    }
}