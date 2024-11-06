using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet bullet; // A lövedék
    Vector2 direction; // A lövés iránya

    void Start() // Egyszer hívódik meg az indításkor
    {
        
    }

    void Update() // Minden képkockánál lefut, figyeli a felhasználói inputokat
    {
        // A fegyver irányának megfelelően kiszámítja a lövés irányát
        // A Vector2.right az egységvektor (1, 0), amit a fegyver forgatásával alakít át
        direction = (transform.localRotation * Vector2.right).normalized;
    }

    public void Shoot() //Lövés a fegyverrel
    {
        GameObject go = Instantiate(bullet.gameObject, transform.position, Quaternion.identity); // Új Bullet példány a fegyver aktuális pozíciójából
        Bullet goBullet = go.GetComponent<Bullet>(); // A Bullet komponens lekérése az újonnan létrehozott objektumból
        goBullet.direction = direction; // Beállítja a Bullet irányát a fegyver irányának megfelelően
    }
}