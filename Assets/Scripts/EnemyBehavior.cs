using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public float maxHealth;
    public float currentHealth;
    public int xpAmount;    

    public FloatingHealthBar healthBar;
    public GameObject target;    

    void Update()
    {                
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
         if(collision.gameObject.tag == "Tower")
        {
            DealDamage();
        }
    }

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            CommitDeath();
        }
    }

    void DealDamage()
    {
        CharacterSkillTree.Tree.currentHealth -= currentHealth;
        Destroy(gameObject);
    }

    void CommitDeath()
    {
        EventManager.instance.AddExperience(xpAmount);
        Destroy(gameObject);
    }
}
