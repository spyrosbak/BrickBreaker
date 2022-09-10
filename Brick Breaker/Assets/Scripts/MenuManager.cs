using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public Text playerName;
    public Canvas menuCanvas;
    public AudioSource backgroundMusic;
    public string topPlayer = null;
    public int bestScore = 0;

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
        DontDestroyOnLoad(backgroundMusic.gameObject);
    }

    public void OnStartButtonClicked()
    {
        //playerName.text = gameObject.GetComponent<InputField>().text;
        SceneManager.LoadScene("main");
    }

    [System.Serializable]
    public class SaveData
    {
        public string topPlayer;
        public int bestScore;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();
        data.topPlayer = topPlayer;
        data.bestScore = bestScore;

        string jsonText = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", jsonText);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(jsonData);

            topPlayer = data.topPlayer;
            bestScore = data.bestScore;
        }
    }
}
