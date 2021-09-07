using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
    public static float enemyHealth = 100f;
    public Collider[] enemyCol;
    
    public void DeductHealth(float deductHealth) 
    {
        enemyHealth -= deductHealth;

        if (enemyHealth <= 0) { EnemyDead(); }
    }

    void EnemyDead()
    {
        enemyHealth = 0f;
        foreach (var col in enemyCol)
        {
            col.enabled = false;
        }
        enemyHealth = 100f;
        UIManager.instance.killCount++;
        UIManager.instance.UpdateKillCounterUI();
        Destroy(gameObject);
        GunFire.instance2.SpawnCapsule();
    }
}
