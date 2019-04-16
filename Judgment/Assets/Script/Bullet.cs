using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    public float fMoveSpeed;
    float fMultiplier = 80;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Invoke("SelfDestroy", 2f);
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += fMoveSpeed * transform.forward;
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
