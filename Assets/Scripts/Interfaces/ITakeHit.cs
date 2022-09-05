using System;
using UnityEngine;

public interface ITakeHit
{
    Transform transform { get; }
    bool Alive { get; }
    void TakeHit(IDamage hitBy);
    event Action OnHit;
}