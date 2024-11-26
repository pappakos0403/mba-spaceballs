using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    Vector2 initialPosition; // Az űrhajó kezdőpozíciója
    Gun[] guns; // Az űrhajóhoz tartozó fegyverek tömbje

    float moveSpeed = 3; // Alap mozgási sebesség
    float speedMultiplier = 1; // Sebesség szorzó (power-up-hoz)

    int hits = 3; // Találatok száma (életek száma)
    bool invincible = false; // Sérthetetlenségi állapot
    float invincibleTimer = 0; // Sérthetetlenségi időzítő
    float invincibleTime = 2; // Sérthetetlenség időtartama másodpercben

    // Bemeneti jelek az irányításhoz
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    bool shoot; // Lövés jelzése

    SpriteRenderer spriteRenderer; // Az űrhajó sprite renderelője

    GameObject shield; // Pajzs objektum
    int powerUpGunLevel = 0; // Power-up szintje a fegyverekhez

    public void Awake()
    {
        initialPosition = transform.position; // Kezdő pozíció mentése
        spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>(); // Sprite renderelő beállítása
    }

    void Start() // Egyszer fut le a játék indulásakor
    {
        shield = transform.Find("Shield").gameObject; // Pajzs objektum lekérése
        DeactivateShield(); // Pajzs kikapcsolása
        guns = transform.GetComponentsInChildren<Gun>(); // Fegyverek lekérése
        foreach (Gun gun in guns)
        {
            gun.isActive = true; // Fegyver aktiválása
            if (gun.powerUpLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false); // Fegyver kikapcsolása, ha power-up szükséges hozzá
            }
        }
    }

    void Update() // Minden képkocka frissítése
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPauseMenu();
        }

        // Bemenetek ellenőrzése (WASD és nyilak)
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        shoot = Input.GetKeyDown(KeyCode.Space); // Lövés jelzése
        if (shoot)
        {
            shoot = false;
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf) // Ha a fegyver aktív, lő
                {
                    gun.Shoot();
                }
            }
        }

        // Sérthetetlenség kezelése
        if (invincible)
        {
            if (invincibleTimer >= invincibleTime)
            {
                invincibleTimer = 0;
                invincible = false; // Sérthetetlenség vége
                spriteRenderer.enabled = true; // Sprite megjelenítése
            }
            else
            {
                invincibleTimer += Time.deltaTime; // Időzítő növelése
                spriteRenderer.enabled = !spriteRenderer.enabled; // Villogó effekt
            }
        }
    }

    private void FixedUpdate() // Fizikai mozgás kezelése fix időközönként
    {
        Vector2 pos = transform.position; // Az aktuális pozíció lekérése

        float moveAmount = moveSpeed * speedMultiplier * Time.fixedDeltaTime; // Mozgási mérték
        Vector2 move = Vector2.zero; // Mozgásirány inicializálása

        // Mozgás irányainak beállítása
        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }

        // Átlós mozgás normalizálása
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        pos += move; // Új pozíció beállítása

        // Képernyőhatárok ellenőrzése és beállítása
        if (pos.x <= 1f) pos.x = 1f;
        if (pos.x >= 16.8f) pos.x = 16.8f;
        if (pos.y <= 0.8f) pos.y = 0.8f;
        if (pos.y >= 9.2f) pos.y = 9.2f;

        transform.position = pos; // Végleges pozíció beállítása
    }

    void ActivateShield() // Pajzs aktiválása
    {
        shield.SetActive(true);
    }

    void DeactivateShield() // Pajzs deaktiválása
    {
        shield.SetActive(false);
    }

    bool HasShield() // Visszatérés pajzs állapotával
    {
        return shield.activeSelf;
    }

    void AddGuns() // Fegyverek hozzáadása power-up által
    {
        powerUpGunLevel++;
        foreach (Gun gun in guns)
        {
            if (gun.powerUpLevelRequirement <= powerUpGunLevel)
            {
                gun.gameObject.SetActive(true); // Aktiválás a szint szerint
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    void SetSpeedMultiplier(float mult) // Sebesség szorzó beállítása
    {
        speedMultiplier = mult;
    }

    public void ResetShip() // Hajó újraindítása
    {
        transform.position = initialPosition; // Kezdőpozíció visszaállítása
        DeactivateShield(); // Pajzs kikapcsolása
        powerUpGunLevel = -1; // Power-up szint alaphelyzetbe állítása
        AddGuns(); // Fegyverek frissítése
        SetSpeedMultiplier(1); // Sebesség visszaállítása
    }

    void Hit(GameObject gameObjectHit) // Ha találat éri a hajót
    {
        if (HasShield())
        {
            DeactivateShield(); // Pajzs deaktiválása, ha aktív
        }
        else
        {
            if (!invincible) // Ha nem sérthetetlen
            {
                hits--; // Élet csökkentése
                Level.instance.DecreaseHealth(1);

                if (hits <= 0)
                {
                    Destroy(gameObject); // Hajó elpusztítása, ha nincs több élet
                }
                else
                {
                    invincible = true; // Sérthetetlenség aktiválása
                }
                ResetShip(); // Hajó újraindítása
            }
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f; // Megállítja az időt
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive); // Additív betöltés
    }

    private void OnTriggerEnter2D(Collider2D collision) // Ütközések kezelése
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null && bullet.isEnemy)
        {
            Hit(bullet.gameObject); // Ellenséges lövedék találata
        }

        Destructable destructable = collision.GetComponent<Destructable>();
        if (destructable != null)
        {
            Hit(destructable.gameObject); // Elpusztítható objektum találata
        }

        PowerUp powerUp = collision.GetComponent<PowerUp>();
        if (powerUp)
        {
            if (powerUp.activateShield) ActivateShield(); // Pajzs power-up aktiválása
            if (powerUp.addGuns) AddGuns(); // Fegyver power-up aktiválása
            if (powerUp.increaseSpeed) SetSpeedMultiplier(speedMultiplier + 1); // Sebesség power-up aktiválása
            if (powerUp.increaseHealth)
            {
                Level.instance.IncreaseHealth(1); // +1 élet power up aktiválása
                if (hits < 5)
                {
                    hits++;
                }
            }
            Level.instance.AddScore(powerUp.pointValue); // Pontszám növelése
            Destroy(powerUp.gameObject); // Power-up elpusztítása
        }
    }

    
}