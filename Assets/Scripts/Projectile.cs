using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private const float G = 9.8f;

    private GlobalCollision collision;
    private Vector3 target;
    private Vector3 axis;
    private bool flyDelay = false;
    private bool isLaunch = false;
    private float angle = 1.3f;
    private float explosinRadius;
    private float deltaTime = 0;
    private float speed;
    private float currentTimeFly = 0;
    private float currentBounce = 0;

    public GameObject explosion;
    public float flyHight;
    public float flySpeed;
    public float explosinPower;
    public float flyTimeDelay;

	// Use this for initialization
	void Start () {
        collision = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GlobalCollision>();
        explosinRadius = explosion.transform.localScale.x;
	}

    // Update is called once per frame
    void Update() {

        if (deltaTime >= flyTimeDelay)
        {
            deltaTime = 0;
            flyDelay = false;
        }

        if (isLaunch)
        {
            deltaTime += Time.deltaTime;

            if (!flyDelay)
            {
                transform.Translate(new Vector3(axis.x * SpeedX(speed, angle) * flyHight,
                                                SpeedY(speed, angle, currentTimeFly, 1),
                                                axis.z * SpeedZ(speed, angle) * flyHight));
                currentTimeFly += Time.deltaTime * flyHight;
                flyDelay = true;
            }
        }

        if (transform.position.y < target.y)
        {
            collision.Blowup(transform.position, explosinRadius, explosinPower);

            currentBounce++;
            currentTimeFly = 0;

            transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
            Instantiate(explosion, new Vector3(transform.position.x, target.y - 1.5f, transform.position.z), Quaternion.identity);

            speed *= 0.8f;
        }

        if (currentBounce == 3)
        {
            Destroy(gameObject);
        }
	}

    public void Launch(Vector3 target)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        this.target = target;
        axis = Vector3.Normalize(target - player.transform.position);

        speed = Mathf.Sqrt((Vector3.Distance(player.transform.position, target) * G) / Mathf.Sin(2 * angle)) / 7.8f;

        isLaunch = true;
    }

    private float SpeedX(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle)) ;
    }

    private float SpeedY(float startSpeedY, float angle, float currentTime, float side)
    {
        return (((startSpeedY * Mathf.Sin(angle)) - (G * currentTime)) * side );
    }

    private float SpeedZ(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle)) ;
    }

}
