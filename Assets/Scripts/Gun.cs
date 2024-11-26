using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int powerUpLevelRequirement = 0;

    public Bullet bullet; // A lövedék
    Vector2 direction; // A lövés iránya

    public bool autoShoot = false;
    public float shootIntervalSeconds = 0.5f;
    public float shootDelaySeconds = 0.0f;
    float shootTimer = 0f;
    float delayTimer = 0f;
    AudioSource _audio;
    public bool isActive = false;

    private void Awake()
    {
        if (autoShoot)
        {
            if (GetComponentInParent<AudioSource>() == null)
            {
                transform.parent.gameObject.AddComponent<AudioSource>();
            }
            _audio = GetComponentInParent<AudioSource>();
            _audio.clip = (AudioClip)Resources.Load("EnemyShoot");
            _audio.volume = PlayerPrefs.GetInt("volume", 100) / 100f;
            _audio.playOnAwake = false;
        }
        
    }

    void Update() // Minden képkockánál lefut, figyeli a felhasználói inputokat
    {
        if (!isActive)
        {
            return;
        }

        // A fegyver irányának megfelelően kiszámítja a lövés irányát
        // A Vector2.right az egységvektor (1, 0), amit a fegyver forgatásával alakít át
        direction = (transform.localRotation * Vector2.right).normalized;

        if (autoShoot)
        {
            if (delayTimer >= shootDelaySeconds)
            {
                if (shootTimer >= shootIntervalSeconds)
                {
                    _audio.Play();
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
