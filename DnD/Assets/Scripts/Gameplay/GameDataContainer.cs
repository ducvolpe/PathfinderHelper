using Dnd.Data;
using Dnd.FileData;
using Dnd.Gameplay;
using System.Collections.Generic;

namespace Dnd.Game
{
    public class GameDataContainer : Singleton<GameDataContainer>
    {
        public List<PlayingCharacter> playersData { get; private set; }
        public List<ICharacter> allCharacters { get; private set; }
        public List<IEnemy> worldCharacters { get; private set; }
        public PlayingCharacter currentCharacter { get; set; }

        private CharacterCreator[] _creators;

        public void StartGame(List<PlayerData> _data)
        {
            _creators = new CharacterCreator[] { new PlayerCreator(), new EnemyCreator() };
            playersData = new List<PlayingCharacter>();
            allCharacters = new List<ICharacter>();
            worldCharacters = new List<IEnemy>();

            foreach (var p in _data)
                AddPlayingCharacter(p);
        }

        public void AddPlayingCharacter(PlayerData _player)
        {
            Character character = _creators[0].CreateCharacter(_player);
            PlayingCharacter newPlayer = (PlayingCharacter)character;

            if (!allCharacters.Contains(character))
                allCharacters.Add(character);

            if (!playersData.Contains(newPlayer))
                playersData.Add(newPlayer);
        }

        public void RemovePlayer(PlayingCharacter _player)
        {
            if (allCharacters.Contains(_player))
                allCharacters.Remove(_player);

            if (playersData.Contains(_player))
                playersData.Remove(_player);
        }

        public void AddWorldCharacter(EnemyData _enemy, int _distance)
        {
            Character character = _creators[1].CreateCharacter(_enemy);
            IEnemy newEnemy = (IEnemy)character;
            if (!worldCharacters.Contains(newEnemy))
                worldCharacters.Add(newEnemy);

            if (!allCharacters.Contains(character))
                allCharacters.Add(character);
        }

        public void RemoveWorldCharacter(Character _enemy)
        {
            IEnemy r_enemy = (IEnemy)_enemy;
            if (allCharacters.Contains(_enemy))
                allCharacters.Remove(_enemy);

            if (worldCharacters.Contains(r_enemy))
                worldCharacters.Remove(r_enemy);
        }
    }
}