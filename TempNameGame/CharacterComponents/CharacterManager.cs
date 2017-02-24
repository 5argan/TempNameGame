using System.Collections.Generic;

namespace TempNameGame.CharacterComponents
{
    public class CharacterManager
    {
        private readonly Dictionary<string, ICharacter> _characters = new Dictionary<string, ICharacter>();
        public static CharacterManager Instance { get; } = new CharacterManager();

        private CharacterManager()
        {
            
        }

        public ICharacter GetCharacter(string name)
        {
            return _characters.ContainsKey(name) ? _characters[name] : null;
        }

        public void AddCharacter(string name, ICharacter character)
        {
            if (!_characters.ContainsKey(name))
            {
                _characters.Add(name, character);
            }
        }
    }
}