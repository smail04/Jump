using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkerPlatform : MonoBehaviour
{
    [SerializeField] private float maxDistanceDelta = 1;
    [SerializeField] private float force = 1;
    [SerializeField] private Transform point1, point2;
    private bool walkToEnd = false;
    private Rigidbody playerRB;

    void Start()
    {
        point1.parent = null;
        point2.parent = null;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, (walkToEnd) ? point2.position : point1.position, maxDistanceDelta);
        if (transform.position == point1.position || transform.position == point2.position)
            Reverse();
    }

    private void Reverse()
    {
        walkToEnd = !walkToEnd;
        if (playerRB) playerRB.AddForce(Vector3.up * force, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint point in collision.contacts)
        {
            if (Vector3.Dot(point.normal, Vector3.down) > 0.9f)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                if (player)
                {
                    player.transform.SetParent(transform);
                    player.transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
                    playerRB = player.GetComponent<Rigidbody>();
                }
            }
        }        
    }

    private void OnCollisionExit(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            player.transform.SetParent(null);
            playerRB = null;
        }
    }
}
