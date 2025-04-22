using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float lifeTime = 2.5f;

    private float currentTimer;

    private void Start()
    {
        currentTimer = lifeTime;
    }

    private void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
