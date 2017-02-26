using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.ConversationComponents
{
    public class GameScene
    {
        protected Game _game;
        protected string _text;
        public int selectedIndex { get; }

        public string Text
        {
            get { return _text;}
            set { _text = value; }
        }

        public static Texture2D Selected { get; }
        public List<SceneOption> Options { get; set; }

        public bool IsMouseOver { get; }

        [ContentSerializerIgnore]
        public Color NormalColor { get; set; }

        [ContentSerializerIgnore]
        public Color HighlightColor { get; set; }

        public Vector2 MenuPosition { get; }

        [ContentSerializerIgnore]
        public SceneAction OptionAction => Options[selectedIndex].OptionAction;

        public string OptionScene => Options[selectedIndex].OptionScene;

        public string OptionText => Options[selectedIndex].OptionText;
    }
}