using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public bool Damage(GameObject damager, int damage);
}
