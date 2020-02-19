using System;

public interface ICharacter
{
    bool RollCheckAC(int _rollValue);
    bool RollCheckSkill(int _index, int _rollValue);
    string RollCheckAttack(ICharacter _opponent, int _distance);
}
