using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    public bool isRagdoll;
    public List<Rigidbody> Rigidbodies = new List<Rigidbody>();

    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private List<Collider> colliders = new List<Collider>();
    private AudioSource audioSource;

    private void Awake()
    {
        TryGetComponent(out animator);
        if (animator == null) return;
        TryGetComponent(out capsuleCollider);
        TryGetComponent(out audioSource);

        GetComponentsInChildren(Rigidbodies);
        GetComponentsInChildren(colliders);

        for (int i = 0; i < Rigidbodies.Count; i++)
        {
            Rigidbodies[i].isKinematic = true;
            colliders[i].isTrigger = true;
        }

        capsuleCollider.isTrigger = false;
    }

    public void EnableRagdoll()
    {
        audioSource.Play();

        if (isRagdoll) return;

        isRagdoll = !isRagdoll;

        for (int i = 0; i < Rigidbodies.Count; i++)
        {
            Rigidbodies[i].isKinematic = false;
            Rigidbodies[i].linearVelocity = Vector3.zero;
            colliders[i].isTrigger = false;
        }

        capsuleCollider.isTrigger = true;
        animator.enabled = false;
    }
}
