using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    public MainManager Manager;
    public LevelManager levelManager;

    private void Start()
    {
        Manager = MainManager.Instance;
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);

        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Manager.GameOver();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            levelManager.GameOver();
        }
    }
}
