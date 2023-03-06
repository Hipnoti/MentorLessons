using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SerializedSaveGame 
{
    public CharacterStats characterStats;
    public List<string> completedQuests;
    public List<EnemiesInfo> enemiesInfos;


    
}
