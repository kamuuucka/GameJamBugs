using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ant : MonoBehaviour
{
    [SerializeField] private GameObject ant;
    [SerializeField] private TMP_InputField inputField;

    private void Update()
    {
        ant.transform.position += transform.up * 0.02f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Search")
        {
            Debug.Log("yes");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Search"))
        {
            Debug.Log("trigger");
        }
    }
}
