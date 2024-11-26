using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GunTests
{
    [SetUp]
    public void Setup()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            GameObject.DestroyImmediate(bullet);
        }
    }

    [Test]
    public void Gun_InitialState_ShouldBeInactive()
    {
        // Arrange
        GameObject gameObject = new GameObject();
        Gun gun = gameObject.AddComponent<Gun>();

        // Assert
        Assert.IsFalse(gun.isActive, "Gun should be initially inactive");
    }

    [Test]
    public void Gun_Shoot_ShouldCreateBullet()
    {
        // Arrange
        GameObject gunGameObject = new GameObject();
        Gun gun = gunGameObject.AddComponent<Gun>();

        GameObject bulletPrefab = new GameObject();
        Bullet bulletComponent = bulletPrefab.AddComponent<Bullet>();
        gun.bullet = bulletComponent;

        gun.isActive = true;

        // Act
        gun.Shoot();

        // Assert
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        Assert.AreEqual(1, bullets.Length, "A bullet should be created when shooting");
    }

    [Test]
    public void Gun_InactiveShouldNotShoot()
    {
        // Arrange
        GameObject gunGameObject = new GameObject();
        Gun gun = gunGameObject.AddComponent<Gun>();

        GameObject bulletPrefab = new GameObject();
        Bullet bulletComponent = bulletPrefab.AddComponent<Bullet>();
        gun.bullet = bulletComponent;

        // Act
        gun.Shoot();

        // Assert
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        Assert.AreEqual(0, bullets.Length, "No bullet should be created when gun is inactive");
    }
}

public class ShipTests
{
    private GameObject shipObject;
    private Ship ship;

    [SetUp]
    public void Setup()
    {
        // Létrehozunk egy új GameObject-et a teszthez
        shipObject = new GameObject();
        ship = shipObject.AddComponent<Ship>();

        // Alapvető komponensek hozzáadása
        GameObject sprite = new GameObject("Sprite");
        sprite.transform.parent = shipObject.transform;
        sprite.AddComponent<SpriteRenderer>();

        GameObject shield = new GameObject("Shield");
        shield.transform.parent = shipObject.transform;

        // Inicializálás
        ship.Awake();
        ship.Start();
    }

    [Test]
    public void ResetShip_SetsPositionToInitial()
    {
        // Állítsuk át az űrhajót egy új pozícióra
        shipObject.transform.position = new Vector2(5, 5);

        // Hívjuk meg a ResetShip metódust
        ship.ResetShip();

        // Ellenőrizzük, hogy visszaállt-e az eredeti pozícióra
        Assert.AreEqual(new Vector2(0.0f, 0.0f), (Vector2)shipObject.transform.position);
    }

    [Test]
    public void ActivateShield_ShieldIsActive()
    {
        // Aktiváljuk a pajzsot
        ship.ActivateShield();

        // Ellenőrizzük, hogy a pajzs aktív
        Assert.IsTrue(shipObject.transform.Find("Shield").gameObject.activeSelf);
    }

    [Test]
    public void DeactivateShield_ShieldIsNotActive()
    {
        // Aktiváljuk, majd deaktiváljuk a pajzsot
        ship.ActivateShield();
        ship.DeactivateShield();

        // Ellenőrizzük, hogy a pajzs inaktív
        Assert.IsFalse(shipObject.transform.Find("Shield").gameObject.activeSelf);
    }

    [Test]
    public void Hit_DoesNotDecreaseLife_WhenInvincible()
    {
        // Sérthetetlenség bekapcsolása
        ship.invincible = true;

        // Ütközés szimulálása
        int initialHits = ship.hits;
        ship.Hit(null);

        // Ellenőrizzük, hogy az élet nem csökkent
        Assert.AreEqual(initialHits, ship.hits);
    }

    [TearDown]
    public void TearDown()
    {
        // Takarítás a teszt végén
        Object.DestroyImmediate(shipObject);
    }
}

