using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SegmentController : MonoBehaviour
{
    [SerializeField] float  speed;
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.isGameOver && playerController.isGameStarted)
        {
            transform.position += Vector3.back * speed;
        }
    }
}
