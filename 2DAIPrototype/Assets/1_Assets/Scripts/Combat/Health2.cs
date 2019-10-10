using UnityEngine;

namespace Platformer.Scripts.Combat
{
    public class Health2 : MonoBehaviour
    {
        public float HP;

        public void TakeDamage(float damage)
        {
            HP = Mathf.Max(HP - damage, 0);
        }
    }
}
