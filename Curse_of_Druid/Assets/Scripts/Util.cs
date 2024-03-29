using UnityEngine;

public enum TILE_ID
{
    Debug,
    Basic,
    Passing,
    Cracked,
    Destroyed,
    Brick,
    Vine
}

public enum ITEM_ID
{
    Debug,
    Pickaxe,
    Machete,
    Torch
}

[System.Serializable]
public struct Coordinate
{
    [SerializeField]
    private int x;
    [SerializeField]
    private int y;
    public int X => x;
    public int Y => y;

    public static Coordinate zero = new Coordinate(0, 0);

    public Coordinate(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public override int GetHashCode() => x ^ y;

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (GetType() != obj.GetType())
            return false;

        Coordinate point = (Coordinate)obj;

        if (x != point.x)
            return false;

        return y == point.y;
    }

    public static bool operator ==(Coordinate c1, Coordinate c2) => Object.Equals(c1, c2);

    public static bool operator !=(Coordinate c1, Coordinate c2) => !Object.Equals(c1, c2);

    public static Coordinate operator +(Coordinate c1, Coordinate c2) => new Coordinate(c1.X + c2.X, c1.Y + c2.Y);

    public static Coordinate operator -(Coordinate c1, Coordinate c2) => new Coordinate(c1.X - c2.X, c1.Y - c2.Y);

    public static int Distance(Coordinate c1, Coordinate c2) => Mathf.Abs(c1.X - c2.X) + Mathf.Abs(c1.Y - c2.Y);

    /// <summary>
    /// Change transition.position to Coordinate
    /// </summary>
    public static Coordinate WorldPointToCoordinate(Vector3 point)
    {
        return new Coordinate(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y));
    }

    /// <summary>
    /// Change Coordinate to transition.position
    /// </summary>
    public static Vector2 CoordinatetoWorldPoint(Coordinate coor)
    {
        return new Vector2(coor.X + 0.5f, coor.Y + 0.5f);
    }
}