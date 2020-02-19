using UnityEngine;

namespace Dnd.SO
{
    public class TraitData : ScriptableObject
    {
        public int id;
        public string traitName;
        public string requirements;
        public string advantage;
        public string deficiency;
        public string growth;
        public bool isOnce;
    }
}