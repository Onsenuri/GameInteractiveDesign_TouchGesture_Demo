using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum SkillType
    {
        Normal,
        Thunder,
        FireBall,
        StoneBall
    }
    public float speed = 5.0f;
    public float damage = 50.0f;
    public SkillType skillType;

    public GameObject thunderBoltHitEffect;
    public GameObject fireBallHitEffect;
    public GameObject stoneBallHitEffect;
    public GameObject normalHitEffect;
    
    private Animator _animator;
    private bool _isMoving =true;
    

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
        if (_isMoving)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy _enemy = other.GetComponent<Enemy>();
            if (skillType == SkillType.Thunder)
            {
                if (_enemy.enemyType == Enemy.EnemyType.Fire)
                    _enemy.health -= damage * 2.0f;
                else 
                    _enemy.health -= damage;
                Debug.Log("Enemy Detected_Thunder");
                Instantiate(thunderBoltHitEffect, transform.position, Quaternion.identity);
                DestroyMyself();
            }
            else if (skillType == SkillType.FireBall)
            {
                if (_enemy.enemyType == Enemy.EnemyType.Ice)
                    _enemy.health -= damage * 2.0f;
                else 
                    _enemy.health -= damage;
                Debug.Log("Enemy Detected_FireBall");
                Instantiate(fireBallHitEffect, transform.position, Quaternion.identity);
                DestroyMyself();
            }
            else if (skillType == SkillType.StoneBall)
            {
                if (_enemy.enemyType == Enemy.EnemyType.Stone)
                    _enemy.health -= damage * 2.0f;
                else 
                    _enemy.health -= damage;
                Debug.Log("Enemy Detected_StoneBall");
                Instantiate(stoneBallHitEffect, transform.position, Quaternion.identity);
                DestroyMyself();
            }
            else if (skillType == SkillType.Normal)
            {
                _enemy.health -= damage;
                Debug.Log("Enemy Detected_Normal");
                Instantiate(normalHitEffect, transform.position, Quaternion.identity);
                DestroyMyself();
            }
            
        }
    }
}
