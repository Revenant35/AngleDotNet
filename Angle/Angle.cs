namespace Angle;

/// <summary>
/// Represents an angle, providing utility methods for trigonometric operations and conversions between degrees and radians.
/// </summary>
public class Angle : IComparable<Angle>, IEquatable<Angle>
{
    public static Angle Epsilon { get; } = FromRadians(1e-4);
    public static Angle Zero { get; } = new(0.0);


    #region Public Properties

    public double Radians { get; }
    public double Degrees => double.RadiansToDegrees(Radians);

    #endregion


    /// <summary>
    /// Initializes a new instance of the <see cref="Angle"/> class with the specified value in radians.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    private Angle(double radians)
    {
        Radians = radians;
    }


    #region Constructors

    /// <summary>
    /// Creates an <see cref="Angle"/> instance from a value in degrees.
    /// </summary>
    /// <param name="degrees">The angle in degrees.</param>
    /// <returns>An <see cref="Angle"/> instance representing the specified angle in degrees.</returns>
    public static Angle FromDegrees(double degrees) => new(double.DegreesToRadians(degrees));

    /// <summary>
    /// Creates an <see cref="Angle"/> instance from a value in radians.
    /// </summary>
    /// <param name="radians">The angle in radians.</param>
    /// <returns>An <see cref="Angle"/> instance representing the specified angle in radians.</returns>
    public static Angle FromRadians(double radians) => new(radians);

    #endregion


    #region Public Methods

    /// <summary>
    /// Normalizes the angle to a value between 0 and 2π radians (0 and 360 degrees).
    /// </summary>
    /// <returns>A new <see cref="Angle"/> instance representing the normalized angle.</returns>
    public Angle Normalize()
    {
        var normalizedRadians = Radians % double.Tau;
        if (normalizedRadians < 0)
            normalizedRadians += double.Tau;
        return new Angle(normalizedRadians);
    }

    /// <summary>
    /// Calculates the sine of the angle. Wraps the <see cref="Math.Sin"/> method.
    /// </summary>
    /// <returns>The sine of the angle.</returns>
    public double Sin() => double.Sin(Radians);

    /// <summary>
    /// Calculates the cosine of the angle. Wraps the <see cref="Math.Cos"/> method.
    /// </summary>
    /// <returns>The cosine of the angle.</returns>
    public double Cos() => double.Cos(Radians);

    /// <summary>
    /// Calculates the tangent of the angle. Wraps the <see cref="Math.Tan"/> method.
    /// </summary>
    /// <returns>The tangent of the angle.</returns>
    public double Tan() => double.Tan(Radians);

    /// <summary>
    /// Calculates the haversine of the angle, used in spherical distance calculations. https://en.wikipedia.org/wiki/Haversine_formula
    /// </summary>
    /// <returns>The haversine of the angle.</returns>
    public double Haversine() => Math.Pow(Math.Sin(Radians / 2), 2);

    #endregion


    #region Comparators

    public static bool operator >(Angle a, Angle b) => a.CompareTo(b) > 0;
    public static bool operator <(Angle a, Angle b) => a.CompareTo(b) < 0;
    public static bool operator >=(Angle a, Angle b) => a.CompareTo(b) > 0 || a.Equals(b);
    public static bool operator <=(Angle a, Angle b) => a.CompareTo(b) < 0 || a.Equals(b);
    public static bool operator ==(Angle a, Angle b) => a.Equals(b);
    public static bool operator !=(Angle a, Angle b) => !a.Equals(b);

    #endregion


    #region Operators

    public static Angle operator +(Angle a1, Angle a2) => new(a1.Radians + a2.Radians);
    public static Angle operator -(Angle a1, Angle a2) => new(a1.Radians - a2.Radians);
    public static Angle operator *(Angle angle, double scalar) => new(angle.Radians * scalar);
    public static Angle operator *(double scalar, Angle angle) => angle * scalar;
    public static Angle operator /(Angle angle, double scalar) => new(angle.Radians / scalar);
    public static Angle operator /(double scalar, Angle angle) => angle / scalar;

    #endregion


    #region IComparable<Angle>

    public int CompareTo(Angle? other) => other is null ? 1 : Radians.CompareTo(other.Radians);

    #endregion


    #region IEquatable<Angle>

    public bool Equals(Angle? other) => other is not null && Math.Abs(Radians - other.Radians) < Epsilon.Radians;
    public override bool Equals(object? obj) => obj is Angle other && Equals(other);
    public override int GetHashCode() => Radians.GetHashCode();

    #endregion


    public override string ToString() => $"{Degrees:G}°";
}