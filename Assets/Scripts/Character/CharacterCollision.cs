using System.Collections;
using System.Collections.Generic;
using Freelf.Character.DataTransfer;
using Freelf.Character.Interfaces;
using UnityEngine;


namespace Freelf.Character 
{
    public class CharacterCollision : CharacterComponent, IDamageable, IAttached<DamageData>
    {
        private DamageData _damageData;
        public void Attached(ref DamageData value)
        {
            _damageData = value;
        }

        public override void Init()
        {
            // pass
        }

        public void TakeDamage(int damage)
        {
            _damageData.Damage(damage);
        }
    }
}
