using System.Collections;
using UnityEngine;
using UniRx;
using JH_ECS;

public class WorldGenerator : MonoBehaviour
{
    private static WorldGenerator instance;
    public static WorldGenerator Instance
    {
        get { return instance; }
    }
    public ReactiveProperty<int> _generateCount=new ReactiveProperty<int>(0);
    private bool isGenerating = false;
    void Awake()
    {
        instance = this;
        Generator.PersonGenerator.Instance.OnInit();
    }
    private void Start()
    {
        //初始化事件库
        var actionLibrary = new ActionLibrary();
        /*var list = actionLibrary.ChooseWillExecuteActions("移动");
        list[0]();*/
    }
    private IEnumerator ShowPersonsInfo()
    {
        var list = Generator.PersonGenerator.Instance.GeneratedPersonDatas;
        for (int i = 1; i < list.Count/100+1; i++)
        {
            for (int j = 1; j < 101; j++)
            {
                list[j*i-1].ShowInfo();
            }
            yield return null;
        }
    }
    private IEnumerator GeneratePersons(int count)
    {
        //生成角色数据
        isGenerating = true;
        for (int i = 0; i < count/100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                CreateAndPrintPerson();
            }
            yield return null;
        }
        //随机建立角色之间的关系
        var list = Generator.PersonGenerator.Instance.GeneratedPersonDatas;
        for (int i = 1; i < list.Count/100+1; i++)
        {
            for (int m = 1; m < 101; m++)
            {
               BulidRelationShip(list[i*m-1]);
            }
            yield return null;
        }
        isGenerating = false;
    }
    private void BulidRelationShip(PersonEntity person)
    {
        int bulidCount = UnityEngine.Random.Range(0, 10);
        for (int j = 0; j < bulidCount; j++)
        {
            int randomPersonID =
                UnityEngine.Random.Range(0, Generator.PersonGenerator.Instance.GeneratedPersonDatas.Count);
            int randomRealtionship = UnityEngine.Random.Range(0, 4);
            person.BulidRelationShip(Generator.PersonGenerator.Instance.GeneratedPersonDatas[randomPersonID],randomRealtionship);
        }
    }
    private void CreateAndPrintPerson()
    {
        PersonEntity personEntity = Generator.PersonGenerator.Instance.GenerateRandomPerson();
        _generateCount.Value++;
    }
}
