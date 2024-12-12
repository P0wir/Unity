using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 enemyVelocity;
    private bool groundedEnemy;
    private float enemySpeed = 2.0f;
    private float gravityValue = -9.81f;

    public Transform pointA; 
    public Transform pointB;
    private Transform targetPoint;
    public Transform player; 
    public float detectionRadius = 5.0f;
    private bool chasingPlayer = false;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        targetPoint = pointA; 
    }

    private void Update()
    {
        groundedEnemy = controller.isGrounded;
        if (groundedEnemy && enemyVelocity.y < 0)
        {
            enemyVelocity.y = 0f;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            chasingPlayer = true;
        }
        else
        {
            chasingPlayer = false;
        }
        if (chasingPlayer)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        enemyVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(enemyVelocity * Time.deltaTime);
    }

    private void Patrol()
    {
        Vector3 moveDirection = (targetPoint.position - transform.position).normalized;
        controller.Move(moveDirection * Time.deltaTime * enemySpeed);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = targetPoint == pointA ? pointB : pointA;
        }
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }
    }

    private void ChasePlayer()
    {
        Vector3 moveDirection = (player.position - transform.position).normalized;

        if (transform.position.x >= Mathf.Min(pointA.position.x, pointB.position.x) &&
            transform.position.x <= Mathf.Max(pointA.position.x, pointB.position.x))
        {
            controller.Move(moveDirection * Time.deltaTime * enemySpeed);
            transform.forward = moveDirection;
        }
        else
        {
            chasingPlayer = false;
        }
    }
  }
