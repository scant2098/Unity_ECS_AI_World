using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH_ECS
{
   public enum RelationShip
 {
   STRANGER = 0,
   MOTHER = 1,
   FATHER = 2,
   FRIEND = 3
}
   public class PersonEntity:Entity
   {
   //基础信息
   public string Name;
   public int Age;
   public int Grade;
   //人物属性
   public int Intelligence;
   public int Health;
   public int FamilyBackGround;
   public int Appearance;
   //Others
   private Vector2 _postion;
   public Dictionary<RelationShip,List<PersonEntity>> RelationShips=new Dictionary<RelationShip, List<PersonEntity>>();
   public PersonEntity(string name, int age, int grade,int intelligence,int health,int familyBackGround,int appearance)
   {
      Name = name;
      Age = age;
      Grade = grade;
      Intelligence = intelligence;
      Health = health;
      FamilyBackGround = familyBackGround;
      Appearance = appearance;
   }

   public void BulidRelationShip(PersonEntity person,RelationShip relationShip)
   {
      //爸爸妈妈只能有一个
      if (relationShip == RelationShip.MOTHER || relationShip == RelationShip.MOTHER)
      {
         if(RelationShips.ContainsKey(relationShip)) return;
         RelationShips.Add(relationShip,new List<PersonEntity>(){person}); return;
      }
      //此类关系没有建立就新建列表
      if (!RelationShips.ContainsKey(relationShip)) { RelationShips.Add(relationShip,new List<PersonEntity>(){person}); return;}
      List<PersonEntity> list;
      RelationShips.TryGetValue(relationShip,out list);
      if(list.Count>0) list.Add(person);
   }
   public void BulidRelationShip(PersonEntity person,int relationShipID)
   {
      var relationShip = (RelationShip)relationShipID;
      //爸爸妈妈只能有一个
      if (relationShip == RelationShip.FATHER || relationShip == RelationShip.MOTHER)
      {
         if(RelationShips.ContainsKey(relationShip)) return;
         RelationShips.Add(relationShip,new List<PersonEntity>(){person}); return;
      }
      //此类关系没有建立就新建列表
      if (!RelationShips.ContainsKey(relationShip)) { RelationShips.Add(relationShip,new List<PersonEntity>(){person}); return;}
      List<PersonEntity> list;
      RelationShips.TryGetValue(relationShip,out list);
      if(list.Count>0) list.Add(person);
   }


   public void ShowInfo()
   {
      string baseInfo = "名字:" + Name + "," + "年龄:" + Age + "," + "等级:" + "\n" + Grade
                        + "智力:" + Intelligence + "," + "体质:" + Health + "," + "家境:" + FamilyBackGround + "," + "颜值:" +
                        Appearance + "\n";
      string realtionship="";
      foreach (var realtion in RelationShips)
      {
         realtionship += Enum.GetName(typeof(RelationShip),realtion.Key)+":";
         foreach (var person in realtion.Value)
         {
            realtionship += person.Name + ", ";
         }
         realtionship += "\n";
      }
      Debug.Log(baseInfo+realtionship);
   }
  }
}
