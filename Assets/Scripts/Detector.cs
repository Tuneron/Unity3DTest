using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Player player;
    private GameObject[] enemys;
    private bool gunIsFull = true;

    public float reloadTime = 6f;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gunIsFull)
            reloadTime -= Time.deltaTime;

        if (reloadTime <= 0)
        {
            gunIsFull = true;
            reloadTime = 6f;
        }

        FindEnemys();
    }

    public void FindEnemys()
    {
        if (gunIsFull)
            foreach (GameObject e in enemys)
                if (Vector3.Distance(player.transform.position, e.transform.position) <= player.Vision)
                {
                    player.Shot(e);
                    gunIsFull = false;
                }
    }
}
