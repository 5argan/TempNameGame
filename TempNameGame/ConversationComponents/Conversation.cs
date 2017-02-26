using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.ConversationComponents
{
    public class Conversation
    {
        private string currentScene;
        public string Name { get; }

        public string FirstScene { get; }
        public GameScene CurrentScene => Scenes[currentScene];

        public Dictionary<string, GameScene> Scenes { get; set; }

        public Texture2D Background { get; }
        public SpriteFont SpriteFont { get; }
        public string BackgroundName { get; set; }
        public string FontName { get; set; }

        public Conversation(string name, string firstScene, Texture2D background, SpriteFont font)
        {
            Scenes = new Dictionary<string, GameScene>();
            Name = name;
            FirstScene = firstScene;
            Background = background;
            SpriteFont = font;
        }

        public void Update(GameTime gameTime)
        {
            CurrentScene.Update(gameTime, PlayerIndex.One);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentScene.Draw(gameTime, spriteBatch, Background, SpriteFont);
        }

        public void AddScene(string sceneName, GameScene scene)
        {
            if (!Scenes.ContainsKey(sceneName))
                Scenes.Add(sceneName, scene);
        }

        public GameScene GetScene(string sceneName)
        {
            return Scenes.ContainsKey(sceneName) ? Scenes[sceneName] : null;
        }

        public void StartConversation()
        {
            currentScene = FirstScene;
        }

        public void ChangeScene(string sceneName)
        {
            currentScene = sceneName;
        }
    }
}