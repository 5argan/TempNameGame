using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsigneVictoriae.Characters
{
    public class CharacterManager
    {
        public bool Interact(ICharacter actor, ICharacter target) =>
            actor.PlayerId != target.PlayerId ? EngageCombat(actor, target) : SupportUnit(actor, target);

        private static bool SupportUnit(ICharacter actor, ICharacter target) =>
            CanTarget(actor, target) && actor.Support(target);

        private static bool EngageCombat(ICharacter actor, ICharacter target)
        {
            if (!CanTarget(actor, target))
                return false;

            if (!actor.Attack(target))
                return false;

            return !CanTarget(target, actor) || target.Attack(actor);
        }

        private static int GetDistance(ICharacter actor, ICharacter target) =>
            (int)(Math.Abs(actor.Position.X - target.Position.X) + Math.Abs(actor.Position.Y - target.Position.Y));

        private static bool CanTarget(ICharacter actor, ICharacter target) =>
            actor.IsAlive() && target.IsAlive() && actor.AttackRange >= GetDistance(actor, target);
    }
}