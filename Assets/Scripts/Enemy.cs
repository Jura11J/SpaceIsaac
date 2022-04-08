using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntityHealth
{
    public float speed = 2f;    // Follow speed

    [HideInInspector]
    public bool hasTarget = false;  
    [HideInInspector]
    public GameObject target;   

    public float hitpoints = 100f;
    private bool isDead = false; 

    private Rigidbody2D rb;

    public ScObWeapon currentWeapon;

    private float lastFired = 0f;       // ostatni strzal
    private float reloadTimer = 0f;     // czas do konca reload
    private int magazine;               // ilosc pociskow
    private bool reloading = true;      

 

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (hasTarget) {
            //dystans player-enemy
            float distance = Vector3.Distance(transform.position, target.transform.position);
            
            if (distance > 2) {
                // sledzenie
                follow(target.transform);
            }
            shoot();
        }

        if (isDead) {
            // umieraj gnido
            Destroy(gameObject);
        }
    }

    // strzelanie
    private void shoot() {
        lastFired -= Time.deltaTime;
        if (reloading) {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0) {
                reloading = false;
                magazine = currentWeapon.magazineCapacity;
            }
        } else {
            if (lastFired <= 0f && !reloading) {

                if (magazine == 1) {
                    startReload();
                }

                Vector3 direction = (target.transform.position - transform.position).normalized;

                GameObject bulletObject = Instantiate(currentWeapon.bulletPrefab, (transform.position + (direction * 0.6f)), Quaternion.identity);
                bulletObject.layer = 9;
                Bullet bullet = bulletObject.GetComponent<Bullet>();
                bullet.targetVector = direction.normalized;
                bulletObject.transform.position = transform.position;
                lastFired = currentWeapon.fireRate;

                magazine -= 1;
            }

        }
    }
    //przeladowanie
    private void startReload() {
        reloading = true;
        reloadTimer = currentWeapon.reloadSpeed;
    }

    // przy kolizji
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name.Equals("PlayerObject")) {    
            target = collision.gameObject;      // wziecie gracza na cel
            hasTarget = true;   
        }
    }

    //po kolizji bez kolizji
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name.Equals("PlayerObject")) {
            target = null;
            hasTarget = false;
        }
    }

    private void follow(Transform target) {
        // sledzenie
        rb.AddForce((target.transform.position - transform.position).normalized * speed);
    }

    public void IGainHealth(float health) {
        // podniusl powerup TODO
    }
    
    public void ITakeDamage(float damage) {//dostal pociskiem
        hitpoints -= damage;
        if (hitpoints <= 0) {
            isDead = true;
        }
    }
}
