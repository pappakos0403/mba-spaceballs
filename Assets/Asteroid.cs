using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float moveSpeed = 7.5f; //Aszteroida sebessége
    public float rotationSpeed = 300f; //Forgatási sebesség
    
    void Start()
    {

    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        // Az objektum jelenlegi pozíciójának lekérése
        Vector2 pos = transform.position;

        // Az objektum balra mozgatása az x koordináta csökkentésével
        pos.x -= moveSpeed * Time.fixedDeltaTime;

        //Forgatás a Z tengely körül
        transform.Rotate(0, 0, rotationSpeed * Time.fixedDeltaTime);

        if (pos.x < -2.5f)
        {
            //Határon túli objektum törlése
            Destroy(gameObject);
        }

        // Frissíti az objektum pozícióját
        transform.position = pos;
    }

}
