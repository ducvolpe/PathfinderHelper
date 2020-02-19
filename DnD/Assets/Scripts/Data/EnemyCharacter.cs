using System.Collections.Generic;

namespace Dnd.Gameplay
{
    public class EnemyCharacter : Character, IEnemy
    {
        public string bestiaryType { get; set; }
        public int relation { get; set; }
        public int difficulty { get; set; }

        public EnemyCharacter(string _name, string _icon, int[] _modificators, int _maxWeight, Skills _skills, ArmorClass _ac, IAttackState _as, List<Equipment> _eq, List<Spell> _spells, List<Trait> _traits, string _bt, int _relation, int _difficulty) 
            : base(_name, _icon, _modificators, _maxWeight, _skills, _ac, _as, _eq, _spells, _traits)
        {
            this.bestiaryType = _bt;
            this.relation = _relation;
            this.difficulty = _difficulty;
        }

        public void Dialogue(int _skillId, int _rollValue)
        {
            if (bestiaryType == "разумное")
            {
                int difference = RollCheckSkill(_skillId, _rollValue) ? 1 : -1;
                relation += difference;
            }
        }

        public override bool RollCheckAC(int _rollValue)
        {
            return (AC.sumAC + difficulty >= _rollValue);
        }
    }
}