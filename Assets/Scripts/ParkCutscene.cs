using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ParkCutscene : MonoBehaviour
{

    internal Transform thisTransform;
    Animator animator;
    SpriteRenderer testflip;

    public float moveSpeed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.transform;
        animator = GetComponent<Animator>();
        testflip = GetComponent<SpriteRenderer>();
        animator.SetInteger("Speed", 2);
        testflip.flipX = true;

        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LastScene", currentScene);
        PlayerPrefs.Save();
        //Debug.Log(PlayerPrefs.GetString("LastScene"));
    }

    // Update is called once per frame
    void Update()
    {
        thisTransform.position += Vector3.right * Time.deltaTime * moveSpeed;

        Vector3 finish = new Vector3(8.8f, -2.5f, 0.0f);
        if (thisTransform.position.x > 8.7f && thisTransform.position.x < 8.9f) 
        {
            SceneManager.LoadScene("Game");
        }
    }
}
