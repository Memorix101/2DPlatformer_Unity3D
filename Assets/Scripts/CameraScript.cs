using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    private Transform player;
    private Transform exit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
            try
            {
                player = GameObject.FindWithTag("Player").transform;
            }
            catch (Exception e)
            {
                Debug.LogError("Well, cannot find Player gameobject");
            }
        }
        else
        {
            if (player.transform.position.x >= 2.5f)
            {
                transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(player.GetComponent<Player>().getStartPos.x + 2.5f, transform.position.y, transform.position.z);
            }
        }

        if (!exit)
        {
            try
            {
                exit = GameObject.FindWithTag("Exit").transform;
            }
            catch (Exception e)
            {
                Debug.LogError("Well, cannot find Player gameobject");
            }
        }
        else
        {
            if (player.transform.position.x > exit.transform.position.x - 2.5f)
            {
                transform.position = new Vector3(exit.transform.position.x - 2.5f, transform.position.y, transform.position.z);
            }
        }
    }
}
