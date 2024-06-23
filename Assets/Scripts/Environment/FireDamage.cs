using Freelf.Character.Interfaces;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    public int damage = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<IDamageable>(out var damageable)) return;
        Debug.Log($"Damage applied to {other.name}");
        damageable.TakeDamage(-damage);
    }
}
