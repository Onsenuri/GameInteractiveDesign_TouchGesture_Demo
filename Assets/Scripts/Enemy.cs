using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Fire,
        Ice,
        Stone
    }

    public EnemyType enemyType;
    public float health = 99.0f;

    private Animator _animator;

    void DestroyMyself()
    {
        Destroy(gameObject);
    }
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            _animator.SetBool("isDead", true);
            //Destroy(gameObject);
        }
        else
        {
            transform.Translate(Vector3.left * 1.0f * Time.deltaTime);
        }
    }
}
