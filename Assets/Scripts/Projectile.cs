using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    public GameObject explosion;

    public int damage;

    // Start is called before the first frame update
    private void Start()
    {
        // Destroy(gameObject, lifeTime);
        Invoke("DestroyProjectile", lifeTime);        
    }


    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile() 
    {
        Destroy(gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.tag == "Enemy") 
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
