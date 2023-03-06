using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : Enemy
{
    public int strawsLeft = 10;
    
    public void Damage(int incomingDamage, int damageOverTime)
    {
        
    }
    
    protected override void Awake()
    {
        enemyData.GetScale();
        base.Awake();
    }
}
