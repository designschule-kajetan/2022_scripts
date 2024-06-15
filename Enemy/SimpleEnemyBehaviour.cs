using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class SimpleEnemyBehaviour : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform target;

    [SerializeField] private bool isRotating = false;
    [SerializeField] private float rotationSpeed = 20f;

    public bool hasTarget;
    
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (hasTarget)
        {
            agent.SetDestination(target.position);
        }

        if (isRotating)
        {
            transform.Rotate(transform.up,rotationSpeed*Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            GameController.Instance.LoseGame();
        }
    }
}
