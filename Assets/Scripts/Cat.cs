using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cat : MonoBehaviour
{

	[SerializeField]
	private int _hunger;
	[SerializeField]
	private int _happiness;
	[SerializeField]
	private string _name;
	[SerializeField]
	private int _cleanliness;
	[SerializeField]
	private int _coins;
	[SerializeField]
	public String stage;

	private bool _serverTime;
	int catClickCount;
	int litterClickCount;
	int petCount;
	int scoopCount;
	int coinCount;
	int coinClickCount;
	int coinEarnedCount;
	int foodCount;
	int fedCatCount;

	// Start is called before the first frame update
	void Start()
    {
			UpdateStatus();

			InvokeRepeating("updateDevice", 1f, 60f);

			if (!PlayerPrefs.HasKey("name"))
			{
				PlayerPrefs.SetString("name", "Kitty");
			}
			else
			{
				_name = PlayerPrefs.GetString("name");
			}

	}

    // Update is called once per frame
    void Update()
    {

		saveCat();

		GetComponent<Animator>().SetBool("Jump", gameObject.transform.position.y > -2.3f);

		if (Input.GetMouseButtonUp(0))
		{
			Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
			if (hit)
			{
				if (hit.transform.gameObject.CompareTag("Cat"))
				{
					catClickCount ++;
					if (catClickCount >= 3 && PlayerPrefs.GetInt("petCount") < 20)
					{
						catClickCount = 0;
						updateHappiness(1);
						GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6000));
						petCount++;
						PlayerPrefs.SetInt("petCount", petCount);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
			if (hit)
			{
				//Debug.Log(hit.transform.gameObject.name);
				if (hit.transform.gameObject.CompareTag("Litterbox"))
				{
					litterClickCount++;
					if (litterClickCount >= 3 && PlayerPrefs.GetInt("scoopCount") < 20)
					{
						litterClickCount = 0;
						updateCleanliness(1);
						scoopCount++;
						PlayerPrefs.SetInt("scoopCount", scoopCount);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
			if (hit)
			{
				if (hit.transform.gameObject.CompareTag("Coin Jar"))
				{
					coinClickCount++;
					if (coinClickCount >= 3 && PlayerPrefs.GetInt("coinEarnedCount") < 20)
					{
						coinClickCount = 0;
						updateCoins(1);
						coinEarnedCount++;
						PlayerPrefs.SetInt("coinEarnedCount", coinEarnedCount);
					}
				}
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			Vector2 v = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(v), Vector2.zero);
			if (hit)
			{
				if (hit.transform.gameObject.CompareTag("Food Mat"))
				{
					foodCount++;
					if (foodCount >= 3 && PlayerPrefs.GetInt("fedCatCount") < 20)
					{
						foodCount = 0;
						updateHunger(1);
						fedCatCount++;
						PlayerPrefs.SetInt("fedCatCount", fedCatCount);
					}
				}
			}
		}

	}

	void UpdateStatus()
	{

		if (!PlayerPrefs.HasKey("_hunger"))
		{
			_hunger = 80;
			PlayerPrefs.SetInt("_hunger", _hunger);
		}
		else
		{
			_hunger = PlayerPrefs.GetInt("_hunger");
		}

		if (!PlayerPrefs.HasKey("_happiness"))
		{
			_happiness = 80;
			PlayerPrefs.SetInt("_happiness", _happiness);
		}
		else
		{
			_happiness = PlayerPrefs.GetInt("_happiness");
		}

		if (!PlayerPrefs.HasKey("_cleanliness"))
		{
			_cleanliness = 80;
			PlayerPrefs.SetInt("_cleanliness", _cleanliness);
		}
		else
		{
			_cleanliness = PlayerPrefs.GetInt("_cleanliness");
		}

		if (!PlayerPrefs.HasKey("_coins"))
		{
			_coins = 20;
			PlayerPrefs.SetInt("_coins", _coins);
		}
		else
		{
			_coins = PlayerPrefs.GetInt("_coins");
		}


		if (!PlayerPrefs.HasKey("then"))
		{
			PlayerPrefs.SetString("then", getStringTime());
		}



		TimeSpan ts = getTimeSpan();
		
		_hunger -= (int)(ts.TotalHours * 1.5);
		
		if (_hunger < 0)
		{
			_hunger = 0;
		}
		_happiness -= (int)((100 - _hunger) * (ts.TotalHours / 3));
		
		if (_happiness < 0)
		{
			_happiness = 0;
		}

		_cleanliness -= (int)(ts.TotalHours * 1.5);
		
		if (_cleanliness < 0)
		{
			_cleanliness = 0;
		}

		int daysPassed = getDaysPassed();

		if (daysPassed > 0)
		{
			PlayerPrefs.SetInt("coinCount", 0);
		}

		Debug.Log("player pref coin count before increment " + PlayerPrefs.GetInt("coinCount"));

		if (PlayerPrefs.GetInt("coinCount") < 300 && PlayerPrefs.GetString("LastScene") == "Start" && ts.TotalHours > 1)
		{
			_coins += (int)(1 / (1 + ts.TotalHours) * 500);
			Debug.Log("coins should be gained? " + (int)(1 / (1 + ts.TotalHours) * 500));
			
			if (_coins > 999)
			{
				_coins = 999;
			}

			coinCount = PlayerPrefs.GetInt("coinCount");
			coinCount += (int)(1 / (1 + ts.TotalHours) * 500);
			PlayerPrefs.SetInt("coinCount", coinCount);
			Debug.Log("player pref coin count after increment " + PlayerPrefs.GetInt("coinCount"));


		}

		if (daysPassed > 0)
		{
			PlayerPrefs.SetInt("petCount", 0);
			PlayerPrefs.SetInt("scoopCount", 0);
			PlayerPrefs.SetInt("coinEarnedCount", 0);
			PlayerPrefs.SetInt("fedCatCount", 0);
		}

		saveCat();
		
	}

	void updateDevice ()
	{
		PlayerPrefs.SetString("then", getStringTime());
		PlayerPrefs.SetInt("lastDayPlayed", getLastDayPlayed());
	}

	TimeSpan getTimeSpan()
	{
		return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("then"));
	}

	int getDaysPassed () {
		DateTime whatDayIsIt = DateTime.Now;
		int whatDayWasIt = PlayerPrefs.GetInt("lastDayPlayed");
		return whatDayIsIt.Day - whatDayWasIt;

	}

	string getStringTime()
	{
		DateTime now = DateTime.Now;
		return now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second; 
	}

	int getLastDayPlayed()
	{
		DateTime now = DateTime.Now;
		return now.Day;
	}

	public int Hunger {
		get{ return _hunger; }
		set{ _hunger = value; }
	}

	public int Happiness {
		get{ return _happiness; }
		set{ _happiness = value; }
	}

	public string name
	{
		get { return _name; }
		set { _name = value; }
	}

	public int Cleanliness
	{
		get { return _cleanliness; }
		set { _cleanliness = value; }
	}

	public int Coins
	{
		get { return _coins; }
		set { _coins = value; }
	}

	public void updateHappiness(int i)
	{
		Happiness += i;
		if (Happiness > 100)
		{
			Happiness = 100;
		}
		if (Happiness < 0)
		{
			Happiness = 0;
		}
	}

	public void updateHunger(int i)
	{
		Hunger += i;
		if (Hunger > 100)
		{
			Hunger = 100;
		}
		if (Hunger < 0)
		{
			Hunger = 0;
		}
	}

	public void updateCleanliness(int i)
	{
		Cleanliness += i;
		if (Cleanliness > 100)
		{
			Cleanliness = 100;
		}
		if (Cleanliness < 0)
		{
			Cleanliness = 0;
		}
	}

	public void updateCoins(int i)
	{
		Coins += i;
		if (Coins > 999)
		{
			Coins = 999;
		}
		if (Coins < 0)
		{
			Coins = 0;
		}
	}

	public void saveCat()
	{
		updateDevice();
		PlayerPrefs.SetInt("_hunger", _hunger);
		PlayerPrefs.SetInt("_happiness", _happiness);
		PlayerPrefs.SetInt("_cleanliness", _cleanliness);
		PlayerPrefs.SetInt("_coins", _coins);
	}

}
