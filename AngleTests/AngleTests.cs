namespace AngleTests;

using Angle;
using Bogus;

[TestClass]
public class AngleTests
{
    private readonly Faker _faker = new();

    [TestMethod]
    public void FromDegrees_ShouldReturnCorrectRadians()
    {
        // Arrange
        var expected = _faker.Random.Double();
        
        // Act
        var angle = Angle.FromDegrees(expected);

        // Assert
        Assert.AreEqual(expected, angle.Degrees, Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void FromRadians_ShouldReturnCorrectRadians()
    {
        // Arrange
        var expected = _faker.Random.Double();

        // Act
        var angle = Angle.FromRadians(expected);

        // Assert
        Assert.AreEqual(expected, angle.Radians, Angle.Epsilon.Radians);
    }
    
    [TestMethod]
    [DataRow(450.0)]
    [DataRow(-270.0)]
    public void Normalize_ShouldReturnPositiveNormalizedAngle(double input)
    {
        // Arrange
        var angle = Angle.FromDegrees(input);
        var expected = input % 360;
        if (expected < 0) expected += 360;

        // Act
        var normalized = angle.Normalize();

        // Assert
        Assert.AreEqual(expected, normalized.Degrees, Angle.Epsilon.Degrees);
    }
    
    [TestMethod]
    public void Sin_ShouldReturnCorrectValue()
    {
        // Arrange & Act
        var angle = Angle.FromDegrees(30);

        // Assert
        Assert.AreEqual(0.5, angle.Sin(), Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Cos_ShouldReturnCorrectValue()
    {
        // Arrange & Act
        var angle = Angle.FromDegrees(60);

        // Assert
        Assert.AreEqual(0.5, angle.Cos(), Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Tan_ShouldReturnCorrectValue()
    {
        // Arrange & Act
        var angle = Angle.FromDegrees(45);

        // Assert
        Assert.AreEqual(1.0, angle.Tan(), Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Haversine_ShouldReturnCorrectValue()
    {
        // Arrange & Act
        var angle = Angle.FromDegrees(30);

        // Assert
        Assert.AreEqual(0.06699, angle.Haversine(), 0.00001);
    }

    [TestMethod]
    public void Addition_ShouldReturnCorrectSum()
    {
        // Arrange
        var angle1 = Angle.FromDegrees(30);
        var angle2 = Angle.FromDegrees(60);

        // Act
        var result = angle1 + angle2;

        // Assert
        Assert.AreEqual(90, result.Degrees, Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Subtraction_ShouldReturnCorrectDifference()
    {
        // Arrange
        var angle1 = Angle.FromDegrees(60);
        var angle2 = Angle.FromDegrees(30);

        // Act
        var result = angle1 - angle2;

        // Assert
        Assert.AreEqual(30, result.Degrees, Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Multiplication_ShouldReturnCorrectProduct()
    {
        // Arrange
        var angle = Angle.FromDegrees(45);

        // Act
        var result = angle * 2;

        // Assert
        Assert.AreEqual(90, result.Degrees, Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void Division_ShouldReturnCorrectQuotient()
    {
        // Arrange
        var angle = Angle.FromDegrees(90);

        // Act
        var result = angle / 2;

        // Assert
        Assert.AreEqual(45, result.Degrees, Angle.Epsilon.Degrees);
    }

    [TestMethod]
    public void GreaterThan_ShouldReturnTrueWhenFirstAngleIsLarger()
    {
        // Arrange
        var angle1 = Angle.FromDegrees(90);
        var angle2 = Angle.FromDegrees(45);

        // Assert
        Assert.IsTrue(angle1 > angle2);
    }

    [TestMethod]
    public void LessThan_ShouldReturnTrueWhenFirstAngleIsSmaller()
    {
        // Arrange
        var angle1 = Angle.FromDegrees(30);
        var angle2 = Angle.FromDegrees(45);

        // Assert
        Assert.IsTrue(angle1 < angle2);
    }

    [TestMethod]
    [DataRow(45, 30, DisplayName = "GreaterThan")]
    [DataRow(45, 45, DisplayName = "Equals")]
    public void GreaterThanOrEqual_ShouldReturnTrue_WhenAnglesAreEqualOrLarger(double a1, double a2)
    {
        // Arrange
        var angle1 = Angle.FromDegrees(a1);
        var angle2 = Angle.FromDegrees(a2);

        // Assert
        Assert.IsTrue(angle1 >= angle2);
    }

    [TestMethod]
    [DataRow(30, 45, DisplayName = "LessThan")]
    [DataRow(30, 30, DisplayName = "Equals")]
    public void LessThanOrEqual_ShouldReturnTrue_WhenAnglesAreEqualOrSmaller(double a1, double a2)
    {
        // Arrange
        var angle1 = Angle.FromDegrees(a1);
        var angle2 = Angle.FromDegrees(a2);

        // Assert
        Assert.IsTrue(angle1 <= angle2);
    }

    // Equality
    [TestMethod]
    public void Equality_ShouldReturnTrueForEqualAngles()
    {
        // Assert
        Assert.AreEqual(Angle.FromDegrees(90), Angle.FromDegrees(90));
        Assert.AreNotEqual(Angle.FromDegrees(90), Angle.FromDegrees(91));
    }

    [TestMethod]
    public void Inequality_ShouldReturnTrueForDifferentAngles()
    {
        // Arrange
        var angle1 = Angle.FromDegrees(90);
        var angle2 = angle1 + 2 * Angle.Epsilon;

        // Assert
        Assert.IsTrue(angle1 != angle2);
    }

    [TestMethod]
    public void GetHashCode_ShouldReturnUniqueHash()
    {
        // Assert
        Assert.AreEqual(Angle.FromDegrees(0).GetHashCode(), Angle.FromDegrees(0).GetHashCode());
        Assert.AreNotEqual(Angle.FromDegrees(360).GetHashCode(), Angle.FromDegrees(720).GetHashCode());
    }
}