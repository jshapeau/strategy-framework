using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;

/// <summary>
/// Represents a position on a grid; allows world position and grid position to vary independently.
/// </summary>
public struct GridPosition
{
    public int x;
    public int y;
    public int z;
    
    public static float xScale = 2; 
    public static float yScale = 1;
    public static float zScale = 2;

    public GridPosition(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static GridPosition Zero()
    {
        return new GridPosition(0,0,0);
    }

    public override bool Equals(object obj)
    {
        return obj is GridPosition position &&
            x == position.x &&
            y == position.y &&
            z == position.z;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{x},{y},{z}";
    }

    public string ToString(bool hideHeight)
    {
        return $"{x},{z}";
    }


    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return !(a == b);
    }

    public static GridPosition operator +(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x + b.x, a.y + b.y, a.z + b.z);   
    }

    public static GridPosition operator -(GridPosition a, GridPosition b)
    {
        return new GridPosition(a.x - b.x, a.y - b.y, a.z - b.z);   
    }


    public Vector3 AsVector3(){
        return(new Vector3(x, y, z));
    } 

    public Vector3 ToWorldPosition()
    {
        return(new Vector3(
            x * xScale,
            y * yScale,
            z * zScale)
        );
    }

}


