using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooter : MonoBehaviour
{
    public static Queue<Character> pirorityQueue = new Queue<Character>();
    public static Character currentCharacter;

    public static void SetTarget(Character _character)
    {
        if (!pirorityQueue.Contains(_character))
        {
            _character.BeTargeted(true);
            pirorityQueue.Enqueue(_character);


            Debug.Log("Enque character: " + _character.gameObject.name + " Queue Count: " + pirorityQueue.Count);
            //UnityEditor.EditorApplication.isPaused = true;
        }
        //currentCharacter = _character;
    }

    private Transform spawnPoint;
    [SerializeField] float fInterval;
    private float fCollpaseTime;
    public bool bCanShoot;
    Vector3 vShootDir;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GuideMover guiderMove;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCharacter == null && pirorityQueue.Count > 0)
        {
            currentCharacter = pirorityQueue.Peek();
            currentCharacter.BeTargeted(true);
            guiderMove.NewTarget(currentCharacter.transform);
        }

        if (currentCharacter!=null &&  !currentCharacter.gameObject.activeInHierarchy)
        {
            Character _char = pirorityQueue.Dequeue();
            Debug.Log("Deque character: " + _char .name+ " Queue Count: " + pirorityQueue.Count + ", name Of currentChar: " + currentCharacter.transform.name);
           // UnityEditor.EditorApplication.isPaused = true;
            currentCharacter = null;
            // Keep Looping until finding a new target, or the queue is empty
            while (pirorityQueue.Count > 0)
            {
                Character peek = pirorityQueue.Peek();
                if (peek.gameObject.activeInHierarchy && peek.bTargeted)
                {
                    Debug.Log("Old Target disapper, " + peek.name + " is the new target");
                    peek.BeTargeted(true);
                    currentCharacter = peek;
                    guiderMove.NewTarget(currentCharacter.transform);

                    break;
                }
                else
                {
                    pirorityQueue.Dequeue();
                }
            }
            if (pirorityQueue.Count == 0)
                guiderMove.GoBackToOrigin();
        }
        

        if (bCanShoot && currentCharacter != null)
        {
            Vector3 dirTochar = (currentCharacter.transform.position - spawnPoint.position);
            Vector3 forwardOfChar = currentCharacter.GetComponent<NavMeshAgent>().speed * currentCharacter.transform.forward;
            vShootDir = (dirTochar + forwardOfChar).normalized;

            fCollpaseTime += Time.deltaTime;
            if (fCollpaseTime >= fInterval)
            {
                fCollpaseTime = 0;
                ShootBullet(vShootDir, vShootDir.magnitude);
            }
        }
    }
    public void ShootBullet(Vector3 _dir, float _speed)
    {
        GameObject go = Instantiate(bulletPrefab);
        go.transform.rotation = Quaternion.LookRotation(_dir, go.transform.up);
        go.transform.position = spawnPoint.position;
        go.GetComponent<Bullet>().fMoveSpeed = _speed;
    }

}



