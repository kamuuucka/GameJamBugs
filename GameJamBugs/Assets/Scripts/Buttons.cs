using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    [SerializeField] private Image notes;
    
    public void OpenOverlay()
    {
        notes.gameObject.SetActive(true);
    }

    public void CloseOverlay()
    {
        notes.gameObject.SetActive(false);
    }
}
