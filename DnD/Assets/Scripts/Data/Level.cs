using System.Collections.Generic;
using System.Linq;

namespace Dnd.Gameplay
{
    public class Level
    {
        public delegate void OnLevelUp(string _message);
        public event OnLevelUp onLevelUp;
        public Dictionary<CharacterClass, int> currentClasses;
        public List<ClassSkill> classSkills;
        public int[] spellsPerDay;

        public int generalLevel
        {
            get { return CheckLevel(); }
        }
        private int _experience;

        public Level(int _exp, List<CharacterClass> _classes, List<ClassSkill> _skills)
        {
            this.classSkills = _skills;
            currentClasses = new Dictionary<CharacterClass, int>();
            for (int i = 0; i < _classes.Count; i++)
            {
                if (currentClasses.ContainsKey(_classes[i]))
                    currentClasses[_classes[i]]++;
                else
                    currentClasses.Add(_classes[i], 1);
            }
            AddExp(_exp);
        }

        public void AddExp(int _value)
        {
            _experience += _value;
            int sum = currentClasses.Values.Sum();
            if (generalLevel > sum)
                onLevelUp?.Invoke("Новый уровень!");
        }

        private int CheckLevel()
        {
            int lvl = 1;
            if (_experience >= 2000 && _experience < 5000)
                lvl = 2;
            else if (_experience >= 5000 && _experience < 9000)
                lvl = 3;
            else if (_experience >= 9000 && _experience < 15000)
                lvl = 4;
            else if (_experience >= 15000 && _experience < 23000)
                lvl = 5;
            else if (_experience >= 23000 && _experience < 35000)
                lvl = 6;

            return lvl;
        }
    }
}