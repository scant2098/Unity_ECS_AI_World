using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using JH_ECS;
public class ActionItem
{
    private Delegate _executeAction;
    public string ActionName;

    public void Execute(params object[] parameters)
    {
        _executeAction.DynamicInvoke(parameters);
    }
    public ActionItem(string name, Delegate executeAction)
    {
        ActionName = name;
        _executeAction = executeAction;
    }
}
public class ActionLibrary
{
    private static Dictionary<string, ActionItem> ActionDic = new Dictionary<string, ActionItem>();

    public ActionLibrary()
    {
        TypeManager.Instance.OnInit();
      //  AddAction(new ActionItem("移动", new Action<GameObject,Vector3, float>(MoveSystem.Execute)));

    }
    #region CRUD
    public void AddAction(ActionItem actionItem)
    {
        if(ActionDic.ContainsKey(actionItem.ActionName)) return;
        ActionDic.Add(actionItem.ActionName,actionItem);
    }
    public static ActionItem FindActionByName(string name)
    {
        if(ActionDic.ContainsKey(name)) return ActionDic[name];
        return null;
    }
    public void DeleteAction(string actionName)
    {
        if(!ActionDic.ContainsKey(actionName)) return;
        ActionDic.Remove(actionName);
    }
    #endregion
    public static List<Action> ChooseWillExecuteActions(string eventBody)
    {
        var list = new List<Action>();
        var recive = AIBrain.ThinkEvent(eventBody);
        var actionStrs = recive.Split("->");
        foreach (var actionStr in actionStrs)
        {
            var action = FindActionByName(eventBody);
            //切割参数0
            var parameterStrs = actionStr.Remove(0, 3).Split(";");
            object[] parameters = new object[parameterStrs.Length];
            for (int i = 0; i < parameterStrs.Length; i++)
            {
                parameters[i] = CreateObjectFromString(parameterStrs[i]);
            }
            list.Add(() =>
            {
                action.Execute(parameters);
            });
        }
        return list;
    }
    static object CreateObjectFromString(string input)
    {
        int indexOfOpenParenthesis = input.IndexOf('(');
        if (indexOfOpenParenthesis != -1)
        {
            string typeName = input.Substring(0, indexOfOpenParenthesis);
            string parametersString = input.Substring(indexOfOpenParenthesis + 1, input.Length - indexOfOpenParenthesis - 2);
            var constructorParameters = ParseParameters(parametersString);
            var objvalue = TypeManager.Instance.GetParameterBulidMethod(typeName);
            var obj = objvalue(constructorParameters);
            return obj;
        }
        return null;
    }
    static object[] ParseParameters(string parametersString)
    {
        string[] parameterValues = parametersString.Split(',');
        object[] parsedParameters = new object[parameterValues.Length];
        for (int i = 0; i < parameterValues.Length; i++)
        {
            parsedParameters[i] = ParseParameter(parameterValues[i].Trim());
        }
        return parsedParameters;
    }
    static object ParseParameter(string value)
    {
        if (float.TryParse(value, out float floatValue))
        {
            return floatValue;
        }
        else if (int.TryParse(value, out int intValue))
        {
            return intValue;
        }
        else if (bool.TryParse(value, out bool boolvalue))
        {
            return boolvalue;
        }
        else
        {
            return value;
        }
    }
}
