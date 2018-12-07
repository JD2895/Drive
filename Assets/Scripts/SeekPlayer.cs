using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekPlayer : MonoBehaviour {

    public GameObject player_obj;
    public float movespeed;
    public float turn_rate;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward_dir = -transform.right;
        Vector3 player_dir = this.transform.position - player_obj.transform.position;
        float angle = Vector3.SignedAngle(forward_dir, player_dir, transform.forward);

        if (angle > 30.0)
        {
            transform.Rotate(Vector3.forward, turn_rate);
        }
        else if (angle < -30.00)
        {
            transform.Rotate(Vector3.forward, -turn_rate);
        }

        transform.position = transform.position + ((transform.right).normalized * movespeed);



    }
}