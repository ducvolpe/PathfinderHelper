using UnityEngine;

namespace Dnd.Gameplay
{
    public class MeleeWeapon : Equipment, IWeapon
    {
        public int hands;
        public int damageDice;
        public int size;

        public int Attack(int _bab, int _rollValue, int[] _mods)
        {
            return _bab + _mods[0] + _rollValue;
        }

        public int Hit(int _bonus, int _distance)
        {
            int damage = Random.Range(1, damageDice + 1);
            return (size >= _distance / 5) ? damage * size * hands + _bonus : 0;
        }
    }
}