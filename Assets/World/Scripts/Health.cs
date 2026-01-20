using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 10;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject destroyEffect;

    public float CurrentHealth { get; set; }

    public float CurrentHealthPercentage
    {
        get { return CurrentHealth / maxHealth; }
    }

    float health = 0;
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
    }
    
    public void OnHeal(float amount)
    {
        CurrentHealth += amount;
    }
}
