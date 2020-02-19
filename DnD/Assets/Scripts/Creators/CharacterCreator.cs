using Dnd.FileData;
using Dnd.Gameplay;
using System.Collections.Generic;

namespace Dnd.Data
{
    public abstract class CharacterCreator
    {
        public abstract Character CreateCharacter(CharacterData _data);

        protected List<T> GetData<T, V>(InfoCreator _creator, List<V> _data) where T : CharacterInfo where V : InfoData
        {
            List<T> c_skills = new List<T>();
            for (int i = 0; i < _data.Count; i++)
            {
                T skill = (T)_creator.CreateInfo(_data[i]);
                if (!c_skills.Exists(x => x.id == skill.id))
                    c_skills.Add(skill);
            }

            return c_skills;
        }

        protected List<Equipment> GetEquipment(CharacterData _data, ref Attack _attack)
        {
            List<Equipment> equipment = new List<Equipment>();

            if (_data.weaponIds.Count > 0)
            {
                for (int i = 0; i < _data.weaponIds.Count; i++)
                {
                    EquipmentData e = _data.equipment.Find(x => x.id == _data.weaponIds[i]);
                    if (e != null)
                    {
                        string[] splitData = e.e_type.Split('_');
                        if (splitData.Length > 1)
                        {
                            if (splitData[1] == "d")
                            {
                                DistanceWeaponData dwd = e as DistanceWeaponData;
                                DistanceWeapon dw = new DistanceWeapon()
                                {
                                    id = dwd.id,
                                    eName = dwd.e_name,
                                    weight = dwd.weight,
                                    maxDistance = dwd.maxDistance,
                                    damageDice = dwd.damageDice,
                                    ammoCount = dwd.ammoCount
                                };

                                equipment.Add(dw);
                                _attack.AddWeapon(dw);
                            }
                            else if (splitData[1] == "m")
                            {
                                MeleeWeaponData mwd = e as MeleeWeaponData;
                                MeleeWeapon mw = new MeleeWeapon()
                                {
                                    id = mwd.id,
                                    eName = mwd.e_name,
                                    weight = mwd.weight,
                                    hands = mwd.hands,
                                    damageDice = mwd.damageDice,
                                    size = mwd.size
                                };
                                
                                equipment.Add(mw);
                                _attack.AddWeapon(mw);
                            }
                        }
                    }
                }
            }

            return equipment;
        }
    }
}