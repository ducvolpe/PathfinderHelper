using Dnd.FileData;
using Dnd.Gameplay;

namespace Dnd.Data
{
    public class TraitCreator : InfoCreator
    {
        public override CharacterInfo CreateInfo(InfoData _data)
        {
            Trait skill = new Trait()
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