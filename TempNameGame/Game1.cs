﻿using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TempNameGame.Components;
using TempNameGame.State;
using TempNameGame.State.GameStates;

namespace TempNameGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private GameStateManager _gameStateManager;
        private readonly IIntroScreenState _introScreenState;
        private readonly IMainMenuState _mainMenuState;
        private readonly IGamePlayState _gamePlayState;

        public SpriteBatch SpriteBatch { get; private set; }

        public static Rectangle ScreenRectangle { get; private set; }

        public GameStateManager GameStateManager => GameStateManager;

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
            
            _gameStateManager.ChangeState((IntroScreenState)_introScreenState, PlayerIndex.One);
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
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
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
