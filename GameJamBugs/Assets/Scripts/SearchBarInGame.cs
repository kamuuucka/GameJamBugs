using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchBarInGame : MonoBehaviour
{
    [SerializeField] private List<Sprite> alphabet;
    [SerializeField] private List<GameObject> placeholders;
    [SerializeField] private List<AntManagerScriptableObject> wordsCorrect;
    [SerializeField] private List<Image> lines;
    [SerializeField] private AudioManager audioManager;

    private KeyCode _pressedKey;
    private bool _isCorrect = false;
    private string _typedWord = "";
    private Dictionary<GameObject, Sprite> _letterSprites = new Dictionary<GameObject, Sprite>();
    private Dictionary<GameObject, SpriteRenderer> _placeholdersSprites = new Dictionary<GameObject, SpriteRenderer>();
    private int _wordOnList = 0;
    private int _points = 100;
    private int _lettersTyped = 0;

    public Dictionary<GameObject, Sprite> LetterSprites => _letterSprites;
    public List<GameObject> Placeholders => placeholders;
    public int LettersTyped => _lettersTyped;

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

    private void Start()
    {
        ScoreManager.Instance.ResetScore();
        foreach (GameObject ph in placeholders)
        {
            _placeholdersSprites.Add(ph, ph.GetComponent<SpriteRenderer>());
        }

        foreach (var line in lines)
        {
            line.enabled = false;
        }
    }

    private void Update()
    {
        if (_points <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (_wordOnList > wordsCorrect.Count-1)
        {
            ScoreManager.Instance.SetUpPoints(_points);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            TypeLetter();
            WordCheck(wordsCorrect[_wordOnList].correctWord);
            if (Input.GetKeyUp(KeyCode.Backspace))
            {
                EraseBar();
            }

            if (Input.GetKeyUp(KeyCode.Return) && _isCorrect)
            {
                Debug.Log("You are amazing");
                audioManager.Play("wordCorrect");
                lines[_wordOnList].enabled = true;
                _wordOnList++;
                EraseBar();
            }
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
            audioManager.Play("keyboardTyping");
            char pressedKey = (char)_pressedKey;
            
            int letterNumber = Convert.ToInt32(pressedKey);
            letterNumber -= 97;
            if (letterNumber is <= 25 and >= 0)
            {
                _typedWord += pressedKey;
                _placeholdersSprites[placeholders[_lettersTyped]].sprite = alphabet[letterNumber];
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
        
            foreach (GameObject placeholder in placeholders)
            {
                _placeholdersSprites[placeholder].sprite = null;
                _lettersTyped = 0;
                _typedWord = "";
                _isCorrect = false;
                _letterSprites.Clear();
            }
        
    }

    /// <summary>
    /// Check if the collected word is the same as the word added in the inspector
    /// </summary>
    /// <param name="word"></param>
    private void WordCheck(string word)
    {
        Debug.Log($"Yourword: {_typedWord} WordToFind: {word}");
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
        audioManager.Play("stealLetter");
        _points--;
        _typedWord = _typedWord.Remove(_typedWord.Length - 1,1);
        _isCorrect = false;
        placeholders[_lettersTyped - 1].GetComponent<SpriteRenderer>().sprite = null;
        _letterSprites.Remove(placeholders[_lettersTyped - 1]);
        _lettersTyped--;
        
        Debug.Log(_typedWord);
    }
}
