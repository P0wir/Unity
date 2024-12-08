using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.moveSpeed * Time.deltaTime);

        FacePlayer();
    }

    void FacePlayer()
    {
        if (player != null)
        {
            Vector3 scale = transform.localScale;

            if (transform.position.x < player.position.x)
            {
                scale.x = Mathf.Abs(scale.x); 
            }
            else
            {
                scale.x = -Mathf.Abs(scale.x); 
            }

            transform.localScale = scale; 
        }
    }
}
