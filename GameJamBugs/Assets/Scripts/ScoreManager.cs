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
    }

    public void DisplayScore()
    {
        score.text = _points.ToString();
    }

    public void ResetScore()
    {
        _points = 0;
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

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            DisplayScore();
        }
    }
}
