using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.AvatarComponents;

namespace TempNameGame.PlayerComponents
{
    public class PPlayer : Player
    {
        public const int MAX_AVATARS = 6;
        private readonly List<Avatar> _battleAvatars = new List<Avatar>();
        private int _currentAvatar;

        public override Avatar CurrentAvatar =>
            _battleAvatars[_currentAvatar];

        public PPlayer(Game game, string name, bool gender, Texture2D texture) : base(game, name, gender, texture)
        {
        }

        public void SetCurrentAvatar(int index)
        {
            if (index < 0 || index > MAX_AVATARS)
                throw new IndexOutOfRangeException();
            _currentAvatar = index;
        }

        public Avatar GetBattleAvatar(int index)
        {
            if (index < 0 || index > MAX_AVATARS)
            {
                throw new IndexOutOfRangeException();
            }
            return _battleAvatars[index];
        }

        public void AddBattleAvatar(Avatar avatar)
        {
            if (_battleAvatars.Count >= MAX_AVATARS - 1)
            {
                throw new OverflowException();
            }
            _battleAvatars.Add(avatar);
        }

        public void RemoveBattleAvatar(int index)
        {
            if (index >= _battleAvatars.Count)
                throw new IndexOutOfRangeException();

            _battleAvatars.RemoveAt(index);
        }
    }
}