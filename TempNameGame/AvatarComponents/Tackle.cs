namespace TempNameGame.AvatarComponents
{
    public class Tackle : IMove
    {
        

        public string Name { get; }
        public Target Target { get; }
        public MoveType MoveType { get; }
        public MoveElement MoveElement { get; }
        public Status Status { get; }
        public int UnlockedAt { get; set; }
        public bool IsUnlocked { get; private set; }
        public int Duration { get; set; }
        public int Attack { get; }
        public int Defense { get; }
        public int Speed { get; }
        public int Health { get; }

        public Tackle()
        {
            Name = "Tackle";
            Target = Target.Enemy;
            MoveType = MoveType.Attack;
            MoveElement = MoveElement.None;
            Status = Status.Normal;
            Duration = 1;
            IsUnlocked = false;
            Attack = MoveManager.Random.Next(0, 0);
            Defense = MoveManager.Random.Next();
            Speed = MoveManager.Random.Next();
            Health = MoveManager.Random.Next(10, 15);
        }
        
        public void Unlock()
        {
            IsUnlocked = true;
        }

        public object Clone() =>
            new Tackle { IsUnlocked = this.IsUnlocked };
    }
}