using Dnd.FileData;
using Dnd.Gameplay;

namespace Dnd.Data
{
    public class RaceInfoCreator : InfoCreator
    {
        public override CharacterInfo CreateInfo(InfoData _data)
        {
            RaceSkill skill = new RaceSkill()
            {
                id = _data.id,
                infoName = _data.infoName,
                description = _data.description,
                icon = _data.icon
            };

            return skill;
        }
    }
}