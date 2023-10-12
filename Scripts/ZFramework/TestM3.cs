using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestM3 : MonoBehaviour
{
    void Start()
    {
        ZFramework.EventManager.Default.Receive<MessageBox>(_ =>
        {
            Debug.Log(_.message);
        });
    }
    void Update()
    {
        
    }
}
