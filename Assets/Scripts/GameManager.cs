using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject[] spawnPoints;
    public float timer;
    public float timeBetweenSpawns;
    public float speedMultiplier;
    public static float distance;
    public Text scoreUI;
    public Text highScore;
    void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        scoreUI.text = "Score : " + distance.ToString("F2");
        speedMultiplier += Time.deltaTime * 0.1f;
        timer += Time.deltaTime;
        distance += Time.deltaTime * 0.8f;
        highScore.text = "High Score : " + PlayerPrefs.GetFloat("HighScore").ToString("F2");
        if (timer > timeBetweenSpawns)
        {
            timer = 0;
            int randNum = Random.Range(0, 3);
            Instantiate(spawnObject, spawnPoints[randNum].transform.position, Quaternion.identity);
        }
        if (distance > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", distance);
            highScore.text = "High Score : " + distance.ToString("F2");
        }
    }
}
