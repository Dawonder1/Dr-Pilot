using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator shoot()
    {
        while(playerController.isGameStarted && !playerController.isGameOver)
        {
            Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);
            yield return new WaitForSeconds(1);
        }
    }
}
