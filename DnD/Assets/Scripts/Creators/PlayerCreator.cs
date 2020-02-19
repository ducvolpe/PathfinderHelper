using Dnd.FileData;
using Dnd.Gameplay;
using System.Collections.Generic;

namespace Dnd.Data
{
    public class PlayerCreator : CharacterCreator
    {
        public override Character CreateCharacter(CharacterData _data)
        {
            PlayerData p_data = _data as PlayerData;

            int[] modificators = new int[] { p_data.strength, p_data.dexterity, p_data.persistance, p_data.intelligence, p_data.wisdom, p_data.charisma };

            RaceInfoCreator raceCreator = new RaceInfoCreator();
            List<RaceSkill> r_skills = GetData<RaceSkill, RaceInfoData>(raceCreator, p_data.raceSkills);

            Race.Race_type r_type = GetRace(p_data.raceName);
            Race playerRace = new Race(r_type, r_skills, p_data.raceDescription);
            ClassInfoCreator classCreator = new ClassInfoCreator();
            List<ClassSkill> c_skills = GetData<ClassSkill, ClassInfoData>(classCreator, p_data.classSkills);

            List<CharacterClass> classes = new List<CharacterClass>();
            List<int> classSkills = new List<int>();

            for (int i = 0; i < p_data.classes.Count; i++)
            {
                CharacterClass.Class_Type classType = GetClass(p_data.classes[i]);
                CharacterClass currentClass = new CharacterClass(classType);
                if (classes.Contains(currentClass))
                    classSkills.AddRange(currentClass.classSkills);
                classes.Add(currentClass);
            }

            Level level = new Level(p_data.experience, classes, c_skills);
            Skills mainSkills = new Skills(modificators, classSkills);
            for (int i = 0; i < mainSkills.allSkills.Count; i++)
                mainSkills.PlacePoint(i, p_data.skillPoints[i], level.generalLevel);

            ArmorClass ac = new ArmorClass(p_data.dexterity, playerRace.size)
            {
                armorValue = p_data.armorValue,
                shieldValue = p_data.shieldValue,
                naturalArmor = p_data.naturalArmor
            };
            Attack attack = new Attack(p_data.bab, p_data.attackBonus, p_data.hitBonus);

            SpellCreator spellCreator = new SpellCreator();
            List<Spell> s_skills = GetData<Spell, SpellInfoData>(spellCreator, p_data.spells);

            TraitCreator traitCreator = new TraitCreator();
            List<Trait> t_skills = GetData<Trait, TraitInfoData>(traitCreator, p_data.traits);

            List<Equipment> equipment = GetEquipment(p_data, ref attack);

            PlayingCharacter player = new PlayingCharacter(p_data.c_name, p_data.avatar, modificators, p_data.maxWeight, mainSkills, ac, attack, equipment, s_skills, t_skills, playerRace, level);

            foreach (var eq in equipment)
                eq.onEquip += player.EquipStuff;

            return player;
        }

        private Race.Race_type GetRace(string _name)
        {
            switch (_name)
            {
                case "Human":
                    return Race.Race_type.Man;
                case "Halfling":
                    return Race.Race_type.Halfling;
                case "Halfelf":
                    return Race.Race_type.Halfelf;
                case "Halforc":
                    return Race.Race_type.Halforc;
                case "Gnome":
                    return Race.Race_type.Gnome;
                case "Dwarf":
                    return Race.Race_type.Dwarf;
                case "Elf":
                    return Race.Race_type.Elf;
            }

            return Race.Race_type.Man;
        }

        private CharacterClass.Class_Type GetClass(string _name)
        {
            switch (_name)
            {
                case "Bard":
                    return CharacterClass.Class_Type.Bard;
                case "Cleric":
                    return CharacterClass.Class_Type.Cleric;
                case "Druid":
                    return CharacterClass.Class_Type.Druid;
                case "Monk":
                    return CharacterClass.Class_Type.Monk;
                case "Barbarian":
                    return CharacterClass.Class_Type.Barbarian;
                case "Paladin":
                    return CharacterClass.Class_Type.Paladin;
                case "Ranger":
                    return CharacterClass.Class_Type.Ranger;
                case "Rogue":
                    return CharacterClass.Class_Type.Rogue;
                case "Warrior":
                    return CharacterClass.Class_Type.Warrior;
                case "Wizard":
                    return CharacterClass.Class_Type.Wizard;
                case "Sorcerer":
                    return CharacterClass.Class_Type.Sorcerer;
            }

            return CharacterClass.Class_Type.Warrior;
        }
    }
}