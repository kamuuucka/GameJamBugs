using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputBar : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text debug;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            CheckText(inputField.text);
        }
    }

    private void CheckText(string text)
    {
        if (text.Equals("kama"))
        {
            Debug.Log("GOOOOD");
            debug.alpha = 100;
        }
        else
        {
            Debug.Log("Noooo");
            debug.alpha = 0;
        }
    }
}