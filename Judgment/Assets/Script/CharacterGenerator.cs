using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGenerator : MonoBehaviour
{

    public static CharacterGenerator instance;
    private void Awake()
    {
        instance = this;
        charaPool = new List<Character>();
        for (int i = 0; i < maxCapacity; i++)
        {
            GameObject go = Instantiate(charPrefab);
            go.transform.parent = transform;
            go.name = "char_" + i;
            go.gameObject.SetActive(false);
            go.GetComponent<Character>().bTargeted = false;
            charaPool.Add(go.GetComponent<Character>());
        }
    }

    [SerializeField] int maxCapacity = 50;
    [SerializeField] Destination[] head_end;
    [SerializeField] GameObject charPrefab;

    List<Character> charaPool;

  [SerializeField] float fCDOfNewChar = 0.2f;
    [SerializeField] float fSpeedOffset;
    float fCollpaseTime;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateNewCharacter();
        }
    }

    // Update is called once per frame
    void Update()
    {
        fCollpaseTime += Time.deltaTime;
        if(fCollpaseTime >= fCDOfNewChar)
        {
            CreateNewCharacter();
            fCollpaseTime = 0;
        }
    }

    public void CreateNewCharacter()
    {
        foreach (var item in charaPool)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                item.gameObject.SetActive(true);
             
                item.InitData(head_end[0].GetRandomDestination(), head_end[1].GetRandomDestination(), Random.Range(0, 2) == 0, fSpeedOffset + Random.Range(1, 5f));
                return;
            }
            
        }
        Debug.Log("pool is empty");
    }

}
