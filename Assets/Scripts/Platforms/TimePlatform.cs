using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePlatform : Platform
{
    [SerializeField] private GameObject[] cubes;
    [SerializeField] private Collider _collider;

    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
               
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player player;
        if (isPlayerOnTop(collision, out player))
        {
            StartCoroutine(nameof(HidePlatform));  
        }
    }

    IEnumerator HidePlatform()
    {
        float waitTime = 3.1f;
        float elapsedTime = 0;

        while (elapsedTime < waitTime)
        {
            int index = Mathf.FloorToInt(elapsedTime);
            if (cubes[index].activeInHierarchy) { 
                cubes[index].SetActive(false);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _collider.enabled = false;
        Invoke(nameof(RestorePlatform), 1);
        yield return null;
    }

    private void RestorePlatform()
    {
        foreach (GameObject go in cubes)
        {
            go.SetActive(true);
        }
        _collider.enabled = true;
    }
}
