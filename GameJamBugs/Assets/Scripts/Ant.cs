using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Serialization;

public class Ant : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private SearchBarInGame searchBar;
    [SerializeField] private GameObject lettterToCarry;

    private List<GameObject> _letters;
    private SpriteRenderer _letterSprite;
    

    private void Awake()
    {
        speed /= 100;
        _letterSprite = lettterToCarry.GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        _letters = searchBar.Placeholders;
        gameObject.transform.position += transform.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col);
        if (col.gameObject.CompareTag("Search"))
        {
            if (searchBar.LettersTyped > 0)
            {
                Debug.Log("I can take the letter!");
                Debug.Log(searchBar.LetterSprites[_letters[searchBar.LettersTyped-1]]);
                _letterSprite.sprite = searchBar.LetterSprites[_letters[searchBar.LettersTyped-1]];
                searchBar.StealLetter();
            }
            else
            {
                Debug.Log("I cannot steal");
            }
        }
    }
}
