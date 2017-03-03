﻿using System.Collections.Generic;
using InsigneVictoriae;
using InsigneVictoriae.Characters;
using InsigneVictoriae.State.GameStates;
using InsigneVictoriae.TileEngine;
using InsigneVictoriae.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TempNameGame.TileEngine;

namespace InsigneVictoriae.State.GameStates
{
    

    public interface IGamePlayState
    {
        void SetUpNewGame();
        void LoadExistingGame();
        void StartGame();
    }

    public class GamePlayState : GameStateBase, IGamePlayState
    {
        private TileMap _map;
        private Camera _camera;
        private ICharacter _selectedCharacter;
        private TileEngine.Engine _engine = new TileEngine.Engine(Game1.ScreenRectangle, 64, 64);

        public GamePlayState(Game game) : base(game)
        {
            game.Services.AddService(typeof(IGamePlayState), this);
            _selectedCharacter = null;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.CheckMouseReleased(MouseButton.Left))
            {
                //todo: defer input handling to combat map
                var clickedPosition = MapMouseToCell();
                var target = _map.GetCharacterAtCell((int)clickedPosition.X, (int)clickedPosition.Y);

                if (_selectedCharacter == null)
                    _selectedCharacter = target;
                else if (target == null)
                    _selectedCharacter.MoveToTile((int) clickedPosition.X, (int) clickedPosition.Y);
                else
                    _selectedCharacter.Attack(target);
            }

            _camera.LockCamera(_map, Game1.ScreenRectangle);

            base.Update(gameTime);
        }

        private Vector2 MapMouseToCell()
        {
            var mousePos = new Vector2(InputHandler.MouseState.X, InputHandler.MouseState.Y);
            var temp = TileEngine.Engine.VectorToCell(mousePos).ToVector2();
            var temp2 =  Vector2.Transform(temp,_camera.InverseTransform);
            return temp2;
        }
            


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (_map != null && _camera != null)
                _map.Draw(gameTime, _game.SpriteBatch, _camera);
        }

        public void SetUpNewGame()
        {
            var spriteSheet = _content.Load<Texture2D>(@"PlayerSprites\maleplayer");

            var tiles = _game.Content.Load<Texture2D>(@"Tiles\tileset1");
            var set = new TileSet(8, 8, 32, 32) {Texture = tiles};

            var background = new TileLayer(200, 200);
            var edge = new TileLayer(200, 200);
            var building = new TileLayer(200, 200);
            var decor = new TileLayer(200, 200);

            _map = new TileMap(set, background, edge, building, decor, "test-map");
            _map.Characters.Add(new Character(new Vector2(1, 1), new AnimatedSprite(_game.Content.Load<Texture2D>(@"CharacterSprites\teacherone"), _game.playerAnimations), 2, 1));
            _map.FillEdges();
            _map.FillBuilding();
            _map.FillDecoration();

            _camera = new Camera();
        }

        public void LoadExistingGame()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
        }
    }
}