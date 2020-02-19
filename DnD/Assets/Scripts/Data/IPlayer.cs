using Dnd.Gameplay;

public interface IPlayer
{
    void GetExperience(int _value);
    void GetStuff(Equipment _eq);
    void EquipStuff(int _weight, bool _value);
}
