using Dnd.FileData;
using Dnd.Gameplay;

namespace Dnd.Data
{
    public class SpellCreator : InfoCreator
    {
        public override CharacterInfo CreateInfo(InfoData _data)
        {
            SpellInfoData spellData = _data as SpellInfoData;
            Spell skill = new Spell()
            {
                id = spellData.id,
                infoName = spellData.infoName,
                description = string.Format("<b>Школа:</b> {0}<bd><b>Круг:</b> {1}<bd><b>Время сотворения:</b> {2}<bd><b>Компоненты:</b> {3}<bd><b>Дистанция:</b> {4}<bd><b>Эффект:</b> {5}<bd><b>Длительность:</b> {6}<bd><b>Испытание:</b> {7}<bd><b>Устойчивость к магии:</b> {8}<br>{9}",
                                spellData.school, spellData.spellLevel, spellData.castTime, spellData.components, spellData.range, spellData.effect, spellData.duration, spellData.savingThrow, spellData.resistance, spellData.description),
                icon = spellData.icon
            };

            return skill;
        }
    }
}