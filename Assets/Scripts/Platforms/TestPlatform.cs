using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlatform : Platform
{
    public GameObject victoryText;

    private void OnCollisionEnter(Collision collision)
    {
        Player p;
        if (isPlayerOnTop(collision, out p))
        {
            if (victoryText)
            {
                victoryText.SetActive(true);
            }
        }
    }
}
