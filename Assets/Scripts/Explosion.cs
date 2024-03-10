﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float maxSize = 5;
    public float damage = 50;
    public float speed = 1;
    // Start is called before the first frame update
    private void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.localScale += Vector3.one * Time.deltaTime * speed;
        if(transform.localScale.x > maxSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth != null)
        {
            playerHealth.DealDamage(damage);
        }

        var enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.DealDamage(damage);
        }
        
        
    }
}