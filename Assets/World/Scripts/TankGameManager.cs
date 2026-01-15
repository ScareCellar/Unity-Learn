using TMPro;
using UnityEngine;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] GameObject titlePanel;
    [SerializeField] TMP_Text scoreText;

    public int score = 0;
    
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void OnGameStart()
    {
        titlePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
