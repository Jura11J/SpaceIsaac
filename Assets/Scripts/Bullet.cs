using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 50;          // predkosc pocisku
    public Vector3 targetVector;    // kierunek lotu
    public float lifetime = 10f;     // do kiedy ma leciec zeby nie istnial caly czas
    public float damage = 10;       // ile zada obrazen

    void Start()
    {

        Rigidbody2D rb = gameObject.GetComponentInChildren<Rigidbody2D>();

        rb.AddForce(targetVector.normalized * speed);
        AudioManager.instance.PlaySound("laser");
    }



    void Update()
    {
        // odliczanie zycia
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            // usmierc pocis
            Destroy(gameObject);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            IEntityHealth itd = collision.GetComponent<IEntityHealth>();
            if (itd != null)
            {
                itd.ITakeDamage(damage);
                lifetime = 0f; // gdy ktos oberwie
            }

        }
    }
}
