using System.Collections;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private float minCameraOrthoSize = 6;
    [SerializeField] private float maxCameraOrthoSize = 8;
    private Coroutine current;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = playerTransform.position.y;
        transform.position = pos;
    }

    public void MoveAwayCamera()
    {
        SetCameraOrthoSize(maxCameraOrthoSize);
    }

    public void MoveBackCamera()
    {
        SetCameraOrthoSize(minCameraOrthoSize);
    }

    private void SetCameraOrthoSize(float size)
    {
        StopAllCoroutines();
        StartCoroutine(nameof(SetCameraSize), size);
    }

    IEnumerator SetCameraSize(float targetSize)
    {
        float waitTimeSec = 0.2f;
        float elapsedTime = 0;
        while (elapsedTime < waitTimeSec)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetSize,  elapsedTime / waitTimeSec);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}