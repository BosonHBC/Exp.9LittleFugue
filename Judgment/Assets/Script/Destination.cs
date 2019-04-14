using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    private void Awake()
    {
        m_collider = GetComponent<BoxCollider>();
        dests = new Vector3[iDestCount];
        for (int i = 0; i < iDestCount; i++)
        {
            dests[i] = RandomPointInBounds();
        }
    }
    public int iDestCount;
    Collider m_collider;
    Vector3[] dests;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetRandomDestination()
    {
        return dests[Random.Range(0, iDestCount)];
    }

    Vector3 RandomPointInBounds()
    {
        Vector3 rdPoint = new Vector3(
            Random.Range(m_collider.bounds.min.x, m_collider.bounds.max.x),
            Random.Range(m_collider.bounds.min.y, m_collider.bounds.max.y),
            Random.Range(m_collider.bounds.min.z, m_collider.bounds.max.z)
        );

        return rdPoint;
    }


    private void OnDrawGizmos()
    {
        if(dests!=null)
        for (int i = 0; i < dests.Length; i++)
        {
            Gizmos.DrawSphere(dests[i], 1);
        }
    }
}
