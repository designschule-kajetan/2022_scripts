using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance { private set; get; }

    [SerializeField] private SceneReference sceneMenu;

    [SerializeField] public SceneReference[] scenesLevel;

    private int currentSceneIndexInList;

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Time.timeScale = 0f;
    }

    public void StartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void WinGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("Win");
        FindObjectOfType<UILevel>().ShowWinScreen();
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("You Lost!");
        FindObjectOfType<UILevel>().ShowLoseScreen();
        Time.timeScale = 0f;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(sceneMenu.BuildIndex);
    }

    public void LoadLevel(int listIndex)
    {
        currentSceneIndexInList = listIndex;
        SceneManager.LoadScene(scenesLevel[listIndex].BuildIndex);
    }

    public void LoadNextLevel()
    {
        int sceneToLoad = currentSceneIndexInList + 1;
        if (sceneToLoad < scenesLevel.Length)
        {
            SceneManager.LoadScene(scenesLevel[sceneToLoad].BuildIndex);
        }
        else
        {
            SceneManager.LoadScene(sceneMenu.BuildIndex);
        }
    }
    
}
