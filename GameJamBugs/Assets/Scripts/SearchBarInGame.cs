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
    private Dictionary<GameObject, Sprite> _letterSprites = new Dictionary<GameObject, Sprite>();

    public Dictionary<GameObject, Sprite> LetterSprites
    {
        get => _letterSprites;
        set => _letterSprites = value;
    }

    private int _lettersTyped = 0;

    public List<GameObject> Placeholders
    {
        get => placeholders;
        set => placeholders = value;
    }

    public int LettersTyped
    {
        get => _lettersTyped;
        set => _lettersTyped = value;
    }

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
        Debug.Log(_lettersTyped < placeholders.Count);
        Debug.Log(_isCorrect);
        if (Input.GetKeyUp(_pressedKey) && _lettersTyped < placeholders.Count && !_isCorrect)
        {
            char pressedKey = (char)_pressedKey;
            _typedWord += pressedKey;
            int letterNumber = Convert.ToInt32(pressedKey);
            letterNumber -= 97;
            if (letterNumber is <= 25 and >= 0)
            {
                placeholders[_lettersTyped].GetComponent<SpriteRenderer>().sprite = alphabet[letterNumber];
                _letterSprites.Add(placeholders[_lettersTyped], alphabet[letterNumber]);
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
                _letterSprites.Clear();
            }
        }
    }

    /// <summary>
    /// Check if the collected word is the same as the word added in the inspector TODO: list of words
    /// </summary>
    /// <param name="word"></param>
    private void WordCheck(string word)
    {
        if (_typedWord==word)
        {
            _isCorrect = true;
        }
        else
        {
            _isCorrect = false;
        }
    }

    public void StealLetter()
    {
        _typedWord.Remove(_typedWord.Length - 1);
        _isCorrect = false;
        placeholders[_lettersTyped - 1].GetComponent<SpriteRenderer>().sprite = null;
        _letterSprites.Remove(placeholders[_lettersTyped - 1]);
        _lettersTyped--;
        
        
    }
}
