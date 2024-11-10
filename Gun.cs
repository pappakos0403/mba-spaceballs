using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet; // A lövedék
    Vector2 direction; // A lövés iránya

    //Enemy automata lövés paraméterei
    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;

    void Start() // Egyszer hívódik meg az indításkor
    {
        
    }

    void Update() // Minden képkockánál lefut, figyeli a felhasználói inputokat
    {
        // A fegyver irányának megfelelően kiszámítja a lövés irányát
        // A Vector2.right az egységvektor (1, 0), amit a fegyver forgatásával alakít át
        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot) //Automata lövés mechanika
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer += Time.deltaTime;
                }
            }
            else
            {
                delayTimer += Time.deltaTime;
            }

        }
    }

    public void Shoot() //Lövés a fegyverrel
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity); // Új Bullet példány a fegyver aktuális pozíciójából
        Bullet goBullet = go.GetComponent<Bullet>(); // A Bullet komponens lekérése az újonnan létrehozott objektumból
        goBullet.direction = direction; // Beállítja a Bullet irányát a fegyver irányának megfelelően
    }
}