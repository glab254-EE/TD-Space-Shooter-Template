using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal interface IDamagable
{
   [SerializeField]int MaxHealth {get;}
   int Health {get;}
}
