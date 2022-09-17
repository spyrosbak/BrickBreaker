using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text NameText;
    public Text ScoreText;
    public Text HighScoreText;
    public GameObject GameOverText;
    public GameObject LevelClearedText;

    private float rotationSpeed = 1.5f;
    private bool m_Started = false;
    public int m_Points;
    
    private bool m_GameOver = false;
    private bool m_LevelClear = false;

    private void Awake()
    {
        MenuManager.Instance.LoadPlayerData();
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuManager.Instance.menuCanvas.gameObject.SetActive(false);
        HighScoreText.text = "Best Score: " + MenuManager.Instance.topPlayer.ToString() + " : " + MenuManager.Instance.bestScore.ToString();
        NameText.text = "Player Name : " + MenuManager.Instance.playerName.text;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < LineCount; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (m_LevelClear)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if(m_Points > MenuManager.Instance.bestScore)
        {
            MenuManager.Instance.topPlayer = MenuManager.Instance.playerName.text;
            MenuManager.Instance.bestScore = m_Points;
            MenuManager.Instance.SavePlayerData();
        }

        HighScoreText.text = "Best Score: " + MenuManager.Instance.topPlayer.ToString() + ":" + MenuManager.Instance.bestScore.ToString();
    }

    public void LevelClear()
    {
        m_LevelClear = true;
        LevelClearedText.SetActive(true);

        Destroy(Ball);
    }
}
