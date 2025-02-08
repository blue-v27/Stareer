using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Update is called once per frame
    void FixedUpdate()
    {
        if(this.gameObject.tag == "fan")
            transform.Rotate(0,0,1);
    }
}
