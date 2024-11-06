using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction = new Vector2(1, 0); // A lövedék iránya jobbra (1,0)

    public float speed = 10; // A lövedék sebessége

    public Vector2 velocity; // A lövedék sebessége, amely az irány és a sebesség szorzata

    void Start() // Egyszer hívódik meg az indításkor
    {
        Destroy(gameObject, 3); // A kilőtt lövedék 3 sec múlva eltűnik
    }

    void Update() // Minden képkocka frissítése
    {
        
    }
}