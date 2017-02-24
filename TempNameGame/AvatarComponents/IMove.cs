using System;

namespace TempNameGame.AvatarComponents
{
    public enum Target
    {
        Self,
        Enemy
    }

    public enum MoveType
    {
        Attack,
        Heal,
        Buff,
        Debuff,
        Status
    }

    public enum Status
    {
        Normal,
        Sleep,
        Poison,
        Paralysis
    }

    public enum MoveElement
    {
        None,
        Dark,
        Earth,
        Fire,
        Light,
        Water,
        Wind
    }

    public interface IMove : ICloneable
    {
        string Name { get; }
        Target Target { get; }
        MoveType MoveType { get; }
        MoveElement MoveElement { get; }
        Status Status { get; }
        int UnlockedAt { get; set; }
        bool IsUnlocked { get; }
        int Duration { get; set; }
        int Attack { get; }
        int Defense { get; }
        int Speed { get; }
        int Health { get; }
        void Unlock();
    }
}