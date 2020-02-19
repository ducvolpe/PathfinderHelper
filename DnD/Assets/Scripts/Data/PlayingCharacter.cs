using System.Collections.Generic;

namespace Dnd.Gameplay
{
    public class PlayingCharacter : Character, IPlayer
    {
        public Race race { get; private set; }
        public Level playerLevel { get; private set; }

        public PlayingCharacter(string _name, string _icon, int[] _modificators, int _maxWeight, Skills _skills, ArmorClass _ac, IAttackState _as, List<Equipment> _eq, List<Spell> _spells, List<Trait> _traits, Race _race, Level _level) 
            : base(_name, _icon, _modificators, _maxWeight, _skills, _ac, _as, _eq, _spells, _traits)
        {
            this.playerLevel = _level;
        }

        public void GetExperience(int _value)
        {
            if (_value > 0)
                playerLevel.AddExp(_value);
        }

        public void GetStuff(Equipment _eq)
        {
            equipment.Add(_eq);
        }

        public void EquipStuff(int _weight, bool _value)
        {
            bool penalty = _currentWeight >= maxWeight;
            if (_value)
            {
                _currentWeight += _weight;
                if (_currentWeight >= maxWeight && !penalty)
                    race.baseSpeed -= 10;
            }
            else
            {
                _currentWeight -= _weight;
                if (penalty && _currentWeight < maxWeight)
                    race.baseSpeed += 10;
            }
        }
    }
}