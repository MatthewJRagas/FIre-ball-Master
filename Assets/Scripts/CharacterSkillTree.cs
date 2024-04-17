using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillTree : MonoBehaviour
{
    public static CharacterSkillTree Tree;

    public float castSpeed;
    //mana pool
    public float maxManaPoints;
    public float baseMana;
    public float bonusMana;
    public float maxMana;
    public float currentMana;

    //mana regen
    public float manaRegenPoints;//1.25 bonus mana regen per point
    public float bonusManaRegen;//base:0 max:12.5
    public float baseManaRegen;//2.5/5 seconds
    public float totalManaRegen;//bonus mana regen + base mana regen
        
    //regen timer
    public float regenTimerPoints;//baseRegenTimer - 0.5 seconds per point
    public float timeOffRegenTimer;// = 0.5f * regenTimerPoints
    public float baseRegenTimer;//base 5 seconds, limit is 0.5 seconds
    public float currentRegenTimer;

    //health
    public float healthPoints;
    public float maxHealth;
    public float currentHealth;    

    //Explosion
    public int explosionLevel;
    public float baseExplosionRadius;
    public float bonusExplosionRadius;
    public float totalExplosionRadius;

    //damage
    public float damagePoints;
    public float baseDamage;//10
    public float bonusDamage;//damage points * 10
    public float totalDamage;//base damage + bonus damage

    void Start()
    {        
        //max mana stats
        baseMana = 100;
        bonusMana = maxManaPoints * 10;
        maxMana = baseMana + bonusMana;
        currentMana = maxMana;        

        //mana regen stats
        baseManaRegen = 2.5f;
        manaRegenPoints = 0;
        bonusManaRegen = 1.25f * manaRegenPoints;
        totalManaRegen = baseManaRegen + bonusManaRegen;
        //mana regen timer
        baseRegenTimer = 5.0f;
        timeOffRegenTimer = 0.2f;
        currentRegenTimer = baseRegenTimer;



        //health stats
        maxHealth = 100 + (healthPoints * 10);
        currentHealth = maxHealth;

        //explosion effect stats
        explosionLevel = 0;
        baseExplosionRadius = 1;
        bonusExplosionRadius = explosionLevel * 0.25F;
        totalExplosionRadius = baseExplosionRadius + bonusExplosionRadius;

        //damage stats
        damagePoints = 0;
        baseDamage = 10;
        bonusDamage = damagePoints * 5;
        totalDamage = baseDamage + bonusDamage;
    }

    //singleton check
    public void OnEnable()
    {
        if(Tree != null && Tree != this)
        {
            Destroy(this);
        }
        else
        {
            Tree = this;
        }
    }

    void Update()
    {
        currentRegenTimer -= Time.deltaTime;
        if (currentRegenTimer <= 0 && currentMana < maxMana)
        {
            RegenMana();
        }
        
    }

    //increase the max amount of mana
    public void IncreaseMaxMana()
    {        
        if(maxManaPoints < 10)
        {
            maxManaPoints++;            
        }
        bonusMana = maxManaPoints * 10;
        maxMana = baseMana + bonusMana;
    }

    //increases the amount of mana regenerated
    //per tick of mana regeneration
    public void IncreaseManaRegenAmount()
    {
        if(manaRegenPoints < 10)
        {
            manaRegenPoints++;
        }

        bonusManaRegen = 1.25f * manaRegenPoints;
        totalManaRegen = bonusManaRegen + baseManaRegen;
    }

    //decreases the time between mana regeneration ticks
    public void IncreaseManaRegenSpeed()
    {
        if(regenTimerPoints < 10 && baseRegenTimer > 0.5f)
        {
            regenTimerPoints++;

            baseRegenTimer -= (timeOffRegenTimer * regenTimerPoints);
        }        
    }

    //regenerate mana
    public void RegenMana()
    {                
            currentMana += (baseManaRegen + bonusManaRegen);
            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
            currentRegenTimer = baseRegenTimer;        
    }

    //increase the size of the explosion
    public void IncreaseExplosionRadius()
    {
        explosionLevel++;
        if (totalExplosionRadius < 5f && explosionLevel > 1)
        {
            bonusExplosionRadius = explosionLevel * 0.25F;
            totalExplosionRadius = baseExplosionRadius + bonusExplosionRadius;
        }
        else if(explosionLevel == 30)
        {
            //F.O.A.B.
            //the Fire Of All Balls
            totalExplosionRadius = 20;
        }
    }

    //increase damage of fire ball
    public void IncreaseDamage()
    {
        damagePoints++;
        if (damagePoints < 10)
        {
            bonusDamage = damagePoints * 10;
            totalDamage = baseDamage + bonusDamage;
        }
        else if(damagePoints == 30)
        {
            totalDamage = 10000;
        }
        
        
    }
}
