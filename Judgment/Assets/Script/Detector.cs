using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public float fMovingSpd = 2f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float _hori = Input.GetAxisRaw("Horizontal");
        float _vert = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(_hori, 0, _vert).normalized;
        //transform.position += dir * fMovingSpd * Time.deltaTime;

        rb.velocity = dir * fMovingSpd;
        float zClamp = Mathf.Clamp(transform.position.z, -30f, 3f);
        float xClamp = Mathf.Clamp(transform.position.x, -22f, 22f);
        transform.position = new Vector3(xClamp, transform.position.y, zClamp);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sin"))
        {
            other.GetComponent<Character>().ExposeColor();
        }
    }
}
