using System;

namespace Dnd.Gameplay
{
    public abstract class Equipment
    {
        public delegate void OnEquip(int _weight, bool _value);
        public event OnEquip onEquip;

        public int id;
        public string eName;
        public int weight;
        public bool isEquiped
        {
            get { return _isEquip; }
            set
            {
                _isEquip = value;
                onEquip(weight, _isEquip);
            }
        }

        private bool _isEquip;
    }
}