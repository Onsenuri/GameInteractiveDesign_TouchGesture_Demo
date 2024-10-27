using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    
    public Transform spawnPoint;
    public bool isGameStart = false;
    public float spawnRate = 1.0f;

    public GameObject monster_Fire;
    public GameObject monster_Ice;
    public GameObject monster_Stone;
    
    
    private float _spawnTime = 0.0f;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStart)
        {
            if (_spawnTime > spawnRate)
            {
                int _monsterInt = Random.Range(0, 3);
                if (_monsterInt == 0)
                {
                    GameObject monster = Instantiate(monster_Fire,spawnPoint.position, Quaternion.identity);
                }
                else if (_monsterInt == 1)
                {
                    GameObject monster = Instantiate(monster_Ice,spawnPoint.position, Quaternion.identity);
                }
                else if (_monsterInt == 2)
                {
                    GameObject monster = Instantiate(monster_Stone,spawnPoint.position, Quaternion.identity);
                }
                Debug.Log("Spawn Enemy");
                _spawnTime = 0.0f;
            }
            else
            {
                _spawnTime += Time.deltaTime;
            }
        }
        
    }
}
