using System.Collections.Generic;

namespace Dnd.Gameplay
{
    public class Skills
    {
        public List<Skill> allSkills = new List<Skill>();
        private string[] skillNames = new string[]
        {
            "Акробатика",
            "Блеф",
            "Верховая езда",
            "Внимание",
            "Дипломатия",
            "Зн. География",
            "Зн. История",
            "Зн. Краеведение",
            "Зн. Подземелья",
            "Зн. Природа",
            "Зн. Религия",
            "Зн. Магия",
            "Колдовство",
            "Лазание",
            "Лечениие",
            "Механика",
            "Плавание",
            "Проницательность",
            "Скрытность"
        };
        public Skills(int[] _modificators, List<int> _classSkillId)
        {
            int[] indexes = new int[19];
            indexes[0] = 1;
            indexes[1] = 5;
            indexes[2] = 1;
            indexes[3] = 4;
            indexes[4] = 5;
            indexes[5] = 3;
            indexes[6] = 3;
            indexes[7] = 3;
            indexes[8] = 3;
            indexes[9] = 3;
            indexes[10] = 3;
            indexes[11] = 3;
            indexes[12] = 3;
            indexes[13] = 0;
            indexes[14] = 4;
            indexes[15] = 1;
            indexes[16] = 0;
            indexes[17] = 4;
            indexes[18] = 1;

            for (int i = 0; i < skillNames.Length; i++)
            {
                Skill skill = new Skill(skillNames[i]);
                skill.isLearn = (i < 5 || i >= 13) && i == 15;
                skill.charModificator = _modificators[indexes[i]];
                if (_classSkillId != null && _classSkillId.Count > 0)
                    skill.isClass = _classSkillId.Contains(i);
                allSkills.Add(skill);
            }
        }

        public bool PlacePoint(int _index, int _amount, int _sumLevel)
        {
            return allSkills[_index].PlacePoint(_amount, _sumLevel);
        }

        public void AddExtraPoints(int _index)
        {
            allSkills[_index].otherPoints += _index;
        }
    }

    public class Skill
    {
        public string skillName { get; private set; }
        public bool isLearn { get; set; }
        public bool isClass { get; set; }
        public int points { get; private set; }
        public int charModificator { get; set; }
        public int otherPoints { get; set; }
        public int summary
        {
            get { return (points + charModificator + otherPoints); }
        }

        public Skill(string _name, int _other = 0)
        {
            skillName = _name;
            otherPoints = _other;
        }

        public bool PlacePoint(int _amount, int _level)
        {
            if (points + _amount <= _level)
            {
                points += _amount;
                if (isClass)
                    otherPoints += 3;
                if (!isLearn)
                    isLearn = true;
                return true;
            }
            else
                return false;
        }
    }
}