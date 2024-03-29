﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float value = 100;
    public PlayerProgress playerProgress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAlive()
    {
        return value > 0;
    }

    public void DealDamage(float damage)
    {
        playerProgress.AddExperiens(damage);

        value -= damage;
        if(value <= 0)
        {
            Destroy(gameObject);
        }
    }

}
