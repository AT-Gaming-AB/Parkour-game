using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    PlayerMovement playerMovementScript;
    public static GunFire instance2;
    RaycastHit hit;

    //Used to damage enemy
    [SerializeField] float damageEnemy = 55f;

    [SerializeField] Transform shootPoint;

    [SerializeField] int currentAmmo;

    [SerializeField] float rateOffFire;
    float nextFire = 0;

    [SerializeField] float weaponRange;
    public GameObject EnemyPrefab;
    public GameObject Bullet;
    public Camera playerCamera;
    [HideInInspector] public bool Raycast;
    public Transform BulletShootHole;
    private Animator animator;
    // public ParticleSystem particleSystem;
    //bool withChildren = true;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        GameObject enemyObject = Instantiate (EnemyPrefab);
        enemyObject.transform.position = new Vector3(0,2,-30);
        if(instance2 == null)
        {
            instance2 = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && currentAmmo > 0)
        {
            Shoot();
        }
        if (Input.GetButtonDown("Fire3"))
        {
            SpawnCapsule();
        }
    }
    public void SpawnCapsule()
    {
        GameObject enemyObject = Instantiate (EnemyPrefab);
        enemyObject.transform.position = new Vector3(Random.Range(-5.0f, 5.0f),2,-30);
    }

    public void Shoot()
    {
        if(Time.time > nextFire)
        {
            animator.Play("Base Layer.Recoil");
            
            nextFire = Time.time + rateOffFire;

            currentAmmo--;

            // Shoot Bullet
            GameObject bulletObject = Instantiate(Bullet);
            bulletObject.transform.position = BulletShootHole.transform.position + transform.forward;
            bulletObject.transform.forward = playerCamera.transform.forward;

            // Shoot RayCast
            Raycast = Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, weaponRange);
            if(Raycast)
            {
                if(hit.transform.tag == "Enemy")
                {
                    Debug.Log("Enemy Hit");
                    EnemyHeath enemyHealthScript = hit.transform.GetComponent<EnemyHeath>();
                    enemyHealthScript.DeductHealth(damageEnemy);
                }
                else
                {
                    Debug.Log("Hit other");
                }
            }

            // particleSystem.Play(withChildren);
        }
    }
    // void shootBullet()
    // {
    //     if(Time.time > nextFire) {
    //         nextFire = Time.time + rateOffFire;
            
    //         GameObject bulletObject = Instantiate(Bullet);
    //         bulletObject.transform.position = BulletShootHole.transform.position + transform.forward;
    //         bulletObject.transform.forward = playerCamera.transform.forward;
    //     }
    // }
}
