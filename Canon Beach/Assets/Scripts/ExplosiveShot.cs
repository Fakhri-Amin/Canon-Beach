using Unity.Mathematics;
using UnityEngine;

public class ExplosiveShot : Shot
{
    [SerializeField] private ParticleSystem explosionEffect;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float explosionForce = 3f;
    [SerializeField] private float upwardForce = 1f;

    void OnCollisionEnter(Collision collision)
    {
        var explosion = Instantiate(explosionEffect, transform.position, transform.rotation);

        var rigidBodyColliders = Physics.OverlapSphere(collision.contacts[0].point, explosionRadius);

        foreach (var item in rigidBodyColliders)
        {
            if (item.CompareTag("Target"))
            {
                item.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, collision.contacts[0].point, upwardForce, upwardForce, ForceMode.Impulse);
                TargetUI.Instance.HandleTargetHit();
            }
        }

        Destroy(gameObject);
    }
}
