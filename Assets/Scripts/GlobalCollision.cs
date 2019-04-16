using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCollision : MonoBehaviour
{
    private Player player;

    public GameObject[] colliders;
    public CustomRigidBody[] rigidBodys;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int j = i + 1; j < colliders.Length; j++)
            {
                if (CheckYCollision(colliders[i].transform, colliders[j].transform))
                {
                    rigidBodys[i].isStatic = true;
                }
                else
                {
                    rigidBodys[i].isStatic = false;
                }
            }
        }
    }

    public bool CheckYCollision(Transform firstCollider, Transform secondCollider)
    {
        return (firstCollider.position.y - firstCollider.localScale.y  <= secondCollider.position.y - secondCollider.localScale.y);
    }

    public void Blowup(Vector3 target, float explosionRadius, float explosinPower)
    {
        foreach (GameObject e in colliders)
        {
            if (Vector3.Distance(target, e.transform.position) <= explosionRadius)
            {
                Debug.Log(string.Format("Enemy {0} is dead by explosion !\n {1}", e.name, Vector3.Distance(target, e.transform.position)));
                e.GetComponent<CustomRigidBody>().ThrowStart(explosinPower, Vector3.Normalize(target - e.transform.position) * -1);
            }
        }
    }
}
