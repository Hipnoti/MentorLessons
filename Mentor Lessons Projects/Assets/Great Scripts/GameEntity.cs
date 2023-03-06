using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{


    [SerializeField] protected bool destroyOnDie = true;

    protected abstract void Die();
}
