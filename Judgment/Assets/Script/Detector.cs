using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    public float fMovingSpd = 2f;
    private Rigidbody rb;
    List<Character> sinChar;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sinChar = new List<Character>();

    }
    delegate void SetPosition(Vector3 _pos);
    event SetPosition onSetPos;

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Alert();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sin"))
        {
            //Debug.Log(other.name + "Enter detection area");
            Character inDetect = other.GetComponent<Character>();
            inDetect.ExposeColor();
            sinChar.Add(inDetect);

            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sin"))
        {
            Character inDetect = other.GetComponent<Character>();
            inDetect.RecoverColor();
            sinChar.Remove(inDetect);
        }
    }

    public void Alert()
    {
        for (int i = 0; i < sinChar.Count; i++)
        {
            if (sinChar[i].bSin)
            {
                //sinChar[i].transform.GetChild(1).gameObject.SetActive(true);
                Shooter.SetTarget(sinChar[i]);

            }
        }
    }

}
