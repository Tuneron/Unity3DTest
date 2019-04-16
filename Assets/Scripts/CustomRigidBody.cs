using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{
    private const float G = 9.8f;

    private Vector3 axis;
    private float angle;
    private float impulse;
    private float delataTime;

    public float gravityVertical;
    public bool gravity;
    public bool isStatic;

    // Use this for initialization
    void Start()
    {
        angle = 0.7f;
        impulse = 0f;
        delataTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gravity)
        {
            if (impulse > 0)
            {
                Throw(gravityVertical);
            }
            else
                Fall(gravityVertical);
        }
    }

    public void Fall(float vertical)
    {
        if (!isStatic)
        {
            transform.Translate(new Vector3(0f, (G * delataTime) * vertical, 0f));
            delataTime += (Time.deltaTime * (vertical * -1)) / 16;
        }
    }

    private void Throw(float vertical)
    {
        if (!isStatic)
        {
            transform.Translate(new Vector3(axis.x * SpeedX(impulse, angle),
                            SpeedY(impulse, angle, delataTime, vertical * -1),
                            axis.z * SpeedZ(impulse, angle)));
            delataTime += Time.deltaTime / 6;
        }
    }

    public void ThrowStart(float impulse, Vector3 axis)
    {
        if (gravity)
        {
            this.impulse = impulse;
            this.axis = axis;
            delataTime = 0f;
            isStatic = false;
            transform.Translate(0f, impulse + 2f, 0f);
        }
    }

    private float SpeedX(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle));
    }

    private float SpeedY(float startSpeedY, float angle, float currentTime, float side)
    {
        return (((startSpeedY * Mathf.Sin(angle)) - (G * currentTime)) * side);
    }
    private float SpeedZ(float startSpeedX, float angle)
    {
        return (startSpeedX * Mathf.Cos(angle));
    }
}
