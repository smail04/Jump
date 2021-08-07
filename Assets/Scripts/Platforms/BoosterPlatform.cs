using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterPlatform : Platform
{
    [SerializeField] private float force = 6;

    private void Start()
    {
        stable = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player;
        if (isPlayerOnTop(collision, out player))
        {
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
            Rigidbody playerRB = player.GetComponent<Rigidbody>();
            playerRB.velocity = Vector3.zero;
            playerRB.AddForce(Vector3.up * force, ForceMode.VelocityChange);
        }
    }
}
