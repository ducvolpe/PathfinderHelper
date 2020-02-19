using System;
using System.Collections.Generic;

namespace Dnd.FileData
{
    [Serializable]
    public class CharacterData
    {
        public string c_name;
        public string avatar;
        public int strength;
        public int dexterity;
        public int persistance;
        public int intelligence;
        public int wisdom;
        public int charisma;
        public int maxWeight;
        public List<int> skillPoints;
        public int armorValue;
        public int shieldValue;
        public int naturalArmor;
        public List<int> weaponIds;
        public int bab;
        public int attackBonus;
        public int hitBonus;
        public List<int> equipmentIds;
        public List<EquipmentData> equipment;
        public List<TraitInfoData> traits;
        public List<SpellInfoData> spells;
    }

    [Serializable]
    public class PlayerData : CharacterData
    {
        public string raceName;
        public string raceDescription;
        public int experience;
        public List<string> classes;
        public List<RaceInfoData> raceSkills;
        public List<ClassInfoData> classSkills;
    }

    [Serializable]
    public class EnemyData : CharacterData
    {
        public int healthDice;
        public int size;
        public string bestiaryType;
        public int relation;
        public int difficulty;
    }

    [Serializable]
    public class EquipmentData
    {
        public int id;
        public string e_name;
        public string e_type;
        public int weight;
    }

    [Serializable]
    public class DistanceWeaponData : EquipmentData
    {
        public int maxDistance;
        public int damageDice;
        public int ammoCount;
    }

    [Serializable]
    public class MeleeWeaponData : EquipmentData
    {
        public int hands;
        public int damageDice;
        public int size;
    }

    [Serializable]
    public class InfoData
    {
        public int id;
        public string infoName;
        public string description;
        public string icon;
    }

    [Serializable]
    public class RaceInfoData : InfoData
    {

    }

    [Serializable]
    public class ClassInfoData : InfoData
    {

    }

    [Serializable]
    public class TraitInfoData : InfoData
    {

    }

    [Serializable]
    public class SpellInfoData : InfoData
    {
        public string school;
        public int spellLevel;
        public string castTime;
        public string components;
        public string range;
        public string effect;
        public string duration;
        public string savingThrow;
        public string resistance;
    }
}