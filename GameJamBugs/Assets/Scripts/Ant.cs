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

    public SearchBarInGame SearchBar
    {
        get => searchBar;
        set => searchBar = value;
    }

    [SerializeField] private GameObject letterToCarry;

    public GameObject LetterToCarry
    {
        get => letterToCarry;
        set => letterToCarry = value;
    }

    private List<GameObject> _letters;
    private SpriteRenderer _letterSprite;
    private Vector3 _target;
    private Vector3 _lastDirection;
    private bool _hasLetter;
    private float _step;


    private void Awake()
    {
        _letterSprite = letterToCarry.GetComponent<SpriteRenderer>();
        _letterSprite.sprite = null;
    }

    private void Update()
    {
        _letters = searchBar.Placeholders;
        _step = speed * Time.deltaTime;
        if (searchBar.LettersTyped > 0 && !_hasLetter)
        {
            _target = _letters[searchBar.LettersTyped - 1].transform.position;
            //Vector3 antPosition = transform.position;
            float angle = Mathf.Atan2(_target.y - transform.position.y, _target.x - transform.position.x) *
                Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            _lastDirection = (_target - transform.position).normalized;
            transform.position += transform.up * _step;
        }
        else if (_hasLetter)
        {
            transform.position += _step * _lastDirection;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Search"))
        {
            if (searchBar.LettersTyped > 0 && !_hasLetter)
            {
                PickUpLetter(col);
            }
            else
            {
                Debug.Log("I cannot steal");
            }
        }
    }

    private void Move(float step)
    {
        
    }

    private void PickUpLetter(Collider2D col)
    {
        if (_letters[searchBar.LettersTyped - 1].Equals(col.gameObject))
        {
            _letterSprite.sprite = searchBar.LetterSprites[_letters[searchBar.LettersTyped - 1]];
            searchBar.StealLetter();
            _hasLetter = true;
        }
    }
}