using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    float angle = 0;

    Quaternion desiredRotQ;

    float targetTime = 0.5f;

    bool enableInputInCredits = false;

    enum Sides { START, CREDIT, EXIT, SCOREBOARD}

    Sides currentSide = Sides.START;

    public GameObject Credits;
    public GameObject MenuCube;

    public AudioClip MusicClip;
    public AudioSource MusicSource;


    void Start() {
        Credits.SetActive(false);
        MenuCube.SetActive(true);

        MusicSource.clip = MusicClip;
	}

	// Update is called once per frame
	void Update () {
        if (MenuCube.activeSelf == true)
        {
            if (Input.GetButton("Left") && Input.GetButton("Right"))
            {
                MusicSource.Play();
                FindSide();
            }
            else
            {
                if (Input.GetButtonUp("Left"))
                {
                    MusicSource.Play();
                    angle += 90;
                    currentSide += 1;
                    if ((int)currentSide == 4)
                    {
                        currentSide = 0;
                    }
                }
                if (Input.GetButtonUp("Right"))
                {
                    MusicSource.Play();
                    angle -= 90;
                    currentSide -= 1;
                    if ((int)currentSide == -1)
                    {
                        currentSide = Sides.SCOREBOARD;
                    }
                }
            }

            desiredRotQ = Quaternion.Euler(MenuCube.transform.eulerAngles.x, angle, MenuCube.transform.eulerAngles.z);
            MenuCube.transform.rotation = Quaternion.Lerp(MenuCube.transform.rotation, desiredRotQ, Time.deltaTime * 10f);
        }
        else
        {
            //Simple timer to start scrolling after 2 seconds.
            if (targetTime > 0)
            {
                targetTime -= Time.deltaTime;

                if (targetTime <= 0.0f)
                {
                    enableInputInCredits = true;
                }
            }

            if (Input.GetButtonUp("Left")||Input.GetButtonUp("Right")) { EnableMenu(); }
        }

    }

    void FindSide()
    {
        switch (currentSide)
        {
            case (Sides.START):
                SceneManager.LoadScene("Game", LoadSceneMode.Single);
                break;
            case (Sides.CREDIT):
                Credits.SetActive(true);
                MenuCube.SetActive(false);
                break;
            case (Sides.SCOREBOARD):
                SceneManager.LoadScene("HighScores");
                break;
        }

    }

    void EnableMenu()
    {
        if (enableInputInCredits)
        {
            MusicSource.Play();
            Credits.SetActive(false);
            MenuCube.SetActive(true);
            enableInputInCredits = false;
            targetTime = 0.5f;
        }
    }
}
