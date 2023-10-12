using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TestM2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            ZFramework.EventManager.Default.Publish(new MessageBox{message = "我测1"});
        }
    }
}
