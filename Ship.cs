using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;

public class Ship: MonoBehaviour
{
    float moveSpeed = 3; // Alap mozgási sebesség

    // Bemeneti jelek az irányításhoz
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;

    void Start() // Egyszer fut le a játék indulásakor
    {

    }

    void Update() // Minden képkocka frissítése
    {
        // Bemenetek ellenőrzése (WASD és nyilak)
        moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private void FixedUpdate() // Fizikai mozgás kezelése fix időközönként
    {
        private void FixedUpdate() // Fizikai mozgás kezelése fix időközönként

        float moveAmount = moveSpeed * Time.fixedDeltaTime; // Mozgási mérték
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


        transform.position = pos; // Végleges pozíció beállítása
    }

}