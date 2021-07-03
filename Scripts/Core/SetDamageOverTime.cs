using UnityEngine;

public class SetDamageOverTime : MonoBehaviour
{
    [SerializeField] int ticks;
    [SerializeField] string type, playerOrEnemy;
    [SerializeField] float damage, inBetweenTime;
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DamageOverTime>().GetCharacterType()!=playerOrEnemy)
        {
            other.GetComponent<DamageOverTime>().ApplyDamageOverTime(ticks, damage, inBetweenTime, type);
        }
    }
}
