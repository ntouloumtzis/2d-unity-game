using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //poso grigora trexei o paiktis sto xwro

    private Rigidbody2D rb; //ola ta physics p xreiazontai apo th Unity
    private Animator anim;

    private Vector2 moveAmount; //metavliti p tha kanei calculate poso xreiazetai na proxwrisei o paiktis

    public float health;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); //apla dilwnete se metavliti to component p einai attached sto xaraktira
    }

    // Update is called once per frame
    private void Update()
    { 
        //Apothikeuei ta inputs tou paikti
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")); //gia ti kinisi tou xaraktira (.GetAxisRaw gia non responsiveness)
        moveAmount = moveInput.normalized * speed; //to .normalized gia na vevaiwsoume oti dn kineitai grigorotera amesws

        if (moveInput != Vector2.zero) {
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }

    // FixedUpdate is called for every physics frame
    private void FixedUpdate() 
    {
        //xrisimopoioume ti sunartisi MovePosition, gia na einai o xaraktiras frame-rate independent. Se kathe suskeui tha trexei me thn idio speed. Ta objects dld tha kinountai me tin idia taxuthta.
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime); 
    }

    public void TakeDamage(int amount) {
        health -= amount;

        if (health <= 0) 
        {
            Destroy(this.gameObject);
        }
    }
}
