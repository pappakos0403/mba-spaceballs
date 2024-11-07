using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructable : MonoBehaviour
{    
    // Megsemmisíthető állapot jelzése
    bool canBeDestroyed = false;

    void Start()
    {
        // (Üres, nincs szükség kezdeti beállításra)
    }

    void Update()
    {
        // Ha az x pozíció kisebb, mint 17.5, akkor megsemmisíthető
        if (transform.position.x < 17.5f)
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ha nem megsemmisíthető, kilépés
        if (!canBeDestroyed)
        {
            return;
        }
        
        // Ha ütközés történt egy Bullet-tel, mindkét objektumot megsemmisítjük
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(bullet.gameObject);
        }
    }
}
