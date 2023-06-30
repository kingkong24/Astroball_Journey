using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerMovement = other.GetComponent<PlayerControl>();
            if (playerMovement != null)
            {
                playerMovement.ResetPlayer();
            }
        }
    }
}
