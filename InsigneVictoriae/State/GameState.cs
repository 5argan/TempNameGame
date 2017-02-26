using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace InsigneVictoriae.State
{
    public abstract partial class GameState : DrawableGameComponent, IGameState
    {
        protected readonly IStateManager _manager;
        protected readonly List<GameComponent> _childComponents;
        protected GameState _tag;
        protected PlayerIndex? _currentPlayerIndex;
        protected ContentManager _content;

        public PlayerIndex? CurrentPlayerIndex
        {
            get { return _currentPlayerIndex; }
            set { _currentPlayerIndex = value; }
        }
        public GameState Tag => _tag;
        public List<GameComponent> Components => _childComponents;

        public GameState(Game game) : base(game)
        {
            _tag = this;
            _childComponents = new List<GameComponent>();
            _content = Game.Content;
            _manager = (IStateManager) Game.Services.GetService(typeof(IStateManager));
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _childComponents)
                if (component.Enabled) component.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (var component in _childComponents)
            {
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null && gameComponent.Visible)
                {
                    gameComponent.Draw(gameTime);
                }
            }
        }

        protected internal virtual void StateChanged(object sender, EventArgs e)
        {
            if (_manager.CurrentState == _tag)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public virtual void Show()
        {
            Enabled = true;
            Visible = true;
            foreach (var component in _childComponents)
            {
                component.Enabled = true;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null) gameComponent.Visible = true;
            }
        }

        public virtual void Hide()
        {
            Enabled = false;
            Visible = false;
            foreach (var component in _childComponents)
            {
                component.Enabled = false;
                var gameComponent = component as DrawableGameComponent;
                if (gameComponent != null) gameComponent.Visible = false;
            }
        }
    }
}