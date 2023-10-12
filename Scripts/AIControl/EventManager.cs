using System.Collections.Generic;
using UnityEngine;
public class EventManager : Singleton<EventManager>
{
    private EventLibrary eventLib;

    void Start()
    {
        eventLib = new EventLibrary();
        eventLib.AddEvent("捕捉飞行的昆虫|智力:1|年龄:10");
        eventLib.AddEvent("翻转树木寻找虫子|智力:3|年龄:25");
        eventLib.AddEvent("躲避来袭的风暴|智力:2|年龄:20");
        eventLib.AddEvent("发现水源|智力:1|年龄:15");
        eventLib.AddEvent("追赶漫游的兽群|智力:4|年龄:30");
        // 角色的属性
        Dictionary<string, int> attributes = new Dictionary<string, int>
        {
            { "智力", 7 },
            { "年龄", 21 },
            { "wealth", 12 }
        };
        // 查找匹配事件
        List<Event> matchingEvents = eventLib.FindMatchingEvents(attributes);
        // 输出匹配的事件
        Debug.Log("匹配的事件：");
        foreach (var evnt in matchingEvents)
        {
            Debug.Log(evnt.Name);
        }
    }
}