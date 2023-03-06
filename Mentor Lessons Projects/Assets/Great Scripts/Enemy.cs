using System;
using System.Collections;
using System.Collections.Generic;
using Mentor;
using UnityEngine;

public class Enemy : GameEntity
{
    public CharacterStats stats;
    [SerializeField] protected EnemyData enemyData;
    [SerializeField] Renderer hairRenderer;
    [SerializeField] float speed;


    protected virtual void Awake()
    {
        if (enemyData != null)
        {
            stats.HP = enemyData.startingHP;
            stats.stamina = enemyData.statringStamina;
            if (hairRenderer != null)
                hairRenderer.material.color = enemyData.GetRandomHairColor();
        }

    }

    public virtual int Damage(int incomingDamage)
    {
        return incomingDamage;
    }

    protected override void Die()
    {
        if (destroyOnDie)
            Destroy(gameObject);

        Debug.Log("Enmemy Die");
    }
}
