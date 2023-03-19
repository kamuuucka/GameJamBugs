using System.Collections;
using UnityEngine;

public class AntManager : MonoBehaviour
{
    [SerializeField] private GameObject ant;
    [SerializeField] private float timeBetweenAnts = 5;
    [SerializeField] private SearchBarInGame searchBar;

    private void Start()
    {
        StartCoroutine(GenerateAnts());
    }

    IEnumerator GenerateAnts()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenAnts);
            ant.GetComponent<Ant>().SearchBar = searchBar;
            GameObject newAnt = Instantiate(ant, transform.position, Quaternion.identity);
            newAnt.transform.parent = gameObject.transform;
        }
    }
}