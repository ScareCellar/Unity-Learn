using UnityEngine;
using UnityEngine.SceneManagement;

public class TankGameManager : MonoBehaviour
{
    [SerializeField] GameObject titlePanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject deadPanel;
    [SerializeField] GameObject winPanel;


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
        gamePanel.SetActive(debug);
    }

    
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length == 0)
        {
            deadPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            winPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
    }

    public void OnGameStart()
    {
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void OnGameWin()
    {
        Time.timeScale = 0.0f;
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuit()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
