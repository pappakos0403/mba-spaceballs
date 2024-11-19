using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector2 direction = new Vector2(1, 0); // A lövedék iránya jobbra (1,0)
    public float speed = 10; // A lövedék sebessége

    public Vector2 velocity; // A lövedék sebessége, amely az irány és a sebesség szorzata

    public bool isEnemy = false;
    
    void Start() // Egyszer hívódik meg az indításkor
    {
        Destroy(gameObject, 3); // A kilőtt lövedék 3 sec múlva eltűnik
        DontDestroyOnLoad(gameObject);
    }

    void Update() // Minden képkockánál lefut, figyeli a felhasználói inputokat
    {
        velocity = direction * speed; // Meghatározza a lövedék mozgásának mértékét

    }

    private void FixedUpdate() // Fix időközönként hívodik, itt történik a lövedék tényleges mozgása
    {
        Vector2 pos = transform.position; // Lekéri a lövedék aktuális pozícióját

        pos += velocity * Time.fixedDeltaTime; // Frissíti a pozíciót a sebesség és az eltelt idő alapján

        transform.position = pos; // Beállítja a lövedék új pozícióját
    }
}
