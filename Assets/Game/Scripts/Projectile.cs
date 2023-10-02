using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Define a velocidade do projétil")]
    [Tooltip("Cuidado com essa velocidade pq quanto maior, menos a precisão das validações de colisão")]
    [SerializeField] private float speed;
    
    private void OnEnable()
    {
        MoveProjectile();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Bateu");
        this.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    private void Update()
    {
        MoveProjectile();
    }
    
    private void MoveProjectile()
    {
        transform.position += transform.forward * (Time.deltaTime * speed);
    }
}
