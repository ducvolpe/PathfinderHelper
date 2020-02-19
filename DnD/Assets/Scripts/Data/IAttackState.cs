using Dnd.Gameplay;

public interface IAttackState
{
    void AddWeapon(IWeapon _weapon);
    void RemoveWeapon(IWeapon _weapon);
    void ChangeWeapon(int _index);
    string AttackState(int[] _mods, ICharacter _opponent, int _distance);
}