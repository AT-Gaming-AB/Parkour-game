using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunFire : MonoBehaviour
{
    RaycastHit hit;
    private bool Raycast;
    [SerializeField] Transform shootPoint;
    public ParticleSystem muzzleflash;
    float nextFire = 0;
    public AudioSource gunShot;
    public AudioSource gunEmpty;
    public AudioSource Reload;
    private Animator animator;
    public GameObject BulletHolePrefab;
    public TextMeshProUGUI ammoText;
    public float ammoCount = 12;
    private float currentAmmo;
    public float firerate = 0.5f;
    public float damageEnemy = 40f;
    [HideInInspector] public bool gamePaused;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        currentAmmo = ammoCount;
        ammoText.text = "ammo: " + ammoCount; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && !gamePaused)
        {
            Shoot();
        }
        if (Input.GetKey(KeyCode.R) & currentAmmo != ammoCount && !gamePaused)
        {
            currentAmmo = ammoCount;
            nextFire = Time.time + 1f;
            ammoText.text = "ammo: " + currentAmmo; 
            animator.Play("Base Layer.Reload");
            Reload.Play();
            Invoke("StopAudio", 0.5f);
        }
    }

    public void Shoot()
    {
        if (Time.time > nextFire & currentAmmo > 0)
        {
            currentAmmo--;
            ammoText.text = "ammo: " + currentAmmo; 
            nextFire = Time.time + firerate;
            muzzleflash.Play();
            gunShot.Play();
            animator.Play("Base Layer.Recoil");
            // Shoot RayCast
            Raycast = Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, 50f);
            if(Raycast)
            {
                GameObject newHole = Instantiate(BulletHolePrefab, hit.point + hit.normal * 0.001f, Quaternion.identity) as GameObject;
                newHole.transform.LookAt(hit.point + hit.normal);
                Destroy(newHole, 5f);
                if(hit.transform.tag == "Enemy")
                {
                    EnemyHit();
                }
                else if (hit.transform.tag == "Target") 
                {
                    GameObject HitGameObject = hit.transform.gameObject;
                    TargetHit(HitGameObject);
                }
                else
                {
                    Debug.Log("Hit other");
                }
            }
        }
        else if (Time.time > nextFire & currentAmmo == 0)
        {
            nextFire = Time.time + firerate;
            gunEmpty.Play();
        }
    }
    private void StopAudio()
    {
        Reload.Stop();
    }

    private void EnemyHit()
    {
        Debug.Log("Enemy Hit");
        EnemyHealth enemyHealthScript = hit.transform.GetComponent<EnemyHealth>();
        enemyHealthScript.DeductHealth(damageEnemy);
    }
    private void TargetHit(GameObject hit)
    {
        Debug.Log("Target Hit");
        Destroy(hit.transform.parent.gameObject);
    }
}
