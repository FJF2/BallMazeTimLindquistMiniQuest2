using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using System.Security.Permissions;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI winText;
    public GameObject winTextObject;
    public GameObject loseTextObject;

    private int hp;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hp = 10;

        SetHpText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetHpText()
    {
        hpText.text = "HP: " + hp.ToString();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            if(hp < 10)
            {
                hp += 1;
                SetHpText();
            }
        }
        else if(other.gameObject.CompareTag("Winning"))
        {
            winText.text = "You Win! Time: " + Time.realtimeSinceStartup + " seconds.";
            winTextObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            hp -= 1;
            SetHpText();
            if(hp <= 0)
            {
                loseTextObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
