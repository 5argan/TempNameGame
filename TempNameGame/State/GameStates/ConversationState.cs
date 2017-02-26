using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.CharacterComponents;
using TempNameGame.Components;
using TempNameGame.ConversationComponents;
using TempNameGame.PlayerComponents;

namespace TempNameGame.State.GameStates
{
    public interface IConversationState
    {
        void SetConversation(Player player, ICharacter character);
        void StartConversation();
    }

    public class ConversationState : GameStateBase, IConversationState
    {
        private Conversation _conversation;
        private SpriteFont _font;
        private Texture2D _background;
        private Player _player;
        private ICharacter _speaker;

        public ConversationState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IConversationState), this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _font = _game.Content.Load<SpriteFont>(@"Fonts\InterfaceFont");
            _background = _game.Content.Load<Texture2D>(@"Scenes\scenebackground");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.CheckKeyReleased(Keys.Space) || InputHandler.CheckKeyReleased(Keys.Enter))
            {
                switch (_conversation.CurrentScene.OptionAction.Action)
                {
                    case ActionType.Buy:
                        break;
                    case ActionType.Change:
                        _speaker.SetConversation(_conversation.CurrentScene.OptionScene);
                        _manager.PopState();
                        break;
                    case ActionType.End:
                        _manager.PopState();
                        break;
                    case ActionType.GiveItems:
                        break;
                    case ActionType.GiveKey:
                        break;
                    case ActionType.Quest:
                        break;
                    case ActionType.Sell:
                        break;
                    case ActionType.Talk:
                        _conversation.ChangeScene(_conversation.CurrentScene.OptionScene);
                        break;
                }
            }

            _conversation.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _game.SpriteBatch.Begin();
            _conversation.Draw(gameTime, _game.SpriteBatch);
            _game.SpriteBatch.End();
        }

        public void SetConversation(Player player, ICharacter character)
        {
            _player = player;
            _speaker = character;

            if (ConversationManager.ConversationList.ContainsKey(character.Conversation))
                _conversation = ConversationManager.ConversationList[character.Conversation];
            else
                _manager.PopState();
        }

        public void StartConversation()
        {
            _conversation.StartConversation();
        }
    }
}