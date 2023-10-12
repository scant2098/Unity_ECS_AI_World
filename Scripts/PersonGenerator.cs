using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JH_ECS;
namespace Generator
{
   public class PersonGenerator : Singleton<PersonGenerator>,IGenerator
   {
      //名字生成
      public List<PersonEntity> GeneratedPersonDatas = new List<PersonEntity>();
      private string _surnamespath = "StaticData/surnames";
      private string _givennamespath = "StaticData/given_names";
      private List<string> surnames = new List<string>();
      private List<string> givenNames = new List<string>();
      public PersonEntity GenerateNewPerson()
      {
         var name = GenerateName();
         var intelligence = Random.Range(0, 10);
         var health = Random.Range(0, 10);
         var famillybackground = Random.Range(0, 10);
         var apperance = Random.Range(0, 10);
         PersonEntity person = new PersonEntity(name, 0, 0, intelligence, health, famillybackground, apperance);
         GeneratedPersonDatas.Add(person);
         return person;
      }

      public PersonEntity GenerateRandomPerson()
      {
         var name = GenerateName();
         var age = Random.Range(0, 100);
         var grade = Random.Range(0, 10);
         //TODO：让四项属性的随机与age和grade产生关联，使其合理化
         var intelligence = Random.Range(0, 20);
         var health = Random.Range(0, 20);
         var famillybackground = Random.Range(0, 20);
         var apperance = Random.Range(0, 20);
         PersonEntity person = new PersonEntity(name, age, grade, intelligence, health, famillybackground, apperance);
         GeneratedPersonDatas.Add(person);
         return person;
      }
      private string GenerateName()
      {
         if (surnames.Count <= 0 || givenNames.Count <= 0) return "";
         string randomSurname = surnames[Random.Range(0, surnames.Count - 1)];
         string randomGivenName = givenNames[Random.Range(0, givenNames.Count - 1)];
         string fullName = randomSurname.Trim() + randomGivenName.Trim();
         return fullName;
      }
      //初始化
      private IEnumerator InitAsync()
      {
         var surnameRequest = Resources.LoadAsync<TextAsset>(_surnamespath);
         var givenNameRequest = Resources.LoadAsync<TextAsset>(_givennamespath);
         yield return new WaitUntil(() => surnameRequest.isDone && givenNameRequest.isDone);
         var surnameFile = surnameRequest.asset as TextAsset;
         var givenNameFile = givenNameRequest.asset as TextAsset;
         if (surnameFile != null && givenNameFile != null)
         {
            surnames = new List<string>(surnameFile.text.Split('\n'));
            givenNames = new List<string>(givenNameFile.text.Split('\n'));
         }
      }
      public void OnInit()
      {
        WorldGenerator.Instance.StartCoroutine(InitAsync());
      }
   }
}

