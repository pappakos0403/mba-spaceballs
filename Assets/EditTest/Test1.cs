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


