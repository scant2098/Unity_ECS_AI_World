using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Profiling;

namespace JH_ECS
{
    public class EntityGridInfo
    {
        public int GridX;
        public int GridY;
        public int GridZ;
        public EntityGridInfo(int x, int y, int z)
        {
            GridX = x;
            GridY = y;
            GridZ = z;
        }
    }
    public class SpaceSplitHelper
    {
        public int gridSize = 10; // 网格的X轴列数
        private List<Entity>[,,] grid; 
        private Dictionary<Entity,EntityGridInfo> _entityGridInfos=new Dictionary<Entity, EntityGridInfo>();
        public void InitializeGrid()
        {
            gridSize = (int)EcsManager.CurrentWorld.WorldSize / 1000;
            grid = new List<Entity>[gridSize, gridSize, gridSize];
            // 创建网格
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int z = 0; z < gridSize; z++)
                    {
                        grid[x, y, z] = new List<Entity>();
                    }
                }
            }
        }
        public void ShowGridInfo()
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    for (int z = 0; z < gridSize; z++)
                    {
                        // 获取当前网格单元格中的实体数量
                        int entityCount = grid[x, y, z].Count;
                        // 打印信息
                        if(entityCount!=0)
                          Debug.Log($"Grid [{x}, {y}, {z}] contains {entityCount} entities.");
                    }
                }
            }
        }
        public void RefreshGridEntityInfo(Entity entity)
        {
            // 获取实体的当前位置
            EVector3 currentPosition = entity.GetComponent<PositionComponent>().position;
            // 计算实体在新位置应该位于的网格单元格坐标
            int newX = Mathf.FloorToInt(currentPosition.x / gridSize);
            int newY = Mathf.FloorToInt(currentPosition.y / gridSize);
            int newZ = Mathf.FloorToInt(currentPosition.z / gridSize);
            // 获取实体之前所在的网格单元格坐标
            int oldX = _entityGridInfos[entity].GridX;
            int oldY = _entityGridInfos[entity].GridY;
            int oldZ = _entityGridInfos[entity].GridZ;
            // 如果实体从一个网格单元格移动到另一个网格单元格
            if (newX != oldX || newY != oldY || newZ != oldZ)
            {
                // 从旧网格单元格中删除实体
                grid[oldX, oldY, oldZ].Remove(entity);
                // 添加实体到新的网格单元格
                try
                {
                    grid[newX, newY, newZ].Add(entity);
                    // 更新实体的网格坐标
                    _entityGridInfos[entity].GridX = newX;
                    _entityGridInfos[entity].GridY= newY;
                    _entityGridInfos[entity].GridZ = newZ;
                }
                catch
                {
                }
            }
        }
        public void AddObjectToGrid(Entity entity)
        {
            // 根据游戏对象的位置将其添加到网格中的相应单元格
            Vector3 vector3 = entity.gameObject.transform.position;
            EVector3 position = new EVector3(vector3.x, vector3.y, vector3.z);
            int x = Mathf.FloorToInt(position.x / gridSize);
            int y = Mathf.FloorToInt(position.y / gridSize);
            int z = Mathf.FloorToInt(position.z / gridSize);
            // 确保x、y和z的值在合法范围内
            x = Mathf.Clamp(x, 0, gridSize - 1);
            y = Mathf.Clamp(y, 0, gridSize - 1);
            z = Mathf.Clamp(z, 0, gridSize - 1); 
            _entityGridInfos.Add(entity,new EntityGridInfo(x,y,z));
            grid[x, y, z].Add(entity);
        }
        public List<Entity> GetObjectsInGrid(EVector3 position)
        {
            // 根据位置获取网格中的游戏对象列表
            int x = Mathf.FloorToInt(position.x / gridSize);
            int y = Mathf.FloorToInt(position.y / gridSize);
            int z = Mathf.FloorToInt(position.z / gridSize);
            // 确保x、y和z的值在合法范围内
            x = Mathf.Clamp(x, 0, gridSize - 1);
            y = Mathf.Clamp(y, 0, gridSize - 1);
            z = Mathf.Clamp(z, 0, gridSize - 1);
            return grid[x, y, z];
        }
        public List<Entity> GetNearlyEntity (Entity entity)
        {
            var position = entity.GetComponent<PositionComponent>().position;
            // 根据位置获取网格中的游戏对象列表
            int x = Mathf.FloorToInt(position.x / gridSize);
            int y = Mathf.FloorToInt(position.y / gridSize);
            int z = Mathf.FloorToInt(position.z / gridSize);
            // 确保x、y和z的值在合法范围内
            x = Mathf.Clamp(x, 0, gridSize - 1);
            y = Mathf.Clamp(y, 0, gridSize - 1);
            z = Mathf.Clamp(z, 0, gridSize - 1);
            var list = grid[x, y, z].Where(_ => _ != entity).ToList();
            return list;
        }
    }
}