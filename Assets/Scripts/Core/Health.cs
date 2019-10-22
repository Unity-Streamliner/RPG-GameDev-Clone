using UnityEngine;

namespace RPG.Core 
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float health = 100f;

        private bool _isDead = false;
        
        public bool IsDead
        {
            get { return _isDead; }
        }

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
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}