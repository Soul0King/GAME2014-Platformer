using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonsScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GreenGrassLVL");
    }
    public void LoadGrassLevel()
    {
        SceneManager.LoadScene("GreenGrassLVL");
    }
    public void LoadDesertLevel()
    {
        SceneManager.LoadScene("DesertLVL");
    }
    public void LoadIceLevel()
    {
        SceneManager.LoadScene("IceCaveLVL");
    }
    public void LoadWinLevel()
    {
        SceneManager.LoadScene("GameWinLVL");
    }
    public void LoadLoseLevel()
    {
        SceneManager.LoadScene("GameLoseLVL");
    }
    public void LoadMainMenuLevel()
    {
        SceneManager.LoadScene("MainMenuLVL");
    }
}
