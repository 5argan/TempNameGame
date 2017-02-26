using System;
using System.Collections.Generic;

namespace TempNameGame.AvatarComponents
{
    public class MoveManager
    {
        private static Dictionary<string, IMove> allMoves = new Dictionary<string, IMove>();
        public static Random Random { get; } = new Random();

        public static void FillMoves()
        {

        }

        public static IMove GetMove(string name) => 
            allMoves.ContainsKey(name) ? (IMove) allMoves[name].Clone() : null;

        public static void AddMove(IMove move)
        {
            if (!allMoves.ContainsKey(move.Name))
                allMoves.Add(move.Name, move);
        }
    }
}