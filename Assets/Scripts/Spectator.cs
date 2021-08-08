using System.Collections;
using UnityEngine;

public class Spectator : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private float minCameraOrthoSize = 6;
    [SerializeField] private float maxCameraOrthoSize = 9;
    private Coroutine current;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.y = playerTransform.position.y;
        transform.position = pos;        
    }

    private void LateUpdate()
    {

        float offset = (_camera.transform.position.x - playerTransform.position.x);

        _camera.transform.position = Vector3.Lerp(_camera.transform.position,
                                                new Vector3(playerTransform.position.x, _camera.transform.position.y, _camera.transform.position.z),
                                                Time.deltaTime * 6);

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