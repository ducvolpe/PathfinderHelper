using Dnd.FileData;
using Dnd.Gameplay;

namespace Dnd.Data
{
    public abstract class InfoCreator
    {
        public abstract CharacterInfo CreateInfo(InfoData _data);
    }
}