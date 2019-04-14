using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character : MonoBehaviour
{
    NavMeshAgent agent;
    bool bInit;
    [HideInInspector]
    public float fHp;
    [SerializeField] float fMaxHp;
    Vector3 m_dest;
    Color c_fullSin = Color.red;
    Color c_noSin = Color.white;

    Color myColor;
    Material mat;
    MeshRenderer mr;
    public bool bSin;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = new Material(mr.material);
        SetColor(c_noSin);
    }
    public void InitData(Vector3 _head, Vector3 _dest, bool b_sin, float _speed)
    {
        bInit = true;
        fHp = fMaxHp;
        m_dest = _dest;
        transform.position = _head;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
        agent.SetDestination(m_dest);

        bSin = b_sin;
        myColor = c_noSin;
        if (bSin)
        {
            float rd = Random.Range(0.45f, 1f);
            myColor = new Color(1, rd, rd, 1);
        }
        SetColor(c_noSin);
    }

    private void SetColor(Color _color)
    {
        if(mat)
        {
            mat.SetColor("_Color", _color);
            mr.material = mat;
        }
    }

    public void ExposeColor()
    {
        SetColor(myColor);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(agent.destination, transform.position) <= 2f)
        {
            gameObject.SetActive(false);
            //CharacterGenerator.instance.CreateNewCharacter();
        }
    }


}
