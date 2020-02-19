using UnityEngine;

namespace Dnd.SO
{
    public class Armor : ScriptableObject
    {
        [Tooltip("Armor class: 0 - light, 1 - medium, 2 - heavy, 3 - shield")]
        public int armorClass;
        public int cost;
        public int defence;
        public string armorName;
        public string description;
    }
}