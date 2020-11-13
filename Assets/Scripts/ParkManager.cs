using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ParkManager : MonoBehaviour
{
	
	public GameObject cat;

	public GameObject[] catList;

	private void Start()
	{
		if (!PlayerPrefs.HasKey("looks"))
		{
			PlayerPrefs.SetInt("looks", 0);
		}
		createCat(PlayerPrefs.GetInt("looks"));
	}

	void Update()
	{
		TimeSpan stageCheck = getStageSpan();

		if (stageCheck < TimeSpan.FromDays(3))
		{
			cat.GetComponent<Cat>().stage = "Baby";
			float babyScale = 2f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(babyScale, babyScale, babyScale);
		}

		if (stageCheck > TimeSpan.FromDays(3) & stageCheck < TimeSpan.FromDays(10))
		{
			cat.GetComponent<Cat>().stage = "Kitten";
			float kittenScale = 3f;
			cat.GetComponent<Cat>().transform.localScale = new Vector3(kittenScale, kittenScale, kittenScale);
		}

		if (stageCheck > TimeSpan.FromDays(10) & stageCheck < TimeSpan.FromDays(18))
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

	public void createCat(int i)
	{
		if (cat)
		{
			Destroy(cat);
		}
		Vector3 start = cat.transform.position;
		cat = Instantiate(catList[i], start, Quaternion.identity) as GameObject;

		PlayerPrefs.SetInt("looks", i);

	}

	TimeSpan getStageSpan()
	{
		return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("beginning"));
	}
}
