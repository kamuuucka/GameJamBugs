using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugsManager : MonoBehaviour
{
    [SerializeField] private List<CockroachBehaviour> bugs;
    public static BugsManager Instance { get; private set; }
    private int _points;

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
        foreach (var bug in bugs)
        {
            if (bug.Dead && !bug.Counted)
            {
                if (bug.IsLadyBug)
                {
                    _points-=3;
                }
                else
                {
                    _points++;
                }
                bug.Counted = true;
            }
        }
    }
}
