using System.Collections.Generic;
using UnityEngine;

namespace Dnd.SO
{
    public class Weapon : ScriptableObject
    {
        #region public vars
        public enum WeaponType
        {
            piercing,
            cutting,
            crushing
        }

        public float cost;
        public float grip;
        [Range(1, 20)]
        public int minCritical;
        [Range(2, 3)]
        public int criticalMultiplier;
        public List<WeaponType> weaponType;
        public Vector2 distance;
        public int ammunition;
        #endregion

        #region public functions
        public int WeaponDamage(int _attackProbability, float _currentGrip, int _distance, int _addedAttack = 0)
        {
            int multiplier = (_attackProbability >= minCritical) ? criticalMultiplier : 1;
            float gripCoefficient = Mathf.Clamp(_currentGrip, 1f, grip);

            int currentDamage = Mathf.FloorToInt(multiplier * gripCoefficient + _addedAttack);
            //calculate distance coefficient
            return currentDamage;
        }
        #endregion
    }
}