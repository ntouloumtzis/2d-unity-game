using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; // how fast the player can move (with public keyword, the variable can show up on Unity's Inspector)
    private Rigidbody2D rb; // contains all the physics inside Unity
    private Vector2 moveAmount; // calculate how much we want the player to move on
    private Animator anim; // contains all the animations inside Unity
    
    public int health; // player's health

    public Image[] hearts; // an array containing our hearts UI
    public Sprite fullHeart; // red hearts
    public Sprite emptyHeart; // black hearts

    public Animator hurtAnim;

    private SceneTransitions sceneTransitions;

    // Start is called before the first frame update of the game
    private void Start()
    {
        // Attach components to our player character (animations & physics)
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransitions = FindObjectOfType<SceneTransitions>();
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
        UpdateHealthUI(health); // call the corresponding function with argument our health variable
        hurtAnim.SetTrigger("hurt"); // pass the hurt trigger when player's being hit

        if (health <= 0) 
        {
            Destroy(this.gameObject);
            sceneTransitions.LoadScene("Loss");
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
        // a simple for loop for our hearts array
        for (int i = 0; i < hearts.Length; i++) 
        {
            // if 0 < the current health of the player e.g 2...
            if (i < currentHealth) 
            {
                // we are getting access to the red heart UI, so to increment the player's health
                hearts[i].sprite = fullHeart;
            } else 
            {
                // otherwise decrease player's health providing the black heart UI
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount) 
    {
        if (health + healAmount > 5) 
        {
            // do not excend the 5 hearts
            health = 5;
        } else {
            
            // otherwise provide with the heart pickup, when player is detected collision
            health += healAmount;
        }

        // call function to update the player's health UI
        UpdateHealthUI(health);
    }
}

