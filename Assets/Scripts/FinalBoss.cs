using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
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
    private Vector2[] targetPositions = { new Vector2(15.8f, 5f), new Vector2(15.8f, 2.5f), new Vector2(15.8f, 7.5f) };
    private int currentTargetIndex = 0;
    private float timeToNextTarget = 0f;

    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        // Ha a Final Boss elérte az x = 15.8f koordinátát, elindítjuk a mozgási ciklust
        if (!isAtTargetX && currentPosition.x <= 15.8f)
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
            // Mozgás az x = 15.8f koordináta felé
            currentPosition.x -= moveSpeed * Time.deltaTime;
            if (currentPosition.x <= 15.8f)
            {
                currentPosition.x = 15.8f;
            }
            transform.position = currentPosition;
        }
    }

    private void SpawnEnemies()
    {
        Vector3 spawnposition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        for (int i = 0; i < 3; i ++) // Random enemy spawnolás
        {
            float spawnOffsetY = i == 1 ? 1f : (i == 2 ? -1f : 0f); // Spawn koordináták eloszlása az Y tengelyen
            Vector3 adjustedPosition = new Vector3(spawnposition.x, spawnposition.y + spawnOffsetY, spawnposition.z);

            int enemyType = UnityEngine.Random.Range(1,4); // Random enemy típus kiválasztása

            if (enemyType == 1) Instantiate(enemyType1, adjustedPosition, Quaternion.identity); // Enemy spawnolása
            if (enemyType == 2) Instantiate(enemyType2, adjustedPosition, Quaternion.identity);
            if (enemyType == 3) Instantiate(enemyType3, adjustedPosition, Quaternion.identity);
        }
    }
}