using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public GameObject player;
    public int max_haz;
    public float difficulty;
    public int haz_count;

    // Use this for initialization
    void Start()
    {
        haz_count = 0;
        StartCoroutine(spawnHazards());

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnHazards()
    {
        for (; haz_count < max_haz; haz_count++)
        {
            Vector3 spawn_pos;
            spawn_pos = Camera.main.ViewportToWorldPoint(new Vector3(((Random.value + (Random.value * 2)) - 1f), 1.4f, 1.0f));
            
            Quaternion spawn_rot = Quaternion.identity;
            GameObject new_haz = Instantiate(hazard, spawn_pos, spawn_rot);
            new_haz.GetComponent<SeekPlayer>().movespeed = Random.Range((0.01f + difficulty / 100), (0.05f + difficulty / 100));
            new_haz.GetComponent<SeekPlayer>().turn_rate = Random.Range((1.0f + difficulty / 10), (1.6f + difficulty / 10));
            new_haz.GetComponent<SeekPlayer>().player_obj = player;

            Vector3 forward_dir = -new_haz.transform.right;
            Vector3 player_dir = new_haz.transform.position - player.transform.position;
            float angle = Vector3.SignedAngle(forward_dir, player_dir, transform.forward);
            new_haz.transform.Rotate(Vector3.forward, angle);

            difficulty += 1.0f;

            yield return new WaitForSeconds(0.3f);
        }


    }

    public void destroy_haz()
    {
        haz_count--;
        return;
    }
}
