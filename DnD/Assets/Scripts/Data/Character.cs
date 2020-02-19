using System.Collections.Generic;
using UnityEngine;

namespace Dnd.Gameplay
{
    public abstract class Character : ICharacter
    {
        public string characterName { get; set; }
        public string icon { get; private set; }
        public int[] modificators { get; set; }
        public int maxWeight { get; set; }
        public Skills skills { get; private set; }
        public ArmorClass AC { get; set; }
        public IAttackState attackState { get; set; }
        public List<Equipment> equipment { get; set; }
        public List<Spell> spells { get; set; }
        public List<Trait> traits { get; set; }

        protected int _currentWeight;

        public Character(string _name, string _icon, int[] _modificators, int _maxWeight, Skills _skills, ArmorClass _ac, IAttackState _as, List<Equipment> _eq, List<Spell> _spells, List<Trait> _traits)
        {
            this.characterName = _name;
            this.icon = _icon;
            this.modificators = _modificators;
            this.maxWeight = _maxWeight;
            this.skills = _skills;
            this.AC = _ac;
            this.attackState = _as;
            this.equipment = _eq;
            this.spells = _spells;
            this.traits = _traits;
        }

        public virtual bool RollCheckAC(int _rollValue)
        {
            return (AC.sumAC >= _rollValue);
        }

        public virtual bool RollCheckSkill(int _index, int _rollValue)
        {
            int roll = Random.Range(1, 21);
            return (skills.allSkills[_index].summary + roll >= _rollValue);
        }

        public virtual string RollCheckAttack(ICharacter _opponent, int _distance)
        {
            return attackState.AttackState(modificators, _opponent, _distance);
        }
    }
}