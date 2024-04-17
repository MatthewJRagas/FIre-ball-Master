using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{    
    public float life;
    void OnEnable()
    {
        gameObject.transform.localScale = new Vector3(CharacterSkillTree.Tree.totalExplosionRadius, CharacterSkillTree.Tree.totalExplosionRadius);
        Destroy(gameObject, life);        
    }    
}
