using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    // Az objektum középpontjának Y pozíciója a szinuszos mozgáshoz
    float sinCenterY;

    // A szinusz hullám amplitúdója
    public float amplitude = 2;

    // A szinusz hullám frekvenciája
    public float frequency = 0.5f;

    // Megfordítja a szinusz hullámot
    public bool inverted;

    // A Start függvény a játék elején fut le
    // Beállítja a szinuszos mozgás középpontját az aktuális Y pozícióra
    void Start()
    {
        sinCenterY = transform.position.y;
    }

    // Az Update függvény minden képkockánál lefut
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Az objektum jelenlegi pozíciójának lekérése
        Vector2 pos = transform.position;

        //Sin érték kiszámítása
        float sin = Mathf.Sin(pos.x * frequency) * amplitude;

        // Hullámzás megfordítása
        if (inverted)
        {
            sin *= -1;
        }

        // Az objektum új Y pozíciójának beállítása a szinusz kitéréssel
        pos.y = sinCenterY + sin;

        // Az objektum pozíciójának frissítése
        transform.position = pos;
    }
}
