using System.Collections.Generic;

namespace Dnd.Gameplay
{
    public class CharacterClass
    {
        public enum Class_Type
        {
            Bard,
            Warrior,
            Cleric,
            Wizard,
            Rogue,
            Ranger,
            Barbarian,
            Monk,
            Sorcerer,
            Druid,
            Paladin
        }

        public Class_Type classType { get; private set; }
        public List<int> classSkills { get; private set; }

        public CharacterClass(Class_Type _type)
        {
            this.classType = _type;

            switch (classType)
            {
                case Class_Type.Bard:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Warrior:
                    classSkills = new List<int> { 0, 3, 17, 18, 27, 30 };
                    break;
                case Class_Type.Cleric:
                    classSkills = new List<int> { 3, 4, 10, 15, 18, 20, 26, 33 };
                    break;
                case Class_Type.Wizard:
                    classSkills = new List<int> { 4, 8, 9, 10, 11, 12, 22, 29 };
                    break;
                case Class_Type.Rogue:
                    classSkills = new List<int> { 0, 1, 2, 5, 9, 12, 18, 28, 30, 31, 34, 35 };
                    break;
                case Class_Type.Ranger:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Barbarian:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Monk:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Sorcerer:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Druid:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
                case Class_Type.Paladin:
                    classSkills = new List<int> { 2, 5, 6, 7, 11, 15, 26, 29, 31, 32, 34 };
                    break;
            }
        }
    }
}