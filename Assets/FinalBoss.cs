using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public float moveSpeed = 2.5f; // Final Boss mozgási sebessége
    public float cycleTime = 2f; // Egy ciklus időtartama 2 másodperc
    private bool isAtTargetX = false;

    public GameObject enemyType1;
    public GameObject enemyType2;
    public GameObject enemyType3;
    
    private Vector2 currentPosition;
    private Vector2[] targetPositions = { new Vector2(13.5f, 5f), new Vector2(13.5f, 2.5f), new Vector2(13.5f, 7.5f) };
    private int currentTargetIndex = 0;
    private float timeToNextTarget = 0f;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        // Ha a Final Boss elérte az x = 13.5f koordinátát, elindítjuk a mozgási ciklust
        if (!isAtTargetX && currentPosition.x <= 13.5f)
        {
            isAtTargetX = true;
            timeToNextTarget = cycleTime;
        }

        if (isAtTargetX)
        {
            timeToNextTarget -= Time.deltaTime;

            // Ha elérte a célpontot, lépjen a következő célpontra
            if (timeToNextTarget <= 0f)
            {
                currentTargetIndex = (currentTargetIndex + 1) % targetPositions.Length;
                timeToNextTarget = cycleTime;
                SpawnEnemies();
            }

            // Mozgás a célpont felé
            currentPosition = Vector2.Lerp(currentPosition, targetPositions[currentTargetIndex], moveSpeed * Time.deltaTime);
            transform.position = currentPosition;
        }
        else
        {
            // Mozgás az x = 13.5f koordináta felé
            currentPosition.x -= moveSpeed * Time.deltaTime;
            if (currentPosition.x <= 13.5f)
            {
                currentPosition.x = 13.5f;
            }
            transform.position = currentPosition;
        }
    }

    private void SpawnEnemies()
    {
        Vector3 spawnposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(enemyType1, spawnposition, Quaternion.identity);
    }
}