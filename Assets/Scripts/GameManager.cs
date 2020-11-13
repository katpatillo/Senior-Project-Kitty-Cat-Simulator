using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
	public GameObject happinessText;
	[SerializeField]
	public GameObject hungerText;
	[SerializeField]
	public GameObject cleanlinessText;
	[SerializeField]
	public GameObject coinsText;
	[SerializeField]
	public GameObject stageText;

	public GameObject nameText;
	public GameObject cat;

	public GameObject namePanel;
	public GameObject nameInput;

	public GameObject[] catList;
	public GameObject foodPanel;
	public GameObject toyPanel;
	public GameObject cleanPanel;
	public GameObject error;
	public GameObject[] randomEvents;

	public int faveToy;
	public int randomEventPicker;

	private void Start()
	{
		//kitten test
		//PlayerPrefs.SetString("beginning", "9/22/2020 17:2:39");
		//teen test
		//PlayerPrefs.SetString("beginning", "9/10/2020 17:2:39");
		//adult test
		//PlayerPrefs.SetString("beginning", "8/11/2020 17:2:39");

		//PlayerPrefs.SetString("then", "10/9/2020 05:20:39");
		//PlayerPrefs.SetInt("lastDayPlayed", 6);

		if (!PlayerPrefs.HasKey("looks"))
		{
			PlayerPrefs.SetInt("looks", 0);
		}
		createCat(PlayerPrefs.GetInt("looks"));

	}

	void Update()
    {

		happinessText.GetComponent<Text>().text = "" + cat.GetComponent<Cat>().Happiness;
		hungerText.GetComponent<Text>().text = "" + cat.GetComponent<Cat>().Hunger;
		cleanlinessText.GetComponent<Text>().text = "" + cat.GetComponent<Cat>().Cleanliness;
		coinsText.GetComponent<Text>().text = "" + cat.GetComponent<Cat>().Coins;
		nameText.GetComponent<Text>().text = cat.GetComponent<Cat>().name;
		stageText.GetComponent<Text>().text = cat.GetComponent<Cat>().stage;

		if (PlayerPrefs.GetString("LastScene") == "WalkCutscene")
		{
			randomEvent();
			PlayerPrefs.DeleteKey("LastScene");
		}

		TimeSpan stageCheck = getStageSpan();
		
		if (stageCheck <= TimeSpan.FromDays(3))
		{
			cat.GetComponent<Cat>().stage = "Baby";
			float babyScale = 2f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(babyScale, babyScale, babyScale);
		}

		if (stageCheck > TimeSpan.FromDays(3) & stageCheck <= TimeSpan.FromDays(10))
		{
			cat.GetComponent<Cat>().stage = "Kitten";
			float kittenScale = 3f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(kittenScale, kittenScale, kittenScale);
		}

		if (stageCheck > TimeSpan.FromDays(10) & stageCheck <= TimeSpan.FromDays(18))
		{
			cat.GetComponent<Cat>().stage = "Teen";
			float teenScale = 4f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(teenScale, teenScale, teenScale);
		}

		if (stageCheck > TimeSpan.FromDays(18))
		{
			cat.GetComponent<Cat>().stage = "Adult";
			float adultScale = 5f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(adultScale, adultScale, adultScale);
		}
	}

	public void triggerNamePanel(bool b)
	{
		namePanel.SetActive(!namePanel.activeInHierarchy);
		if (b)
		{
			cat.GetComponent<Cat>().name = nameInput.GetComponent<InputField>().text;
			PlayerPrefs.SetString("name", cat.GetComponent<Cat>().name);
		}
	}

	TimeSpan getStageSpan()
	{
		return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("beginning"));
	}

	public void buttonBehavior(int i)
	{
		switch (i)
		{
			case (0):
			default:
				foodPanel.SetActive(!foodPanel.activeInHierarchy);
				break;
			case (1):
				toyPanel.SetActive(!toyPanel.activeInHierarchy);
				break;
			case (2):
				cleanPanel.SetActive(!cleanPanel.activeInHierarchy);
				break;
			case (3):
				SceneManager.LoadScene("WalkCutscene");
				break;
			case (4):
				cat.GetComponent<Cat>().saveCat();
				Application.Quit();
				break;

		}
	}

	public void createCat(int i)
	{
		if (cat) {
			Destroy(cat); 
		}
		Vector3 start = cat.transform.position;
		cat = Instantiate(catList[i], start, Quaternion.identity) as GameObject;

		PlayerPrefs.SetInt("looks", i);

	}

	public void dryFood()
	{
		if (cat.GetComponent<Cat>().Coins < 10)
		{
			error.SetActive(true);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-10);
			cat.GetComponent<Cat>().updateHunger(10);
		}
	}

	public void wetFood()
	{
		if (cat.GetComponent<Cat>().Coins < 15)
		{
			error.SetActive(true);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHunger(20);
		}
	}

	public void treats()
	{
		if (cat.GetComponent<Cat>().Coins < 20)
		{
			error.SetActive(true);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-20);
			cat.GetComponent<Cat>().updateHunger(15);
			cat.GetComponent<Cat>().updateHappiness(10);
		}
	}

	public void featherWand()
	{
		faveToy = UnityEngine.Random.Range(1, 5);

		if (cat.GetComponent<Cat>().Coins < 15)
		{
			error.SetActive(true);
		}
		else if (faveToy == 1)
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(25);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(15);
		}
	}

	public void laserPointer()
	{
		faveToy = UnityEngine.Random.Range(1, 5);

		if (cat.GetComponent<Cat>().Coins < 15)
		{
			error.SetActive(true);
		}
		else if (faveToy == 2)
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(25);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(15);
		}
	}

	public void scratcher()
	{
		faveToy = UnityEngine.Random.Range(1, 5);

		if (cat.GetComponent<Cat>().Coins < 15)
		{
			error.SetActive(true);
		}
		else if (faveToy == 3)
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(25);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(15);
		}
	}

	public void yarnBall()
	{
		faveToy = UnityEngine.Random.Range(1, 5);

		if (cat.GetComponent<Cat>().Coins < 15)
		{
			error.SetActive(true);
		}
		else if (faveToy == 4)
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(25);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-15);
			cat.GetComponent<Cat>().updateHappiness(15);
		}
	}

	public void cleanLitterbox()
	{
		if (cat.GetComponent<Cat>().Coins < 5)
		{
			error.SetActive(true);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-5);
			cat.GetComponent<Cat>().updateCleanliness(10);
		}
	}

	public void takeABath()
	{
		if (cat.GetComponent<Cat>().Coins < 10)
		{
			error.SetActive(true);
		}
		else
		{
			cat.GetComponent<Cat>().updateCoins(-10);
			cat.GetComponent<Cat>().updateCleanliness(20);
			cat.GetComponent<Cat>().updateHappiness(-2);
		}
	}

	public void randomEvent ()
	{
		randomEventPicker = UnityEngine.Random.Range(0, 12);

		if (randomEventPicker == 0) {
			cat.GetComponent<Cat>().updateHappiness(5);
			cat.GetComponent<Cat>().updateHunger(2);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 1)	{
			cat.GetComponent<Cat>().updateHappiness(10);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 2) {
			cat.GetComponent<Cat>().updateCoins(5);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 3) {
			cat.GetComponent<Cat>().updateCoins(15);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 4) {
			cat.GetComponent<Cat>().updateHunger(5);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if(randomEventPicker == 5) {
			cat.GetComponent<Cat>().updateHappiness(15);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if(randomEventPicker == 6) {
			cat.GetComponent<Cat>().updateCleanliness(-10);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if(randomEventPicker == 7) {
			cat.GetComponent<Cat>().updateHappiness(-10);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if(randomEventPicker == 8) {
			cat.GetComponent<Cat>().updateHunger(-5);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if(randomEventPicker == 9) {
			cat.GetComponent<Cat>().updateHappiness(-5);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 10) {
			cat.GetComponent<Cat>().updateHappiness(-5);
			cat.GetComponent<Cat>().updateCleanliness(10);
			randomEvents[randomEventPicker].SetActive(true);
		}
		if (randomEventPicker == 11) {
			cat.GetComponent<Cat>().updateHappiness(10);
			cat.GetComponent<Cat>().updateHunger(-10);
			randomEvents[randomEventPicker].SetActive(true);
		}
	}

	public void ok()
	{
		error.SetActive(false);
		randomEvents[randomEventPicker].SetActive(false);
	}
}