using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    protected bool stable = true;
    public bool Stable => stable;

    protected bool isPlayerOnTop(Collision collision, out Player player)
    {
        foreach (ContactPoint point in collision.contacts)
        {
            if (Vector3.Dot(point.normal, Vector3.down) > 0.9f)
            {
                player = collision.gameObject.GetComponent<Player>();
                if (player)
                {
                    return true;
                }
            }
        }
        player = null;
        return false;
    }
}
