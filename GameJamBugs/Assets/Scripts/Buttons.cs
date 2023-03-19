using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

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
}
