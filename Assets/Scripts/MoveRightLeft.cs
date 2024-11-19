using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightLeft : MonoBehaviour
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

        // Ellenőrzi, hogy az objektum elérte-e a -2.5-ös x pozíciót
        if (pos.x < -2.5f)
        {
            // Ha az objektum kiment a megadott határból, törli az objektumot
            Destroy(gameObject);
        }

        // Frissíti az objektum pozícióját
        transform.position = pos;
    }
}
