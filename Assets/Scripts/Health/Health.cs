using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    private void Awake() {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    
    public void TakeDamage(float _damage) {

        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0) {
            // hurt
            anim.SetTrigger("hurt");
        }
        else {
            // dead
            if (!dead) {
                anim.SetTrigger("die");

                if(GetComponent<playermoving>() != null) {
                    GetComponent<playermoving>().enabled = false;
                }

                if(GetComponentInParent<EnemyPatrol>() != null) {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }

                if(GetComponent<Boss>() != null) {
                    GetComponent<Boss>().enabled = false;
                }

                
                dead = true;
            }
        }
    }

    public void Respawn()
    {
        dead = false;
        currentHealth = startingHealth;
        anim.ResetTrigger("die");
        anim.Play("Player");

        GetComponent<playermoving>().enabled = true;
    }
    
}

