﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour {
    // Dreamlo object class properties
    [System.Serializable]
    public class Rootobject {
        public Dreamlo dreamlo;
    }

    [System.Serializable]
    public class Dreamlo {
        public Leaderboard leaderboard;
    }

    [System.Serializable]
    public class Leaderboard {
        public Entry[] entry;
        public Entry single;
    }

    [System.Serializable]
    public class Entry {
        public string name;
        public int score;
        public int seconds;
        public string text;
        public string date;
    }

    public GameObject scoreEntry, content, loading;

    private string leaderboard = "";
    private GameManager _gameManager;
    private PlayerGUID _playerGUID;

    private static string leaderboardUrl = "http://dreamlo.com/lb/";

    void Start () {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerGUID = GameObject.Find("PlayerGUID").GetComponent<PlayerGUID>();
    }

    public void Submit(string guid, int score, float timeAlive) {
        Debug.Log("Submitting Score");
        StartCoroutine(SubmitScore(guid, score, timeAlive));
    }

    void DisplayScores () {
        Rootobject scores = new Rootobject();
        scores = JsonUtility.FromJson<Rootobject>(leaderboard);

        if (scores.dreamlo.leaderboard.entry != null) {
            loading.SetActive(false);

            List<Entry> entries = new List<Entry>();

            foreach (Entry player in scores.dreamlo.leaderboard.entry) {
                entries.Add(player);
            }

            entries = entries.OrderByDescending(x => x.score).ThenByDescending(x => x.seconds).ToList();

            foreach (Entry player in entries) {
                GameObject childScore = Instantiate(scoreEntry, content.transform);
                childScore.transform.Find("Score").GetComponent<Text>().text = player.score.ToString();
                childScore.transform.Find("Time").GetComponent<Text>().text = player.seconds.ToString();

                if (player.name == _playerGUID.Guid) {
                    childScore.GetComponent<Image>().color = new Color32(80, 160, 89, 200); // Highlight current player scores
                }
            }
        } else {
            StartCoroutine(Fake2Scores());
            // loading.GetComponent<Text>().text = "No Highscores";
        }
    }

    IEnumerator Fake2Scores() {
        string url = leaderboardUrl + Secret.PrivateKey + "/add/Adam/0/0";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
        }

        url = leaderboardUrl + Secret.PrivateKey + "/add-json/Bert/0/0";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
            leaderboard = webRequest.downloadHandler.text;
            DisplayScores();
        }
    }

    IEnumerator SubmitScore(string guid, int score, float timeAlive) {
        string url = leaderboardUrl + Secret.PrivateKey + "/add/" + guid + "/" + score + "/" + timeAlive;
        Debug.Log("url: " + url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
            leaderboard = webRequest.downloadHandler.text;
        }
    }

    IEnumerator LoadScores() {
        string url = leaderboardUrl + Secret.PublicKey + "/json";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            yield return webRequest.SendWebRequest();
            leaderboard = webRequest.downloadHandler.text;
            DisplayScores();
        }
    }
}
