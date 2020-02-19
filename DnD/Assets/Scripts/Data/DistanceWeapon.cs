using UnityEngine;

namespace Dnd.Gameplay
{
    public class DistanceWeapon : Equipment, IWeapon
    {
        public int maxDistance { get; set; }
        public int damageDice { get; set; }
        public int ammoCount { get; set; }

        private int _minDistance = 30;

        public int Attack(int _bab, int _rollValue, int[] _mods)
        {
            if (ammoCount > 0)
            {
                ammoCount--;
                return _bab + _mods[1] + _rollValue;
            }
            else return 0;
        }

        public int Hit(int _bonus, int _distance)
        {
            int damage = Random.Range(1, damageDice + 1);
            float distanceCoeff = 1f;
            if (_distance < _minDistance)
                distanceCoeff *= 0.75f;
            if (_distance > maxDistance)
                distanceCoeff *= 0.5f;

            return Mathf.FloorToInt(damage * distanceCoeff + _bonus);
        }
    }
}