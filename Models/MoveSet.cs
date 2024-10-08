using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("RogueGambit.Tests")]

namespace RogueGambit.Models;

public class MoveSet
{
    public readonly HashSet<Move> Moves = new();
    public readonly string Name = string.Empty;

    public MoveSet()
    {
    }

    public MoveSet(string name)
    {
        Name = name;
    }

    public MoveSet(string name, HashSet<Move> moves) : this(name)
    {
        foreach (var move in moves) Moves.Add(move);
    }


    public bool HasChainedMoves { get; internal set; }

    public void AddMove(Vector2 direction, int range, (int, int) chain = default, params MoveAttrib[] attributes)
    {
        Moves.Add(new Move(direction, range, chain, [..attributes]));
        if (chain != default) HasChainedMoves = true;
    }

    public bool RemoveMove(Vector2 direction, int range)
    {
        return Moves.RemoveWhere(m => m.Direction == direction && m.Range == range) > 0;
    }

    public IEnumerable<Move> GetMovesWithAttributes(params MoveAttrib[] attributes)
    {
        return Moves.Where(m => attributes.All(a => m.Attributes.Contains(a)));
    }

    public MoveSet Clone()
    {
        var clone = new MoveSet();
        foreach (var move in Moves) clone.AddMove(move.Direction, move.Range, move.Chain, move.Attributes.ToArray());
        return clone;
    }


    public class Move : IEquatable<Move>
    {
        public Move()
        {
        }

        public Move(Vector2 direction, int range, (int, int) chain, HashSet<MoveAttrib> attributes)
        {
            Direction = direction;
            Range = range;
            Chain = chain;
            IsChained = chain != default;
            Attributes = attributes;
        }

        public Vector2 Direction { get; init; }
        public int Range { get; init; }
        public (int, int) Chain { get; init; }
        public bool IsChained { get; init; }
        public HashSet<MoveAttrib> Attributes { get; init; }


        public bool Equals(Move other)
        {
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Direction.Equals(other.Direction) && Range == other.Range && Equals(Attributes, other.Attributes) &&
                   Chain.Equals(other.Chain);
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