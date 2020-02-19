using Dnd.FileData;
using Dnd.Gameplay;

namespace Dnd.Data
{
    public class ClassInfoCreator : InfoCreator
    {
        public override CharacterInfo CreateInfo(InfoData _data)
        {
            ClassSkill skill = new ClassSkill()
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