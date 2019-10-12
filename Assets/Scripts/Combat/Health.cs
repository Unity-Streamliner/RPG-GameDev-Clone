using UnityEngine;

namespace RPG.Combat 
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;

        private bool _isDead = false;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage , 0);
            Debug.Log("health = " + health); 
            if (health == 0) 
            {
                Die();
            }
        }

        private void Die() 
        {
            if (_isDead) return;
            _isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}