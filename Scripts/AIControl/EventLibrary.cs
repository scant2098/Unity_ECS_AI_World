using System;
using System.Collections.Generic;
// 表示事件的触发条件
public class EventCondition
{
    public string Attribute { get; set; }
    public int MinValue { get; set; }
}

public class Event
{
    public string Name { get; set; }
    public List<EventCondition> Conditions { get; set; }

    public Event(string name)
    {
        Name = name;
        Conditions = new List<EventCondition>();
    }
    public Event(string name, List<EventCondition> conditions)
    {
        Name = name;
        Conditions = conditions;
    }
    public void AddCondition(string attribute, int minValue)
    {
        Conditions.Add(new EventCondition { Attribute = attribute, MinValue = minValue });
    }
}
// 事件库，包含多个事件
public class EventLibrary
{
    private List<Event> events;

    public EventLibrary()
    {
        events = new List<Event>();
    }

    public void AddEvent(Event newEvent)
    {
        events.Add(newEvent);
    }

    public Event AddEvent(string eventData)
    {
        string[] parts = eventData.Split('|');
        if (parts.Length < 2)
        {
            throw new ArgumentException("Invalid event data format");
        }

        Event newEvent = new Event(parts[0]);
        for (int i = 1; i < parts.Length; i++)
        {
            string[] conditionData = parts[i].Split(':');
            if (conditionData.Length == 2 && int.TryParse(conditionData[1], out int minValue))
            {
                newEvent.AddCondition(conditionData[0], minValue);
            }
        }

        events.Add(newEvent);
        return newEvent;
    }
    // 查找与角色属性匹配的事件
    public List<Event> FindMatchingEvents(Dictionary<string, int> attributes)
    {
        List<Event> matchingEvents = new List<Event>();
        foreach (Event evnt in events)
        {
            if (EventMatchesConditions(evnt, attributes))
            {
                matchingEvents.Add(evnt);
            }
        }
        return matchingEvents;
    }

    // 检查事件的触发条件是否满足
    private bool EventMatchesConditions(Event evnt, Dictionary<string, int> attributes)
    {
        foreach (var condition in evnt.Conditions)
        {
            if (!attributes.ContainsKey(condition.Attribute) || attributes[condition.Attribute] < condition.MinValue)
            {
                return false;
            }
        }
        return true;
    }
}
