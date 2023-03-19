using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BugsManager : MonoBehaviour
{
    public static BugsManager Instance { get; private set; }
    private int _points;
    [SerializeField] private int numberOfCockroaches;
    [SerializeField] private int numberOfLadybugs;
    [SerializeField] private float timeLeft = 60.0f;

    private int _roachesKilled;
    private int _ladybugKilled;

    public void AddScore()
    {
        _points += 10;
        _roachesKilled++;
    }

    public void SubtractScore()
    {
        _points -= 15;
        _ladybugKilled++;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        _points = 0;
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;

        if (_ladybugKilled == numberOfLadybugs)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        if (timeLeft > 0 && _roachesKilled == numberOfCockroaches)
        {
            _points += 20;
        }

        if (_roachesKilled == numberOfCockroaches)
        {
            ScoreManager.Instance.SetUpPoints(_points);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Debug.Log("WON");
        }
    }
}