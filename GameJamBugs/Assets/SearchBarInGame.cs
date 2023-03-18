using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchBarInGame : MonoBehaviour
{
    [SerializeField] private List<Sprite> alphabet;
    [SerializeField] private List<GameObject> placeholders;
    [SerializeField] private string correctWord;

    private KeyCode _pressedKey;
    private bool _isCorrect = false;
    private string _typedWord = "";

    private int _lettersTyped = 0;

    /// <summary>
    /// Used to detect the pressed key without having to know which key is it first
    /// </summary>
    private void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyUp)
        {
            _pressedKey = e.keyCode;
        }
    }

    private void Update()
    {
            TypeLetter();
            WordCheck(correctWord);
            EraseBar();

            if (Input.GetKeyUp(KeyCode.Return) && _isCorrect)
            {
                Debug.Log("You are amazing");
            }
    }

    /// <summary>
    /// Put the correct sprite in the placeholder based on what button was pressed.
    /// Makes sure that you can't paste values greater than 25 (Z) and smaller than 0 (A)
    /// </summary>
    private void TypeLetter()
    {
        if (Input.GetKeyUp(_pressedKey) && _lettersTyped < placeholders.Count && !_isCorrect)
        {
            char pressedKey = (char)_pressedKey;
            _typedWord += pressedKey;
            int letterNumber = Convert.ToInt32(pressedKey);
            letterNumber -= 97;
            if (letterNumber is <= 25 and >= 0)
            {
                placeholders[_lettersTyped].GetComponent<SpriteRenderer>().sprite = alphabet[letterNumber];
                _lettersTyped++;
            }
        }
    }

    /// <summary>
    /// Erase the searchbar from all of the letters.
    /// </summary>
    private void EraseBar()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            foreach (GameObject placeholder in placeholders)
            {
                placeholder.GetComponent<SpriteRenderer>().sprite = null;
                _lettersTyped = 0;
                _typedWord = "";
                _isCorrect = false;
            }
        }
    }

    private void WordCheck(string word)
    {
        Debug.Log($"Typed word:{_typedWord}Desired word:{word}.");
        if (_typedWord==word)
        {
            _isCorrect = true;
        }
        else
        {
            _isCorrect = false;
        }
    }
}
