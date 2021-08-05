using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = playerTransform.position.y;
        transform.position = pos;
    }
}