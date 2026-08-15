using UnityEngine;

public interface Damageable
{
    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            OnDestroy();
    }
    void OnDestroy();

    int health {get; set;}
}
