using System;

namespace Dnd.Gameplay
{
    public interface IWeapon
    {
        int Attack(int _bab, int _rollValue, int[] _mods);
        int Hit(int _bonus, int _distance);
    }
}