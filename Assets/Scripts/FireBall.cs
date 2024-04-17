using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{    
    public GameObject explosionPrefab;    
    public LayerMask enemyLayers;
    
    public int manaCost;    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {                
        if (CharacterSkillTree.Tree.explosionLevel == 0 && collision.gameObject.layer == 3)
        {                           
            DealDamage(collision.gameObject.GetComponent<BoxCollider2D>());
        }
        else if (CharacterSkillTree.Tree.explosionLevel >= 1)
        {
            SpawnExplosion();            
        }
        
        Destroy(gameObject);
    }   

    private void DealDamage(Collider2D hitEnemy)
    {
        hitEnemy.GetComponent<EnemyBehavior>().TakeDamage(CharacterSkillTree.Tree.totalDamage);
    }

    private void DealExplosionDamage(Collider2D[] hitEnemies)
    {
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyBehavior>().TakeDamage(CharacterSkillTree.Tree.totalDamage);
        }
    }

    private void SpawnExplosion()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, CharacterSkillTree.Tree.totalExplosionRadius, enemyLayers);
        
        if(hitEnemies != null)
        {
            DealExplosionDamage(hitEnemies);
        }
    }    
}
