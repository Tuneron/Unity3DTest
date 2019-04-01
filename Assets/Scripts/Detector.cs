using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{

    private Player player;
    private GameObject[] Enemys;
    public float ReloadTime = 6f;
    private bool GunIsFull = true;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GunIsFull)
            ReloadTime -= Time.deltaTime;

        if (ReloadTime <= 0)
        {
            GunIsFull = true;
            ReloadTime = 6f;
        }

        FindEnemys();
    }

    public void FindEnemys()
    {
        if (GunIsFull)
            foreach (GameObject e in Enemys)
                if (Vector3.Distance(player.transform.position, e.transform.position) <= player.Vision)
                {
                    player.Shot(e);
                    GunIsFull = false;
                }
    }

    public void Blowup(Vector3 target, float explosionRadius)
    {
        foreach (GameObject e in Enemys)
            if (Vector3.Distance(target, e.transform.position) <= explosionRadius)
                Debug.Log(string.Format("Enemy {0} is dead by explosion !\n {1}", e.name, Vector3.Distance(target, e.transform.position)));

    }
}
