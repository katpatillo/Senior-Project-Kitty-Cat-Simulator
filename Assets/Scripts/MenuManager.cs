using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MenuManager : MonoBehaviour
{

    public Button newGame;
    public Button resumeGame;
    public GameObject resumeGameError;

    // Start is called before the first frame update
    void Start()
    {
        //test resume game error
        //PlayerPrefs.DeleteAll();

        Debug.Log(PlayerPrefs.GetString("beginning"));

        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startNewGame()
    {
        SceneManager.LoadScene("NewGameColor");
    }

    public void loadGame()
    {
        if (!PlayerPrefs.HasKey("beginning"))
        {
            resumeGameError.SetActive(true);
        }
        else SceneManager.LoadScene("Game");
    }

    public void ok()
    {
        resumeGameError.SetActive(false);
    }
}