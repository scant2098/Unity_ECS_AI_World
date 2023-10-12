using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreate : MonoBehaviour
{
    public GameObject m_Prefab;
    public int InitCount;
    public Vector3 startPos;
    void Start()
    {
        CreateObjs();
    }
    private void CreateObjs()
    {
        for (int x = 0; x < Mathf.Pow(InitCount, 1.0f / 3.0f); x++)
        {
            for (int y = 0;  y< Mathf.Pow(InitCount, 1.0f / 3.0f); y++)
            {
                for (int z = 0; z < Mathf.Pow(InitCount, 1.0f / 3.0f); z++)
                {
                    Instantiate(m_Prefab, new Vector3(startPos.x + x, startPos.y + y, startPos.z + z),
                        Quaternion.identity);
                }
            }
        }
    }
    void Update()
    {
        
    }
}
