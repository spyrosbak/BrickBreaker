using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Text playerName;
    public Canvas menuCanvas;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(menuCanvas.gameObject);
    }

    public void OnStartButtonClicked()
    {
        //playerName.text = gameObject.GetComponent<InputField>().text;
        SceneManager.LoadScene("main");
    }
}
