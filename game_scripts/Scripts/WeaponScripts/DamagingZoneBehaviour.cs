using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingZoneBehaviour : MeleeWeaponBehaviour
{
    public Transform player; 

    protected override void Start()
    {
        base.Start();

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Nie znaleziono obiektu gracza z tagiem 'Player'.");
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.position = player.position;
        }
    }
}
