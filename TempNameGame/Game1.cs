using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.AvatarComponents;
using TempNameGame.CharacterComponents;
using TempNameGame.Components;
using TempNameGame.State;
using TempNameGame.State.GameStates;
using TempNameGame.TileEngine;

namespace TempNameGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private readonly GameStateManager _gameStateManager;
        private readonly CharacterManager _characterManager;
        //private readonly AvatarManager _avatarManager;

        private readonly IIntroScreenState _introScreenState;
        private readonly IMainMenuState _mainMenuState;
        private readonly IGamePlayState _gamePlayState;
        private IConversationState _conversationState;

        public SpriteBatch SpriteBatch { get; private set; }

        public static Rectangle ScreenRectangle { get; private set; }

        public Dictionary<AnimationKey, Animation> playerAnimations { get; } = new Dictionary<AnimationKey, Animation>();
        public GameStateManager GameStateManager => _gameStateManager;
        public CharacterManager CharacterManager => _characterManager;
        //public AvatarManager AvatarManager => _avatarManager;

        public IIntroScreenState IntroScreenState => _introScreenState;
        public IMainMenuState MainMenuState => _mainMenuState;
        public IGamePlayState GamePlayState => _gamePlayState;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenRectangle = new Rectangle(0, 0, 1280, 720);

            _graphics.PreferredBackBufferWidth = ScreenRectangle.Width;
            _graphics.PreferredBackBufferHeight = ScreenRectangle.Height;

            _gameStateManager = new GameStateManager(this);
            Components.Add(_gameStateManager);

            IsMouseVisible = true;

            _introScreenState = new IntroScreenState(this);
            _mainMenuState = new MainMenuState(this);
            _gamePlayState = new GamePlayState(this);
            _conversationState = new ConversationState(this);
            
            _gameStateManager.ChangeState((IntroScreenState)_introScreenState, PlayerIndex.One);
            _characterManager = CharacterManager.Instance;
            //_avatarManager = AvatarManager.FromFile()
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            Components.Add(new InputHandler(this));

            var animation = new Animation(3, 64, 64, 0, 0);
            playerAnimations.Add(AnimationKey.WalkDown, animation);
            animation = new Animation(3, 64, 64, 0, 64);
            playerAnimations.Add(AnimationKey.WalkLeft, animation);
            animation = new Animation(3, 64, 64, 0, 128);
            playerAnimations.Add(AnimationKey.WalkRight, animation);
            animation = new Animation(3, 64, 64, 0, 192);
            playerAnimations.Add(AnimationKey.WalkUp, animation);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            AvatarComponents.MoveManager.FillMoves();
            AvatarComponents.AvatarManager.FromFile(@".\Data\avatars.csv", Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

    }
}
