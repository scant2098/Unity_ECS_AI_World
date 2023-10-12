using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TypeManager:Singleton<TypeManager>
{
    private Dictionary<string, Func<object[], object>> _types = new Dictionary<string, Func<object[], object>>();

    public void OnInit()
    {
        _types.Add("bool", (par) => (bool)par[0]);
        _types.Add("char", (par) => (char)par[0]);
        _types.Add("double", (par) => (double)par[0]);
        _types.Add("float", (par) => (float)par[0]);
        _types.Add("int", (par) => (int)par[0]);
        _types.Add("string", (par) => (string)par[0]);
        _types.Add("Vector3", (par) => new Vector3((float)par[0], (float)par[1], (float)par[2]));
        _types.Add("Vector2", (par) => new Vector2((float)par[0], (float)par[1]));
        _types.Add("GameObject",(par) => GameObject.Find((string)par[0]));
    }
    public Func<object[], object> GetParameterBulidMethod(string typename)
    {
        if (_types.ContainsKey(typename))
        {
            return _types[typename];
        }
        Debug.LogError("此类型未加载:"+typename);
        return null;
    }
}
