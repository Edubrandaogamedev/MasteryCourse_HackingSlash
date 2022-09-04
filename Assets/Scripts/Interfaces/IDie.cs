using System;
using UnityEngine;

public interface IDie
{
    public event Action<int,int> OnHealthChanged;
    public event Action<IDie> OnDied;
    public GameObject gameObject { get; }
} 