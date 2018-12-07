using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAtDistanceFromPlayer : MonoBehaviour {

    public Transform player;
    public float MaxDistance = 10.0f;

    public GameController other;
 
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameObject go = GameObject.Find("GameController");
        other = (GameController)go.GetComponent(typeof(GameController));
    }

    void FixedUpdate()
    {
        Distance_To_Player();
    }

    void Distance_To_Player()
    {
        if (Vector3.Distance(player.position, transform.position) > MaxDistance)
        {
            other.destroy_haz();
            Destroy(gameObject);
        }
    }
}
