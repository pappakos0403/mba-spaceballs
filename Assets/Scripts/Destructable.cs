using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructable : MonoBehaviour
{

    public bool canBeDestroyed = false; // Objektum megsemmisíthető állapota
    public bool isFinalBoss = false; // Final Boss ellenőrző
    private int finalBossHealth = 1000; // Final Boss élete
    public int scoreValue = 100; // Megsemmisítésért járó pontszám

    void Start()
    {
        Level.instance.AddDestructable();
    }

    void Update()
    {
        if (transform.position.x < 17.5f && !canBeDestroyed) // Enemy lelőhető, ha a képernyőn található
        {
            canBeDestroyed = true;
            Gun[] guns = transform.GetComponentsInChildren<Gun>(); // Enemy fegyvere aktív, ha a képernyőn található
            foreach (Gun gun in guns)
            {
                gun.isActive = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // Enemy sebzése
    {
        if(!canBeDestroyed)
        {
            return;
        }

        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (!bullet.isEnemy)
            {
                if (!isFinalBoss) // Enemy típus esete
                {
                    Level.instance.AddScore(scoreValue);
                    Destroy(gameObject);
                    Destroy(bullet.gameObject);
                }
                else // Final Boss típus esete
                {
                    Destroy(bullet.gameObject);
                    finalBossHealth--;
                    if (finalBossHealth <= 0)
                    {
                        Level.instance.AddScore(scoreValue);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        Level.instance.RemoveDestructable();
    }
}
