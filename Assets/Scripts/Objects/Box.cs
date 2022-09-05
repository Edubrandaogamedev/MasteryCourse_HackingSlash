using System;
using UnityEngine;

public class Box : MonoBehaviour, ITakeHit
{
    [SerializeField] private float forceAmount = 10f;
    
    private Rigidbody _rb;
    public event Action OnHit = delegate {  };

    public bool Alive => true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void TakeHit(IDamage hitBy)
    {
        var direction = Vector3.Normalize(transform.position - hitBy.transform.position);
        _rb.AddForce(direction * forceAmount, ForceMode.Impulse);
        OnHit();

    }

}
