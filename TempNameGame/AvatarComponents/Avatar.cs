using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TempNameGame.AvatarComponents
{
    public enum AvatarElement
    {
        Dark, Earth, Fire, Light, Water, Wind
    }
    public class Avatar
    {
        private int _level;
        private int _costToBuy;

        public string Name { get; private set; }

        public int Level
        {
            get { return _level; }
            set { _level = MathHelper.Clamp(value, 1, 100); }
        }

        public long Experience { get; private set; }

        public Texture2D Texture { get; private set; }

        public Dictionary<string, IMove> KnownMoves { get; }

        public AvatarElement Element { get; private set; }

        public List<IMove> Effects { get; }

        public static Random Random { get; } = new Random();

        public int BaseAttack { get; private set; }

        public int BaseDefense { get; private set; }

        public int BaseSpeed { get; private set; }

        public int BaseHealth { get; private set; }

        public int CurrentHealth { get; private set; }

        public bool IsAlive => CurrentHealth > 0;

        private Avatar()
        {
            _level = 1;
            KnownMoves = new Dictionary<string, IMove>();
            Effects = new List<IMove>();
        }

        public void ResolveMove(IMove move, Avatar target)
        {
            bool found;
            switch (move.Target)
            {
                case Target.Self:
                    if (move.MoveType == MoveType.Buff)
                    {
                        found = false;
                        foreach (var effect in Effects)
                        {
                            if (effect.Name != move.Name) continue;
                            effect.Duration += move.Duration;
                            found = true;
                        }

                        if (!found)
                            Effects.Add((IMove) move.Clone());
                    }
                    else if (move.MoveType == MoveType.Heal)
                    {
                        CurrentHealth += move.Health;
                        if (CurrentHealth > BaseHealth)
                        {
                            CurrentHealth = BaseHealth;
                        }
                    }
                    else if (move.MoveType == MoveType.Status)
                    {
                        //TODO
                    }
                    break;
                case Target.Enemy:
                    if (move.MoveType == MoveType.Debuff)
                    {
                        found = false;
                        foreach (IMove t in target.Effects)
                        {
                            if (t.Name != move.Name) continue;
                            t.Duration += move.Duration;
                            found = true;
                        }

                        if (!found)
                            target.Effects.Add((IMove)move.Clone());
                    }
                    else if (move.MoveType == MoveType.Attack)
                    {
                        var modifier = GetMoveModifier(move.MoveElement, target.Element);
                        var tDamage = GetAttack() + move.Health*modifier - target.GetDefense();
                        if (tDamage < 1f) tDamage = 1f;
                        target.ApplyDamage((int) tDamage);
                    }
                    break;
            }
            }
        

        public static float GetMoveModifier(MoveElement moveElement, AvatarElement avatarElement)
        {
            var modifier = 1f;
            switch (moveElement)
            {
                case MoveElement.Dark:
                    if (avatarElement == AvatarElement.Light)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Wind)
                    {
                        modifier -= .25f;
                    }
                    break;
                case MoveElement.Earth:
                    if (avatarElement == AvatarElement.Water)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Wind)
                    {
                        modifier -= .25f;
                    }
                    break;
                case MoveElement.Fire:
                    if (avatarElement == AvatarElement.Wind)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Water)
                    {
                        modifier -= .25f;
                    }
                    break;
                case MoveElement.Light:
                    if (avatarElement == AvatarElement.Dark)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Earth)
                    {
                        modifier -= .25f;
                    }
                    break;
                case MoveElement.Water:
                    if (avatarElement == AvatarElement.Fire)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Earth)
                    {
                        modifier -= .25f;
                    }
                    break;
                case MoveElement.Wind:
                    if (avatarElement == AvatarElement.Light)
                    {
                        modifier += .25f;
                    }
                    else if (avatarElement == AvatarElement.Earth)
                    {
                        modifier -= .25f;
                    }
                    break;

            }
            return modifier;
        }

        public void ApplyDamage(int tDamage)
        {
            CurrentHealth -= tDamage;
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < Effects.Count; i++)
            {
                Effects[i].Duration--;
                if (Effects[i].Duration < 1)
                {
                    Effects.RemoveAt(i);
                    i--;
                }
            }
        }

        public int GetAttack()
        {
            var attackMod = 0;
            foreach (var move in Effects)
            {
                if (move.MoveType == MoveType.Buff)
                    attackMod += move.Attack;

                if (move.MoveType == MoveType.Debuff)
                    attackMod -= move.Attack;
            }

            return BaseAttack + attackMod;
        }

        public int GetDefense()
        {
            var defenseMod = 0;
            foreach (var move in Effects)
            {
                if (move.MoveType == MoveType.Buff)
                    defenseMod += move.Defense;

                if (move.MoveType == MoveType.Debuff)
                    defenseMod -= move.Defense;
            }

            return BaseDefense + defenseMod;
        }

        public int GetSpeed()
        {
            var speedMod = 0;
            foreach (var move in Effects)
            {
                if (move.MoveType == MoveType.Buff)
                    speedMod += move.Speed;
                if (move.MoveType == MoveType.Debuff)
                    speedMod -= move.Speed;
            }
            return BaseSpeed + speedMod;
        }

        public int GetHealth()
        {
            var healthMod = 0;
            foreach (var move in Effects)
            {
                if (move.MoveType == MoveType.Buff)
                    healthMod += move.Health;
                if (move.MoveType == MoveType.Debuff)
                    healthMod -= move.Health;
            }

            return BaseHealth + healthMod;
        }

        public void StartCombat()
        {
            Effects.Clear();
            CurrentHealth = BaseHealth;
        }

        public long WinBattle(Avatar target)
        {
            var levelDiff = target.Level - _level;
            long expGained;
            if (levelDiff <= -10)
            {
                expGained = 10;
            }
            else if (levelDiff <= -5)
            {
                expGained = (long)(100f*(float) Math.Pow(2, levelDiff));
            }
            else if (levelDiff <= 0)
            {
                expGained = (long)(50f * (float)Math.Pow(2, levelDiff));
            }
            else if (levelDiff <= 5)
            {
                expGained = (long)(5f * (float)Math.Pow(2, levelDiff));
            }
            else if (levelDiff <= 10)
            {
                expGained = (long) (10f*(float) Math.Pow(2, levelDiff));
            }
            else
            {
                expGained = (long)(50f * (float)Math.Pow(2, levelDiff));
            }

            return expGained;
        }

        public long LoseBattle(Avatar target) => WinBattle(target) / 2;

        public bool CheckLevelUp()
        {
            if (Experience < 50*(1 + (long) Math.Pow(_level, 2.5)))
                return false;

            _level++;
            return true;
        }

        public void AssignPoint(string stat, int points)
        {
            switch (stat)
            {
                case "Attack":
                    BaseAttack += points;
                    break;
                case "Defense":
                    BaseDefense += points;
                    break;
                case "Speed":
                    BaseSpeed += points;
                    break;
                case "Health":
                    BaseHealth += points*5;
                    break;
            }
        }

        public object Clone()
        {
            var avatar = new Avatar
            {
                Name = Name,
                Texture = Texture,
                Element = Element,
                _costToBuy = _costToBuy,
                _level = _level,
                Experience = Experience,
                BaseAttack = BaseAttack,
                BaseDefense = BaseDefense,
                BaseSpeed = BaseSpeed,
                BaseHealth = BaseHealth,
                CurrentHealth = BaseHealth
            };

            foreach (var s in KnownMoves.Keys)
            {
                avatar.KnownMoves.Add(s, KnownMoves[s]);
            }

            return avatar;
        }
    }
}