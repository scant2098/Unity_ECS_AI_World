using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGen : MonoBehaviour
{
    public GameObject Prefab;
    void Start()
    {
        for (int i = 0; i < 10000; i++)
        {
          GameObject obj=  Instantiate(Prefab, transform);
          obj.name = i.ToString();
          int x = Random.Range(0, 10);
          int y = Random.Range(0, 10);
          int z = Random.Range(0, 10);
          obj.transform.localScale /= 10;
          Vector3 vec = new Vector3(x, y, z);
          obj.transform.position = vec;
        }
    }

    void Update()
    {
        
    }
}
