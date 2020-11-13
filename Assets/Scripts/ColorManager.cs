using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class ColorManager : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void blackCat()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("looks", 0);
        PlayerPrefs.SetString("beginning", getStringTime());
        SceneManager.LoadScene("Game");
    }

    public void brownCat()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("looks", 1);
        PlayerPrefs.SetString("beginning", getStringTime());
        SceneManager.LoadScene("Game");
    }

    public void whiteCat()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("looks", 2);
        PlayerPrefs.SetString("beginning", getStringTime());
        SceneManager.LoadScene("Game");
    }

    public void orangeCat()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("looks", 3);
        PlayerPrefs.SetString("beginning", getStringTime());
        SceneManager.LoadScene("Game");
    }

    string getStringTime()
    {
        DateTime now = DateTime.Now;
        return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
    }

}
