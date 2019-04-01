using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public  GameObject player;
    public  GameObject explosion;
    private Vector3 target;
    private bool isLaunch = false;

    private const float g = 9.8f;

    private const float flyTimeDelay = 0.025f;
    private bool flyDelay = false;
    private float deltaTime = 0;

    private float speed;
    private float angle = 1.3f;
    private Vector3 axis;

    private float currentTimeFly = 0;
    private float currentBounce = 0;


	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        explosion = GameObject.FindGameObjectWithTag("Explosion");
	}

    // Update is called once per frame
    void Update() {

        if (deltaTime >= flyTimeDelay)
        {
            deltaTime = 0;
            flyDelay = false;
        }

        if (!isLaunch)
        {
            transform.position = new Vector3(
                player.transform.position.x, 2.5f, player.transform.position.z);
        }
        else
        {
            deltaTime += Time.deltaTime;

            if (!flyDelay)
            {
                transform.Translate(new Vector3(axis.x * SpeedX(speed, angle),
                                                SpeedY(speed, angle, currentTimeFly, 1),
                                                axis.z * SpeedZ(speed, angle)));
                currentTimeFly += Time.deltaTime;
                flyDelay = true;
            }
        }

        if (transform.position.y < 2.5f)
        {
            currentBounce++;
            currentTimeFly = 0;

            transform.position = new Vector3(transform.position.x, 2.5f, transform.position.z);
            explosion.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

            speed *= 0.8f;
        }

        if (currentBounce == 3)
        {
            currentBounce = 0;
            isLaunch = false;
        }
	}

    public void Launch(Vector3 target)
    {
        this.target = target;
        axis = Vector3.Normalize(target - player.transform.position);

        speed = Mathf.Sqrt((Vector3.Distance(player.transform.position, target) * g) / Mathf.Sin(2 * angle)) / 7.8f;

        Debug.Log(string.Format("angle degree {0}", (Vector3.Angle(player.transform.position, target))));
        Debug.Log(string.Format("angle cos {0}", Mathf.Cos(Vector3.Angle(player.transform.position, target) * Mathf.Deg2Rad)));
        Debug.Log(string.Format("angle sin {0}", Mathf.Sin(Vector3.Angle(player.transform.position, target) * Mathf.Deg2Rad)));
        Debug.Log(string.Format("angle tan {0}", Mathf.Tan(Vector3.Angle(player.transform.position, target) * Mathf.Deg2Rad)));

        isLaunch = true;
    }

    private float SpeedX(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle));
    }

    private float SpeedY(float startSpeedY, float angle, float currentTime, float side)
    {
        return ((startSpeedY * Mathf.Sin(angle)) - (g * currentTime)) * side;
    }

    private float SpeedZ(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle));
    }

}
