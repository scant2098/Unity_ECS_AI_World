using System;
using System.Collections.Generic;
using UnityEngine;
using ZFramework.IO;

namespace JH_ECS
{
    public class EcsWorld:IDisposable
    {
        public SystemsController SystemsController;
        public EntitysController EntitysController;
        public SpaceSplitHelper SpaceSplitHelper;

        private StorageTable _storageTableForWrite;
        //仅读取
        public StorageTable StorageTableForRead
        {
            get
            {
                if (_storageTableForWrite.IsNullOrEmpty())
                    return DataStorageHelper.ReadFromJsonFileByWorldName<StorageTable>(
                        EcsManager.CurrentWorld.WorldName);
                else
                    return _storageTableForWrite;
            }   
        }
        public float WorldSize;
        public string WorldName;

        public EcsWorld(string worldName,float worldSize=100000)
        {
            // 自定义类的代码
            WorldSize = worldSize;
            WorldName = worldName;
            EntitysController = new EntitysController();
            _storageTableForWrite = new StorageTable(new List<StorageUnit>(),worldName);
            DataStorageHelper.WriteToJsonFileByWorldName(worldName,_storageTableForWrite);
        }
        //带存储表的构造函数，不需要初始化存储表
        public EcsWorld(string worldName, StorageTable storageTable, float worldSize = 100000)
        {
            WorldSize = worldSize;
            WorldName = worldName;
            _storageTableForWrite = storageTable;
            EntitysController = new EntitysController();
        }
        public void StorageData()
        {
            Debug.Log(_storageTableForWrite.StorageUnits.Count);
            _storageTableForWrite.StorageData();
        }

        public void RemoveStorageUnit(string entityID)
        {
            _storageTableForWrite.RemoveStorageUnit(entityID);
        }

        public void AddStorageUnit(StorageUnit storageUnit)
        {
            _storageTableForWrite.AddStorageUnit(storageUnit);
        }
        public void OnPlayMode()
        {
            SystemsController = new SystemsController(); 
            _storageTableForWrite = DataStorageHelper.ReadFromJsonFileByWorldName<StorageTable>(WorldName);
            StorageTableForRead.LoadData();
            SpaceSplitHelper = new SpaceSplitHelper();
            SpaceSplitHelper.InitializeGrid();
        }

        public void OnQuitMode()
        {
            SystemsController.Systems.Clear();
            ZFramework.EventManager.Default.Dispose();
        }
        public void Dispose()
        {
            this.UnLoadWorld();
        }
    }
}