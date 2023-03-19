using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class CockroachBehaviour : MonoBehaviour
{
    [SerializeField] private float speed = 9.5f;
    [SerializeField] private float waitingTime = 0.5f;
    [SerializeField] private float minX;
    [SerializeField] private float minY;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private bool isLadyBug;

    public bool IsLadyBug
    {
        get => isLadyBug;
        set => isLadyBug = value;
    }

    [SerializeField] private Sprite deadBug;
    [SerializeField] private Animator animator;

    private Vector2 _waypoint = Vector2.zero;
    private bool _dead;

    private bool _hidden;
    private SpriteRenderer _spriteRenderer;


    private void OnMouseDown()
    {
        if (!_hidden && !_dead)
        {
            _dead = true;
            if (isLadyBug)
            {
                BugsManager.Instance.SubtractScore();
            }
            else
            {
            BugsManager.Instance.AddScore();
                
            }
            Debug.Log("He dead dead");
        }
       
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartRunning());
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_dead)
        {
            float angle = Mathf.Atan2(_waypoint.y - transform.position.y, _waypoint.x - transform.position.x) *
                Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.position = Vector2.MoveTowards(transform.position, _waypoint, speed * Time.deltaTime);
        }
        else
        {
            speed = 0;
            animator.enabled = false;
            _spriteRenderer.sprite = deadBug;
        }
    }

    IEnumerator StartRunning()
    {
        while (!_dead)
        {
            _waypoint = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            yield return new WaitForSeconds(waitingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hiding"))
        {
            _hidden = true;
            Debug.Log("I'm hiding hehe");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Hiding"))
        {
            _hidden = false;
            Debug.Log("No more hiding");
        }
    }
}