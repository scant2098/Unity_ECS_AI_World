using System.Collections.Generic;
using System.Linq;
using JH_ECS;
using Newtonsoft.Json;
using UnityEngine;
using ZFramework.IO.CustomerConvert;

namespace ZFramework.IO
{
    public struct StorageTable
    {
        public List<StorageUnit> StorageUnits;
        public string WorldName;

        public StorageUnit FindStorageUnit(string entityID)
        {
            return StorageUnits.Find(unit => unit.Id == entityID);
        }
        public void AddStorageUnit(StorageUnit storageUnit)
        {
            if (StorageUnits.Contains(storageUnit))
            {
                StorageUnits.Remove(storageUnit);
                StorageUnits.Add(storageUnit);
            }
        }

        public void RemoveStorageUnit(string entityID)
        {
            var unit=StorageUnits.Find(unit => unit.Id == entityID);
            if (unit != StorageUnit.Null)
            {
                StorageUnits.Remove(unit);
            }
        }
        public StorageTable(List<StorageUnit> storageUnits,string worldName)
        {
            StorageUnits = storageUnits;
            WorldName = worldName;
        }

        public void LoadData()
        { 
            if(StorageUnits==null) return;
            var list = new List<StorageUnit>(StorageUnits);
            list.ForEach(unit =>
            {
                var entity = EcsManager.CurrentWorld.EntitysController.CreateEntity(unit);
                entity.CreateStorageUnit();
            });
        }
        public void StorageData()
        {
            DataStorageHelper.WriteToJsonFileByWorldName(EcsManager.CurrentWorld.WorldName,this);
        }

        public bool IsNullOrEmpty()
        {
            return StorageUnits == null||StorageUnits.Count<=0;
        }
    }
}