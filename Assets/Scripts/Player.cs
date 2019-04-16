using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Projectile projectile;

    public float Vision = 20f;  // Радиус обзора персонажа
    public float Speed = 0.07f; // Скорость перемещения персонажа

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("d"))
            transform.Translate(0,0,Speed);
        if (Input.GetKey("a"))
            transform.Translate(0,0,-Speed);
        if (Input.GetKey("w"))
            transform.Translate(-Speed,0,0);
        if (Input.GetKey("s"))
            transform.Translate(Speed,0,0);

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void Shot(GameObject enemy)
    {
        Debug.Log(string.Format("Was shot for the {0}\n{1}", enemy.name, System.DateTime.Now.ToString("h:mm:ss tt")));
        Instantiate(projectile, transform.position, Quaternion.identity).Launch(enemy.transform.position);
    }
}
