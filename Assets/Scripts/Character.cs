using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int currentExperience, levelMaxExperience, experiencePerLevelAmount, currentLevel;

    private void OnEnable()
    {
        EventManager.instance.OnExperienceChange += HandleExperienceChange;
        experiencePerLevelAmount = levelMaxExperience;
    }

    private void OnDisable()
    {
        EventManager.instance.OnExperienceChange -= HandleExperienceChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            LevelUp();
        }
    }

    private void HandleExperienceChange(int newExpeirence)
    {
        currentExperience += newExpeirence;

        if(currentExperience >= levelMaxExperience)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {        
        currentLevel++;
        currentExperience -= levelMaxExperience;
        levelMaxExperience = experiencePerLevelAmount * currentLevel;
        CharacterSkillTree.Tree.currentHealth = CharacterSkillTree.Tree.maxHealth;

        //increase max mana on level up
        CharacterSkillTree.Tree.IncreaseMaxMana();
        CharacterSkillTree.Tree.currentMana = CharacterSkillTree.Tree.maxMana;
        //increase mana regeneration amount on level up
        CharacterSkillTree.Tree.IncreaseManaRegenAmount();
        //increase mana regeneration rate on level up
        CharacterSkillTree.Tree.IncreaseManaRegenSpeed();
        //increase radius of explosion effect
        CharacterSkillTree.Tree.IncreaseExplosionRadius();
        //increase damage
        CharacterSkillTree.Tree.IncreaseDamage();
    }    
}
