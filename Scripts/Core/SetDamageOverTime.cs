using UnityEngine;

public class SetDamageOverTime : MonoBehaviour
{
    [SerializeField] int ticks;
    [SerializeField] string type, playerOrEnemy;
    [SerializeField] float damage, inBetweenTime;
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DamageOverTime>() &&
            other.GetComponent<DamageOverTime>().GetCharacterType()!=playerOrEnemy)
        {
            if (GetComponent<Projectiles>() && other.GetComponent<Fighter>().GetPlayerID() 
                == GetComponent<Projectiles>().GetInstantator())
            {
                return;
            }
            other.GetComponent<DamageOverTime>().ApplyDamageOverTime(ticks, damage, inBetweenTime, type);
        }
    }
}
