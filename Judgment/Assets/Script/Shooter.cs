using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private Transform spawnPoint;
    [SerializeField] float fInterval;
    private float fCollpaseTime;
    public bool bCanShoot;
    public Vector3 vShootDir;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (bCanShoot)
        {
            fCollpaseTime += Time.deltaTime;
            if(fCollpaseTime >= fInterval)
            {
                fCollpaseTime = 0;
                RaycastHit hit;
                Ray ray = new Ray(spawnPoint.position, vShootDir);
                if(Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Hitable"))
                    {
                        GiveDamage(hit.collider.gameObject);
                    }
                }
            }
        }
    }
    private void GiveDamage(GameObject _obj)
    {
        Debug.Log("Giving Damage to: " + _obj.name);
    }
}
