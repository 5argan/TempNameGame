using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TempNameGame.State;

namespace InsigneVictoriae.State
{
    public class GameStateManager : GameComponent, IStateManager
    {
        private const int DRAW_ORDER_INCREMENT = 50;
        private const int START_DRAW_ORDER = 5000;

        private readonly Stack<GameState> _gameStates = new Stack<GameState>();
        private int _drawOrder;

        public GameStateManager(Game game) : base(game)
        {
            game.Services.AddService(typeof(IStateManager), this);
        }

        public IGameState CurrentState => _gameStates.Peek();
        public event EventHandler StateChanged;

        public void PushState(GameState state, PlayerIndex? index)
        {
            _drawOrder += DRAW_ORDER_INCREMENT;
            AddState(state, index);
            OnStateChanged();
        }

        private void AddState(GameState state, PlayerIndex? index)
        {
            _gameStates.Push(state);
            state.CurrentPlayerIndex = index;
            Game.Components.Add(state);
            StateChanged += state.StateChanged;
        }

        public void PopState()
        {
            if (_gameStates.Count != 0)
            {
                RemoveState();
                _drawOrder -= DRAW_ORDER_INCREMENT;
                OnStateChanged();
            }
        }

        private void RemoveState()
        {
            var state = _gameStates.Peek();
            StateChanged -= state.StateChanged;
            Game.Components.Remove(state);
            _gameStates.Pop();
        }

        public void ChangeState(GameState state, PlayerIndex? index)
        {
            while (_gameStates.Count > 0)
            {
                RemoveState();
            }
            _drawOrder = START_DRAW_ORDER;
            state.DrawOrder = _drawOrder;
            _drawOrder += DRAW_ORDER_INCREMENT;

            AddState(state, index);
            OnStateChanged();
        }

        public bool ContainsState(GameState state)
        {
            return _gameStates.Contains(state);
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke(this, null);
        }
    }


}