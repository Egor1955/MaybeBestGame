﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> patrolPoints;
    public PlayerController player;
    public float viewAngle;
    public float damage = 30;

    public EnemyHealth enemyHealth;

    private PlayerHealth _playerHealth;

    private NavMeshAgent _navMeshAgent;

    private bool _isPlayerNoticed;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _playerHealth = player.GetComponent<PlayerHealth>();

        enemyHealth = GetComponent<EnemyHealth>();

        PickNewPatrolPoint();
       
    }

    public bool IsAlive()
    {
        return enemyHealth.IsAlive();
    }

    // Update is called once per frame
    void Update()
    {
        var direction = player.transform.position - transform.position;

        if (Vector3.Angle(transform.forward, direction)< viewAngle)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, direction, out hit))
            {
                if (hit.collider.gameObject == player.gameObject)
                {
                    _isPlayerNoticed = true;
                }
                else
                {
                    _isPlayerNoticed = false;
                }
            }
            else
            {
                _isPlayerNoticed = false;
            }
        }
        else
        {
            _isPlayerNoticed = false;
        }

        if (_navMeshAgent.remainingDistance == 0)
        {
            PickNewPatrolPoint();
        }

        ChaseUpdate();
        PatrolUpdate();
        AttackUpdate();
    }
    private void PatrolUpdate()
    {
        if (!_isPlayerNoticed)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                PickNewPatrolPoint();
            }
        }
    }

    private void PickNewPatrolPoint()
    {
        _navMeshAgent.destination = patrolPoints[Random.Range(0, patrolPoints.Count)].position;
    }

    private void ChaseUpdate()
    {
        if (_isPlayerNoticed)
        {
            _navMeshAgent.destination = player.transform.position;
        }
    }
    
    private void AttackUpdate()
    {
        if(_isPlayerNoticed)
        {
            if(_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
               _playerHealth.DealDamage(damage * Time.deltaTime);
            }
        }
    }
}
