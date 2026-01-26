using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 10;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject destroyEffect;

    [SerializeField] float health;
    public float CurrentHealth { get; set; }

    public float CurrentHealthPercentage
    {
        get { return CurrentHealth / maxHealth; }
    }

    bool destroyed = false;

    void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        Debug.Log($"Hit {gameObject}");
        if(destroyed) return;
        CurrentHealth -= damage;
        health = CurrentHealth;
        if (CurrentHealth <= 0)
        {
            destroyed = true;
        }
        
        if(!destroyed && hitEffect != null)
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
        }

        if (destroyed)
        {
            Debug.Log($"Hit {gameObject}");
            if(destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
                //Destroy(destroyEffect, 2.5f);
            }
            Destroy(gameObject);
        }

        if(CurrentHealth > maxHealth) CurrentHealth = maxHealth;
    }
    
    public void OnHeal(float amount)
    {
        CurrentHealth += amount;
    }
}
