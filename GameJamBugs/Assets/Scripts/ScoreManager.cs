using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text score;
    public static int _points;

    public void SetUpPoints(int value)
    {
        _points += value;
        Debug.Log("Total: " + _points);
    }

    public void DisplayScore()
    {
        Debug.Log(_points);
        score.text = _points.ToString();
    }

    public void ResetScore()
    {
        _points = 0;
    }
    
    private void Awake()
    {
        Debug.Log(Instance + "awake called");
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            DisplayScore();
        }
    }
}
