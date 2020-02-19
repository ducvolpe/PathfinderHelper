using System.Collections.Generic;

namespace Dnd.Gameplay
{
    public class Race
    {
        #region public vars
        public enum Race_type
        {
            Man,
            Dwarf,
            Elf,
            Halfling,
            Halfelf,
            Halforc,
            Gnome
        }

        public Race_type race { get; private set; }
        public int size { get; set; }
        public int baseSpeed { get; set; }
        public string raceName
        {
            get
            {
                string rName = string.Empty;
                switch (race)
                {
                    case Race_type.Man:
                        rName = "Человек";
                        break;
                    case Race_type.Dwarf:
                        rName = "Дварф";
                        break;
                    case Race_type.Elf:
                        rName = "Эльф";
                        break;
                    case Race_type.Gnome:
                        rName = "Гном";
                        break;
                    case Race_type.Halfelf:
                        rName = "Полуэльф";
                        break;
                    case Race_type.Halfling:
                        rName = "Полурослик";
                        break;
                    case Race_type.Halforc:
                        rName = "Полуорк";
                        break;
                }

                return rName;
            }
        }

        public List<RaceSkill> raceSkills;
        public string raceDescription { get; set; }

        public Race(Race_type _type, List<RaceSkill> _skills, string _description)
        {
            race = _type;
            raceSkills = _skills;
            raceDescription = _description;
            CalculateRace();
        }

        private void CalculateRace()
        {
            switch (race)
            {
                case Race_type.Man:
                    size = 0;
                    baseSpeed = 30;
                    break;
                case Race_type.Dwarf:
                    size = 0;
                    baseSpeed = 20;
                    break;
                case Race_type.Elf:
                    size = 0;
                    baseSpeed = 30;
                    break;
                case Race_type.Halfling:
                    size = -1;
                    baseSpeed = 20;
                    break;
                case Race_type.Halfelf:
                    size = 0;
                    baseSpeed = 30;
                    break;
                case Race_type.Halforc:
                    size = 0;
                    baseSpeed = 30;
                    break;
                case Race_type.Gnome:
                    size = -1;
                    baseSpeed = 20;
                    break;
            }
        }
        #endregion
    }
}