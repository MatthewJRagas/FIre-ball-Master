using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{     

    public Transform fireBallSpawnPoint;
    public GameObject fireBallPrefab;             
    private bool isCasting = false;

    public float baseSpellStrength;
    public float newBaseSpellStrength;
        

    void Update()
    {
        
        //while space key is held down, cast speed is multiplied with time to increase fireBall Speed;
        if(Input.GetKey(KeyCode.Space) && CharacterSkillTree.Tree.currentMana >= fireBallPrefab.GetComponent<FireBall>().manaCost)
        {            
            isCasting = true;
            baseSpellStrength += (Time.deltaTime * CharacterSkillTree.Tree.castSpeed);
        }

        if(Input.GetKeyUp(KeyCode.Space) && isCasting == true)
        {            
            CastFireball();
            isCasting = false;
            baseSpellStrength = 1.0f;
        }
    }    

    void CastFireball()
    {
        var fireBall = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, fireBallSpawnPoint.rotation);
        fireBall.GetComponent<Rigidbody2D>().velocity = fireBallSpawnPoint.right * baseSpellStrength;

        CharacterSkillTree.Tree.currentMana -= fireBallPrefab.GetComponent<FireBall>().manaCost;
    }
}
