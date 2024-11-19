using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test1
{
    [Test]
    public void BulletInitialization_DefaultDirection_IsCorrect()
    {
        // Arrange - Előkészítés
        GameObject bulletObj = new GameObject();
        Bullet bullet = bulletObj.AddComponent<Bullet>();

        // Act & Assert - Végrehajtás és ellenőrzés
        Assert.AreEqual(new Vector2(1, 0), bullet.direction, "Az alapértelmezett iránynak (1,0)-nak kell lennie");
        Assert.AreEqual(10f, bullet.speed, "Az alapértelmezett sebességnek 10-nek kell lennie");
    }
    
    [UnityTest]
public IEnumerator ShipShield_InitialStateIsInactive()
{
    // Arrange
    GameObject shipObj = new GameObject();
    GameObject shieldObj = new GameObject("Shield");
    shieldObj.transform.parent = shipObj.transform;
    Ship ship = shipObj.AddComponent<Ship>();

    // Wait for Start() to be called by Unity
    yield return null;

    // Assert
    Assert.IsFalse(shieldObj.activeSelf, "A pajzsnak kezdetben inaktívnak kell lennie");
}


    private GameObject CreateTestBullet()
    {
        GameObject bulletObj = new GameObject("TestBullet");
        bulletObj.AddComponent<Bullet>();
        return bulletObj;
    }

    [TearDown]
    public void Cleanup()
    {
        // Tesztek után takarítás
        // Minden létrehozott objektum törlése
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in objects)
        {
            Object.DestroyImmediate(obj);
        }
    }
}
