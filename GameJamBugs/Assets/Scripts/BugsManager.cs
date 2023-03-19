using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsManager : MonoBehaviour
{
    public static BugsManager Instance { get; private set; }
    private int _points;
    [SerializeField] private int numberOfCockroaches;

    private int _roachesKilled;

    public void AddScore()
    {
        _points++;
        _roachesKilled++;
    }

    public void SubtractScore()
    {
        _points--;
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
    }

    private void Update()
    {
        if (_roachesKilled == numberOfCockroaches)
        {
            Debug.Log("THE END YOU WON WOOOO");
        }
    }
}
