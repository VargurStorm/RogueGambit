using System;

namespace RogueGambit.Models;

public class MoveSet
{
    public readonly HashSet<Move> Moves = new();

    public void AddMove(Vector2 direction, int range, params MoveAttrib[] attributes)
    {
        Moves.Add(new Move
        {
            Direction = direction,
            Range = range,
            Attributes = new HashSet<MoveAttrib>(attributes)
        });
    }

    public bool RemoveMove(Vector2 direction, int range)
    {
        return Moves.RemoveWhere(m => m.Direction == direction && m.Range == range) > 0;
    }

    public IEnumerable<Move> GetMovesWithAttributes(params MoveAttrib[] attributes)
    {
        return Moves.Where(m => attributes.All(a => m.Attributes.Contains(a)));
    }

    public class Move : IEquatable<Move>
    {
        public Vector2 Direction { get; init; }
        public int Range { get; init; }
        public HashSet<MoveAttrib> Attributes { get; init; }

        public bool Equals(Move other)
        {
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Direction.Equals(other.Direction) && Range == other.Range && Equals(Attributes, other.Attributes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Move)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Direction, Range, Attributes);
        }

        public bool HasAttribute(MoveAttrib attribute)
        {
            return Attributes.Contains(attribute);
        }
    }
}