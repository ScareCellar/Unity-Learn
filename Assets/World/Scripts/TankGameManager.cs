using TMPro;
using UnityEngine;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] GameObject titlePanel;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] bool debug = false;

    static TankGameManager instance;
    public static TankGameManager Instance 
    {  
        get 
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<TankGameManager>();
            }
            return instance; 
        } 
    }
    public int Score { get; set; } = 0;
    
    void Start()
    {
        Time.timeScale = (debug) ? 1.0f : 0.0f;
        titlePanel.SetActive(!debug);
    }

    
    void Update()
    {
        scoreText.text = Score.ToString();
    }

    public void OnGameStart()
    {
        titlePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnGameWin()
    {
        Time.timeScale = 0.0f;
    }
}
