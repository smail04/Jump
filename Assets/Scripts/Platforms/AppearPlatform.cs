using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPlatform : Platform
{
    [SerializeField] private Material startMaterial;
    [SerializeField] private Material appearMaterial;
    private Renderer _renderer;
    private Collider _collider;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();
        startMaterial = _renderer.material;
    }

    private void OnTriggerExit(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player)
        {
            if (Vector3.Dot(Vector3.up, player.GetComponent<Rigidbody>().velocity.normalized) > 0)
                Appear();
        }
    }

    private void OnCollisionExit(Collision collision)
    {        
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {
            Invoke(nameof(Disappear), 1);
        }        
    }

    private void Appear()
    {
        _renderer.material = appearMaterial;
        _collider.isTrigger = false;
    }

    private void Disappear()
    {
        _renderer.material = startMaterial;
        _collider.isTrigger = true;
    }
}
