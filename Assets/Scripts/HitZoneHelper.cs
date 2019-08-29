using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Hit Enemy Spike
        if (col.gameObject.tag == "Spike")
        {
            // do stuff here
            Debug.Log("HitZoneHelper " + col.gameObject.name);
            transform.parent.GetComponent<Enemy>().KillMe();
        }
    }
}
