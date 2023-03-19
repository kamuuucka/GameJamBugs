using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Image notes;
    [SerializeField] private AudioManager audioManager;

    public void OpenOverlay()
    {
        notes.gameObject.SetActive(true);
        audioManager.Play("button");
    }

    public void CloseOverlay()
    {
        notes.gameObject.SetActive(false);
        audioManager.Play("button");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void WikiHow()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
