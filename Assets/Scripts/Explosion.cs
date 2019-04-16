using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float liveTime;
    private float deltaTime;
	// Use this for initialization
	void Start () {
        deltaTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        deltaTime += Time.deltaTime;
        if (deltaTime >= liveTime)
            Destroy(gameObject);
	}
}
