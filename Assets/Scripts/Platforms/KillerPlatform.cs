using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player) player.Die();
    }
}
