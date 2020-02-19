using System;
using System.Collections.Generic;

namespace Dnd.FileData
{
    [Serializable]
    public class Content
    {
        public List<PlayerData> playingCharacters;
    }

    [Serializable]
    public class StuffContent
    {
        public List<EquipmentData> allEquipment;
        public List<MeleeWeaponData> meleeWeapon;
        public List<DistanceWeaponData> distanceWeapon;
    }
}