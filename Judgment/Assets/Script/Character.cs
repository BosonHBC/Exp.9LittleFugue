using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Character : MonoBehaviour
{
    NavMeshAgent agent;
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
    Camera cam;
    Transform skull;
    public bool bTargeted;
    private float fSinanity;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        mat = new Material(mr.material);
        SetColor(c_noSin);
        cam = Camera.main;
        skull = transform.GetChild(1);
    }
    public void InitData(Vector3 _head, Vector3 _dest, bool b_sin, float _speed)
    {
        bTargeted = false;
        fHp = fMaxHp;
        m_dest = _dest;
        transform.position = _head;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = _speed;
        agent.SetDestination(m_dest);
        fHp = 100f;

        bSin = b_sin;
        myColor = c_noSin;
        if (bSin)
        {
            fSinanity = Random.Range(0.45f, 1f);
            myColor = new Color(1, fSinanity, fSinanity, 1);
        }
        SetColor(c_noSin);
        BeTargeted(false);
    }

    private void SetColor(Color _color)
    {
        if (mat)
        {
            mat.SetColor("_Color", _color);
            mr.material = mat;
        }
    }

    public void ExposeColor()
    {
        SetColor(myColor);
    }
    public void RecoverColor()
    {
        SetColor(c_noSin);
    }
    public void BeTargeted(bool _beTargted)
    {
        transform.GetChild(1).gameObject.SetActive(_beTargted);
        bTargeted = _beTargted;
    }

    // Update is called once per frame
    void Update()
    {
        if (skull.gameObject.activeInHierarchy)
            skull.LookAt(cam.transform);
        if (Vector3.Distance(agent.destination, transform.position) <= 2f)
        {
        bTargeted = false;
          //  Debug.Log(name + "dies because of go to dest");
            gameObject.SetActive(false);
            //CharacterGenerator.instance.CreateNewCharacter();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            fHp -= 100f;
            if(fHp <= 0)
            {
                if (bSin)
                {
                    ScoreControl.instance.ChageScore(150 * fSinanity);
                }
                else
                {
                    ScoreControl.instance.ChageScore(-50);
                }
                Debug.Log(name + "dies because of kill by bullet");
                bTargeted = false;
                gameObject.SetActive(false);
                Destroy(collision.collider.gameObject);
            }
        }
    }

    private void OnDisable()
    {
        bTargeted = false;
    }

}
