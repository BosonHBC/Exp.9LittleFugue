using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _hori;
        float _vert;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sin"))
        {
            other.GetComponent<Character>().ExposeColor();
        }
    }
}
