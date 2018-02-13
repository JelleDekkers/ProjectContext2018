using System;
using UnityEngine;

[Serializable]
public struct IntVector2 {

    public int x, z;

    /// <summary>
    /// Shorthand for writing (0, 0)
    /// </summary>
    public static IntVector2 Zero = new IntVector2(0, 0);

    public IntVector2(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public IntVector2(IntVector2 intVector) {
        x = intVector.x;
        z = intVector.z;
    }

    public override string ToString() {
        return "(" + x.ToString() + ", " + z.ToString() + ")";
    }

    public static float Distance(IntVector2 a, IntVector2 b) {
        return Math.Abs(b.x - a.x) + Math.Abs(b.z - a.z);
    }

    public static IntVector2 operator +(IntVector2 firstCoordinates, IntVector2 secondCoordinates) {
        firstCoordinates.x += secondCoordinates.x;
        firstCoordinates.z += secondCoordinates.z;
        return firstCoordinates;
    }

    public static IntVector2 operator -(IntVector2 firstCoordinates, IntVector2 secondCoordinates) {
        firstCoordinates.x -= Math.Abs(secondCoordinates.x);
        firstCoordinates.z -= Math.Abs(secondCoordinates.z);
        return firstCoordinates;
    }

    public static IntVector2 operator *(IntVector2 firstCoordinates, float f) {
        firstCoordinates.x *= (int)f;
        firstCoordinates.z *= (int)f;
        return firstCoordinates;
    }

    public static IntVector2 operator /(IntVector2 firstCoordinates, float f) {
        firstCoordinates.x /= (int)f;
        firstCoordinates.z /= (int)f;
        return firstCoordinates;
    }

    public static bool operator ==(IntVector2 firstCoordinates, IntVector2 secondCoordinate) {
        return (firstCoordinates.x == secondCoordinate.x) && (firstCoordinates.z == secondCoordinate.z);
    }

    public static bool operator !=(IntVector2 firstCoordinates, IntVector2 secondCoordinate) {
        return (firstCoordinates.x != secondCoordinate.x) || (firstCoordinates.z != secondCoordinate.z);
    }

    public override bool Equals(object obj) {
        return base.Equals(obj);
        //return ((IntVector2)obj).x == x && ((IntVector2)obj).z == z;
    }

    public override int GetHashCode() {
        unchecked {
            int hash = 17;
            hash = hash * 23 + x.GetHashCode();
            hash = hash * 23 + z.GetHashCode();
            return hash;
        }
    }

    public static explicit operator IntVector2(Vector3 pos) {
        return new IntVector2((int)pos.x, (int)pos.z);
    }
}