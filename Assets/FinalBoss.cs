using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    // A sebesség, amellyel az objektum vízszintesen mozog
    public float moveSpeed = 2.5f;

    // A Start függvény a játék elején fut le
    void Start()
    {
        
    }

    // Az Update függvény minden képkockánál lefut
    void Update()
    {
        
    }

    // A FixedUpdate egyenletes időközönként fut, fizikai alapú frissítésekhez megfelelő
    private void FixedUpdate()
    {
        // Az objektum jelenlegi pozíciójának lekérése
        Vector2 pos = transform.position;

        // Az objektum balra mozgatása az x koordináta csökkentésével
        pos.x -= moveSpeed * Time.fixedDeltaTime;

        if (pos.x <= 13.5f)
        {
            pos.x = 13.5f;
            moveSpeed = 0f;
        }

        // Frissíti az objektum pozícióját
        transform.position = pos;
    }
}