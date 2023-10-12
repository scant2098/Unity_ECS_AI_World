using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MessageBox
{ 
    public string message;
}
public class TestM1 : MonoBehaviour
{
    void Start()
    {
        ZFramework.EventManager.Default.Receive<MessageBox>(_ =>
        {
            Debug.LogError(_.message);
        });
    }
    void Update()
    {
    }
}
