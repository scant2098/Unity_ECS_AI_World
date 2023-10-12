using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rig;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = 0;
        float v = 0;
        h=Input.GetAxis("Horizontal"); 
        v=Input.GetAxis("Vertical");
        rig.AddForce(new Vector3(h,0,v).normalized*100);
    }

}
