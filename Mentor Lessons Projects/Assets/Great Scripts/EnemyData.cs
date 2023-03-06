using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mentor/Enemy Data", fileName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
   public int startingHP;
   public int statringStamina;
   public int baseDamage;
   
   public string scaleTextID = string.Empty;
   
   public Color[] hairColors;

   private TextAsset mainGameBalanceText;
   private float enemyScale;

   public Color GetRandomHairColor()
   {
      return hairColors[Random.Range(0, hairColors.Length)];
   }

   public float GetScale()
   {
      if (mainGameBalanceText == null)
      {
         
        
      }

      return 1f;
   }
}
