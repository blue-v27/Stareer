using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("DestoryAfterTime", time);
    }

    private void FixedUpdate() 
    {
        transform.localScale *= 1.02f;
    }

    // Update is called once per frame
    void DestoryAfterTime()
    {
        Destroy(gameObject);
    }
}
