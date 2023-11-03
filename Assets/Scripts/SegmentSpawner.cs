using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SegmentSpawner : MonoBehaviour
{
    //spawn details
    private float spawnRate = 2f;
    private Vector3 spawnPosition = Vector3.forward * 100;
    private float deSpawnPositionZ = -20;

    //lists
    public List<GameObject> allSegments = new List<GameObject>();
    public List<GameObject> activeSegments = new List<GameObject>();
    public List<GameObject> inActiveSegments = new List<GameObject>();

    void Awake()
    {
        prepareSegement();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnSegment()
    {
        while (!FindObjectOfType<PlayerController>().isGameOver)
        {
            //spawn levels
            activateSegment();
            deactivateSegment();

            //increase game difficulty
            if (spawnRate > 0.35f)
                spawnRate -= Time.deltaTime * 2f;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void prepareSegement()
    {
        //create 4 copies of each segment
        foreach (var segment in allSegments)
        {
            for (int i = 0; i < 4; i++)
                Instantiate(segment, gameObject.transform);
        }

        //set each of them inactive
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            inActiveSegments.Add(transform.GetChild(i).gameObject);
        }
        Debug.Log("segments prepared and are inactive");
    }

    private void activateSegment()
    {
        //activate a random segment, set it's position and remove it from the list of inactive segments
        int randomIndex = Random.Range(0, inActiveSegments.Count);

        inActiveSegments[randomIndex].transform.position = spawnPosition;
        inActiveSegments[randomIndex].gameObject.SetActive(true);
        activeSegments.Add(inActiveSegments[randomIndex]);
        inActiveSegments.RemoveAt(randomIndex);
    }

    private void deactivateSegment()
    {
        
        for(int i = 0; i < activeSegments.Count; i++)
        {
            if(activeSegments[i].transform.position.z < deSpawnPositionZ)
        //if a segment is not in the camera's view deactivate it.
            {
                activeSegments[i].gameObject.SetActive(false);
                inActiveSegments.Add(activeSegments[i]);
                activeSegments.RemoveAt(i);
            }
        }
    }
}
