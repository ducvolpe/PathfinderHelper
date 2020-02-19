using System.Collections.Generic;
using UnityEngine;

namespace Dnd.Gameplay
{
    public class Attack : IAttackState
    {
        public List<IWeapon> characterWeapon = new List<IWeapon>();
        public IWeapon selectedWeapon;
        public int bab { get; set; }
        public int attackBonus { get; set; }
        public int hitBonus { get; set; }

        public Attack(int _bab, int _ab, int _hb)
        {
            this.bab = _bab;
            this.attackBonus = _ab;
            this.hitBonus = _hb;
        }

        public void AddWeapon(IWeapon _weapon)
        {
            characterWeapon.Add(_weapon);
            if (selectedWeapon == null)
                selectedWeapon = _weapon;
        }

        public void RemoveWeapon(IWeapon _weapon)
        {
            if (characterWeapon.Contains(_weapon))
            {
                characterWeapon.Remove(_weapon);
                if (selectedWeapon == _weapon)
                    selectedWeapon = (characterWeapon.Count > 0) ? characterWeapon[0] : null;
            }
        }

        public void ChangeWeapon(int _index)
        {
            if (_index < characterWeapon.Count)
                selectedWeapon = characterWeapon[_index];
        }

        public string AttackState(int[] _mods, ICharacter _opponent, int _distance)
        {
            int roll = Random.Range(1, 21);
            if (selectedWeapon != null)
            {
                int attack = selectedWeapon.Attack(bab, roll, _mods);
                bool check = _opponent.RollCheckAC(attack);
                if (check)
                {
                    int damage = selectedWeapon.Hit(hitBonus, _distance);
                    return string.Format("Успешная атака. Бросок {0}. Урон: {1}", attack, damage);
                }
                else
                    return string.Format("Промах. Бросок {0}", attack);
            }
            else
                return "Персонаж не может атаковать";
        }
    }
}