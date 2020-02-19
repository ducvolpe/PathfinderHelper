using UnityEngine;

namespace Dnd.Gameplay
{
    public class ArmorClass
    {
        public int armorValue { get; set; }
        public int shieldValue { get; set; }
        public int dexterity { get; set; }
        public int sizeBonus { get; private set; }
        public int naturalArmor { get; set; }
        public int sumAC
        {
            get { return 10 + armorValue + shieldValue + dexterity + sizeBonus + naturalArmor; }
        }

        public ArmorClass(int _dex, int _size)
        {
            sizeBonus = Mathf.Clamp(_size, -3, 3);
            dexterity = _dex;
        }
    }
}