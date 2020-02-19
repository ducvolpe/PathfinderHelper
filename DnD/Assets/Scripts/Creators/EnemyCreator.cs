using Dnd.FileData;
using Dnd.Gameplay;
using System.Collections.Generic;

namespace Dnd.Data
{
    public class EnemyCreator : CharacterCreator
    {
        public override Character CreateCharacter(CharacterData _data)
        {
            EnemyData e_data = _data as EnemyData;

            int[] modificators = new int[] { e_data.strength, e_data.dexterity, e_data.persistance, e_data.intelligence, e_data.wisdom, e_data.charisma };

            Skills mainSkills = new Skills(modificators, new List<int>());
            for (int i = 0; i < mainSkills.allSkills.Count; i++)
                mainSkills.PlacePoint(i, e_data.skillPoints[i], e_data.healthDice);

            ArmorClass ac = new ArmorClass(e_data.dexterity, e_data.size)
            {
                armorValue = e_data.armorValue,
                shieldValue = e_data.shieldValue,
                naturalArmor = e_data.naturalArmor
            };
            Attack attack = new Attack(e_data.bab, e_data.attackBonus, e_data.hitBonus);

            SpellCreator spellCreator = new SpellCreator();
            List<Spell> s_skills = GetData<Spell, SpellInfoData>(spellCreator, e_data.spells);

            TraitCreator traitCreator = new TraitCreator();
            List<Trait> t_skills = GetData<Trait, TraitInfoData>(traitCreator, e_data.traits);

            List<Equipment> equipment = GetEquipment(e_data, ref attack);

            EnemyCharacter enemy = new EnemyCharacter(e_data.c_name, e_data.avatar, modificators, e_data.maxWeight, mainSkills, ac, attack, equipment, s_skills, t_skills, e_data.bestiaryType, e_data.relation, e_data.difficulty);
            return enemy;
        }
    }
}