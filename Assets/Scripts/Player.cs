using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; // how fast the player can move
    private Rigidbody2D rb; // this variable contains all the physics inside Unity
    private Animator anim; // this variable contains all the animations inside Unity
    private Vector2 moveAmount; // calculate how much we want the player to move on

    public int health; // player's health

    public Image[] hearts;
    public Sprite fullHeart; // red hearts
    public Sprite emptyHeart; // black hearts

    // Start is called before the first frame update of the game
    private void Start()
    {
        // Attach components to our player character (animations & physics)
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called in every single frame
    private void Update()
    {
        // Vector 2 is the x, y coordinates. We detect what keys the user is pressing, 
        // e.g: i) arrowkeyup is pressed and so moveInput = Vector2(x, 1)        
        //     ii) arrowkeydown is pressed and so moveInput = Vector2(x, -1) and the same with x.
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // make sure player does not move faster than expected
        moveAmount = moveInput.normalized * speed;

        // check wheter the player is moving
        if (moveInput != Vector2.zero) {
            anim.SetBool("isRunning", true); // transition to run state, so play the run animations
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }

    // FixedUpdate is called for every physics frame
    private void FixedUpdate() 
    {
        // make the player's movement frame-rate independent (in every device, every object will run the same speed)
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime); 
    }

    // when player's get hit
    public void TakeDamage(int amount) 
    {
        health -= amount;
        UpdateHealthUI(health);

        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeWeapon(Weapon weaponToEquip) 
    {
        // destroy the game object with the corresponding tag
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));

        // spawn (which weapon, which position to spawn, which rotation, spawn to player when equipped)
        Debug.Log("weapon to equip: " + weaponToEquip + " transform: " + transform + " transform.position: " + transform.position);
        Instantiate(weaponToEquip, transform.position, transform.rotation, transform);
    }

    void UpdateHealthUI(int currentHealth) 
    { 
        for (int i = 0; i < hearts.Length; i++) 
        {
            if (i < currentHealth) 
            {
                hearts[i].sprite = fullHeart;
            } else 
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount) {
        if (health + healAmount > 5) {
            health = 5;
        } else {
        health += healAmount;
        }
        UpdateHealthUI(health);
    }
}
