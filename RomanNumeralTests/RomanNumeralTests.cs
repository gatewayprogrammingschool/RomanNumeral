using FluentAssertions;

using GPS.RomanNumerals;

using System;
using System.Collections.Immutable;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

using Xunit;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GPS.RomanNumerals.Tests;

public class RomanNumeralTests
{
    [Fact]
    public void Abs_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        RomanNumeral result = RomanNumeral.Abs(value);

        // Assert
        result.Should().Be(value);
    }

    [Fact]
    public void IsCanonical_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsCanonical(value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsComplexNumber_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsComplexNumber(value);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(1UL, false)]
    [InlineData(2UL, true)]
    public void IsEvenInteger_StateUnderTest_ExpectedBehavior(ulong v, bool expected)
    {

        // Arrange
        RomanNumeral value = new(v);

        // Act
        bool result = RomanNumeral.IsEvenInteger(
            value);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void IsFinite_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = default(RomanNumeral);

        // Act
        bool result = RomanNumeral.IsFinite(
            value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsImaginaryNumber_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = default(RomanNumeral);

        // Act
        bool result = RomanNumeral.IsImaginaryNumber(
            value);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsInfinity_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = default(RomanNumeral);

        // Act
        bool result = RomanNumeral.IsInfinity(
            value);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsInteger_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = default(RomanNumeral);

        // Act
        bool result = RomanNumeral.IsInteger(
            value);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1, true)]
    [InlineData(0, true)]
    [InlineData(1, false)]
    public void IsNaN_StateUnderTest_ExpectedBehavior(long v, bool expected)
    {
        // Arrange
        RomanNumeral value = new(v);

        // Act
        bool result = RomanNumeral.IsNaN(
            value);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(2, false)]
    public void IsNegative_StateUnderTest_ExpectedBehavior(long v, bool expected)
    {
        // Arrange
        RomanNumeral value = new(v);

        // Act
        bool result = RomanNumeral.IsNegative(
            value);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void IsNegativeInfinity_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(0);

        // Act
        bool result = RomanNumeral.IsNegativeInfinity(
            value);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void IsNormal_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(0);

        // Act
        bool result = RomanNumeral.IsNormal(
            value);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    public void IsOddInteger_StateUnderTest_ExpectedBehavior(long v, bool expected)
    {
        // Arrange
        RomanNumeral value = new(v);

        // Act
        bool result = RomanNumeral.IsOddInteger(
            value);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void IsPositive_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsPositive(
            value);

        // Assert
        result.Should().Be(true);
    }

    [Fact]
    public void IsPositiveInfinity_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsPositiveInfinity(
            value);

        // Assert
        result.Should().Be(false);
    }

    [Fact]
    public void IsRealNumber_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsRealNumber(
            value);

        // Assert
        result.Should().Be(true);
    }

    [Fact]
    public void IsSubnormal_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(1);

        // Act
        bool result = RomanNumeral.IsSubnormal(
            value);

        // Assert
        result.Should().Be(false);
    }

    [Fact]
    public void IsZero_StateUnderTest_ExpectedBehavior()
    {
        // Arrange
        RomanNumeral value = new(0);

        // Act
        bool result = RomanNumeral.IsZero(
            value);

        // Assert
        result.Should().Be(false);
    }

    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(2, 2, 2)]
    [InlineData(3, 2, 3)]
    public void MaxMagnitude_StateUnderTest_ExpectedBehavior(ulong f, ulong s, ulong max)
    {
        // Arrange
        RomanNumeral x = new(f);
        RomanNumeral y = new(s);
        RomanNumeral expected = new(max);

        // Act
        RomanNumeral result = RomanNumeral.MaxMagnitude(
            x,
            y);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 2)]
    [InlineData(3, 2, 2)]
    public void MaxMagnitudeNumber_StateUnderTest_ExpectedBehavior(ulong f, ulong s, ulong max)
    {
        // Arrange
        RomanNumeral x = new(f);
        RomanNumeral y = new(s);
        RomanNumeral expected = new(max);

        // Act
        RomanNumeral result = RomanNumeral.MaxMagnitudeNumber(
            x,
            y);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 2, 1)]
    [InlineData(2, 2, 2)]
    [InlineData(3, 2, 2)]
    public void MinMagnitude_StateUnderTest_ExpectedBehavior(ulong f, ulong s, ulong max)
    {
        // Arrange
        RomanNumeral x = new(f);
        RomanNumeral y = new(s);
        RomanNumeral expected = new(max);

        // Act
        RomanNumeral result = RomanNumeral.MinMagnitude(
            x,
            y);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(1, 2, 2)]
    [InlineData(2, 2, 2)]
    [InlineData(3, 2, 3)]
    public void MinMagnitudeNumber_StateUnderTest_ExpectedBehavior(ulong f, ulong s, ulong max)
    {
        // Arrange
        RomanNumeral x = new(f);
        RomanNumeral y = new(s);
        RomanNumeral expected = new(max);

        // Act
        RomanNumeral result = RomanNumeral.MinMagnitudeNumber(
            x,
            y);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", 1)]
    [InlineData("II", 2)]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("VI", 6)]
    [InlineData("VII", 7)]
    [InlineData("VIII", 8)]
    [InlineData("IX", 9)]
    [InlineData("X", 10)]
    [InlineData("XL", 40)]
    [InlineData("L", 50)]
    [InlineData("XC", 90)]
    [InlineData("C", 100)]
    [InlineData("CD", 400)]
    [InlineData("D", 500)]
    [InlineData("CM", 900)]
    [InlineData("M", 1000)]
    [InlineData("\u2160", 1)]
    [InlineData("\u2161", 2)]
    [InlineData("\u2162", 3)]
    [InlineData("\u2163", 4)]
    [InlineData("\u2164", 5)]
    [InlineData("\u2165", 6)]
    [InlineData("\u2166", 7)]
    [InlineData("\u2167", 8)]
    [InlineData("\u2168", 9)]
    [InlineData("\u2169", 10)]
    [InlineData("\u2169\u216C", 40)]
    [InlineData("\u216C", 50)]
    [InlineData("\u2169\u216D", 90)]
    [InlineData("\u216D", 100)]
    [InlineData("\u216D\u216E", 400)]
    [InlineData("\u216E", 500)]
    [InlineData("\u216D\u216F", 900)]
    [InlineData("\u216F", 1000)]
    public void Parse_StateUnderTest_ExpectedBehavior(string str, ulong e)
    {
        RomanNumeral expected = new(e);

        // Arrange
        ReadOnlySpan<char> s = str.ToImmutableArray().AsSpan();
        NumberStyles style = default(global::System.Globalization.NumberStyles);
        IFormatProvider? provider = null;

        // Act
        RomanNumeral result = RomanNumeral.Parse(
            s,
            style,
            provider);

        // Assert
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData("I", 1)]
    [InlineData("II", 2)]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("VI", 6)]
    [InlineData("VII", 7)]
    [InlineData("VIII", 8)]
    [InlineData("IX", 9)]
    [InlineData("X", 10)]
    [InlineData("XL", 40)]
    [InlineData("L", 50)]
    [InlineData("XC", 90)]
    [InlineData("C", 100)]
    [InlineData("CD", 400)]
    [InlineData("D", 500)]
    [InlineData("CM", 900)]
    [InlineData("M", 1000)]
    [InlineData("\u2160", 1)]
    [InlineData("\u2161", 2)]
    [InlineData("\u2162", 3)]
    [InlineData("\u2163", 4)]
    [InlineData("\u2164", 5)]
    [InlineData("\u2165", 6)]
    [InlineData("\u2166", 7)]
    [InlineData("\u2167", 8)]
    [InlineData("\u2168", 9)]
    [InlineData("\u2169", 10)]
    [InlineData("\u2169\u216C", 40)]
    [InlineData("\u216C", 50)]
    [InlineData("\u2169\u216D", 90)]
    [InlineData("\u216D", 100)]
    [InlineData("\u216D\u216E", 400)]
    [InlineData("\u216E", 500)]
    [InlineData("\u216D\u216F", 900)]
    [InlineData("\u216F", 1000)]
    public void TryParse_StateUnderTest_ExpectedBehavior(string str, ulong e)
    {
        RomanNumeral expected = new(e);

        // Arrange
        ReadOnlySpan<char> s = str.ToImmutableArray().AsSpan();
        NumberStyles style = default(global::System.Globalization.NumberStyles);
        IFormatProvider? provider = null;

        // Act
        bool result = RomanNumeral.TryParse(
            s, style, provider,
            out var value);

        // Assert
        result.Should().Be(true);
        value.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", "\u2160", 0)]
    [InlineData("II", "\u2161", 0)]
    [InlineData("III", "\u2162", 0)]
    [InlineData("IV", "\u2163", 0)]
    [InlineData("V", "\u2164", 0)]
    [InlineData("VI", "\u2165", 0)]
    [InlineData("VII", "\u2166", 0)]
    [InlineData("VIII", "\u2167", 0)]
    [InlineData("IX", "\u2168", 0)]
    [InlineData("X", "\u2169", 0)]
    [InlineData("XL", "\u2169\u216C", 0)]
    [InlineData("L", "\u216C", 0)]
    [InlineData("XC", "\u2169\u216D", 0)]
    [InlineData("C", "\u216D", 0)]
    [InlineData("CD", "\u216D\u216E", 0)]
    [InlineData("D", "\u216E", 0)]
    [InlineData("CM", "\u216D\u216F", 0)]
    [InlineData("M", "\u216F", 0)]
    [InlineData("II", "\u2160", 1)]
    [InlineData("III", "\u2160", 2)]
    [InlineData("IV", "\u2160", 3)]
    [InlineData("V", "\u2160", 4)]
    [InlineData("VI", "\u2160", 5)]
    [InlineData("VII", "\u2160", 6)]
    [InlineData("VIII", "\u2160", 7)]
    [InlineData("IX", "\u2160", 8)]
    [InlineData("X", "\u2160", 9)]
    [InlineData("XL", "\u2160", 39)]
    [InlineData("L", "\u2160", 49)]
    [InlineData("XC", "\u2160", 89)]
    [InlineData("C", "\u2160", 99)]
    [InlineData("CD", "\u2160", 399)]
    [InlineData("D", "\u2160", 499)]
    [InlineData("CM", "\u2160", 899)]
    [InlineData("M", "\u2160", 999)]
    [InlineData("I", "\u216F", -999)]
    [InlineData("II", "\u216F", -998)]
    [InlineData("III", "\u216F", -997)]
    [InlineData("IV", "\u216F", -996)]
    [InlineData("V", "\u216F", -995)]
    [InlineData("VI", "\u216F", -994)]
    [InlineData("VII", "\u216F", -993)]
    [InlineData("VIII", "\u216F", -992)]
    [InlineData("IX", "\u216F", -991)]
    [InlineData("X", "\u216F", -990)]
    [InlineData("XL", "\u216F", -960)]
    [InlineData("L", "\u216F", -950)]
    [InlineData("XC", "\u216F", -910)]
    [InlineData("C", "\u216F", -900)]
    [InlineData("CD", "\u216F", -600)]
    [InlineData("D", "\u216F", -500)]
    [InlineData("CM", "\u216F", -100)]
    public void CompareTo_StateUnderTest_ExpectedBehavior(string l, string r, int expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left.CompareTo(right);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", "\u2160", true)]
    [InlineData("II", "\u2161", true)]
    [InlineData("III", "\u2162", true)]
    [InlineData("IV", "\u2163", true)]
    [InlineData("V", "\u2164", true)]
    [InlineData("VI", "\u2165", true)]
    [InlineData("VII", "\u2166", true)]
    [InlineData("VIII", "\u2167", true)]
    [InlineData("IX", "\u2168", true)]
    [InlineData("X", "\u2169", true)]
    [InlineData("XL", "\u2169\u216C", true)]
    [InlineData("L", "\u216C", true)]
    [InlineData("XC", "\u2169\u216D", true)]
    [InlineData("C", "\u216D", true)]
    [InlineData("CD", "\u216D\u216E", true)]
    [InlineData("D", "\u216E", true)]
    [InlineData("CM", "\u216D\u216F", true)]
    [InlineData("M", "\u216F", true)]
    [InlineData("II", "\u2160", false)]
    [InlineData("III", "\u2160", false)]
    [InlineData("IV", "\u2160", false)]
    [InlineData("V", "\u2160", false)]
    [InlineData("VI", "\u2160", false)]
    [InlineData("VII", "\u2160", false)]
    [InlineData("VIII", "\u2160", false)]
    [InlineData("IX", "\u2160", false)]
    [InlineData("X", "\u2160", false)]
    [InlineData("XL", "\u2160", false)]
    [InlineData("L", "\u2160", false)]
    [InlineData("XC", "\u2160", false)]
    [InlineData("C", "\u2160", false)]
    [InlineData("CD", "\u2160", false)]
    [InlineData("D", "\u2160", false)]
    [InlineData("CM", "\u2160", false)]
    [InlineData("M", "\u2160", false)]
    [InlineData("I", "\u216F", false)]
    [InlineData("II", "\u216F", false)]
    [InlineData("III", "\u216F", false)]
    [InlineData("IV", "\u216F", false)]
    [InlineData("V", "\u216F", false)]
    [InlineData("VI", "\u216F", false)]
    [InlineData("VII", "\u216F", false)]
    [InlineData("VIII", "\u216F", false)]
    [InlineData("IX", "\u216F", false)]
    [InlineData("X", "\u216F", false)]
    [InlineData("XL", "\u216F", false)]
    [InlineData("L", "\u216F", false)]
    [InlineData("XC", "\u216F", false)]
    [InlineData("C", "\u216F", false)]
    [InlineData("CD", "\u216F", false)]
    [InlineData("D", "\u216F", false)]
    [InlineData("CM", "\u216F", false)]
    public void Equals_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left.Equals(right);

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("\u2160", 1)]
    [InlineData("\u2161", 2)]
    [InlineData("\u2162", 3)]
    [InlineData("\u2163", 4)]
    [InlineData("\u2164", 5)]
    [InlineData("\u2165", 6)]
    [InlineData("\u2166", 7)]
    [InlineData("\u2167", 8)]
    [InlineData("\u2168", 9)]
    [InlineData("\u2169", 10)]
    [InlineData("\u2169\u216C", 40)]
    [InlineData("\u216C", 50)]
    [InlineData("\u2169\u216D", 90)]
    [InlineData("\u216D", 100)]
    [InlineData("\u216D\u216E", 400)]
    [InlineData("\u216E", 500)]
    [InlineData("\u216D\u216F", 900)]
    [InlineData("\u216F", 1000)]
    public void ToString_StateUnderTest_ExpectedBehavior(string expected, ulong value)
    {
        // Arrange
        RomanNumeral RomanNumeral = new(value);
        string? format = null;
        IFormatProvider? formatProvider = null;

        // Act
        string result = RomanNumeral.ToString(
            format,
            formatProvider);

        // Assert
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData("I", 1)]
    [InlineData("II", 2)]
    [InlineData("III", 3)]
    [InlineData("IV", 4)]
    [InlineData("V", 5)]
    [InlineData("VI", 6)]
    [InlineData("VII", 7)]
    [InlineData("VIII", 8)]
    [InlineData("IX", 9)]
    [InlineData("X", 10)]
    [InlineData("XL", 40)]
    [InlineData("L", 50)]
    [InlineData("XC", 90)]
    [InlineData("C", 100)]
    [InlineData("CD", 400)]
    [InlineData("D", 500)]
    [InlineData("CM", 900)]
    [InlineData("M", 1000)]
    [InlineData("\u2160", 1)]
    [InlineData("\u2161", 2)]
    [InlineData("\u2162", 3)]
    [InlineData("\u2163", 4)]
    [InlineData("\u2164", 5)]
    [InlineData("\u2165", 6)]
    [InlineData("\u2166", 7)]
    [InlineData("\u2167", 8)]
    [InlineData("\u2168", 9)]
    [InlineData("\u2169", 10)]
    [InlineData("\u2169\u216C", 40)]
    [InlineData("\u216C", 50)]
    [InlineData("\u2169\u216D", 90)]
    [InlineData("\u216D", 100)]
    [InlineData("\u216D\u216E", 400)]
    [InlineData("\u216E", 500)]
    [InlineData("\u216D\u216F", 900)]
    [InlineData("\u216F", 1000)]
    public void GetHashCode_StateUnderTest_ExpectedBehavior(string s, ulong value)
    {
        // Arrange
        RomanNumeral RomanNumeral = new(value);

        // Act
        int result = RomanNumeral.GetHashCode();

        // Assert
        result.Should().Be(value.GetHashCode());
    }

    [Theory]
    [InlineData("I", "\u2160", true)]
    [InlineData("II", "\u2161", true)]
    [InlineData("III", "\u2162", true)]
    [InlineData("IV", "\u2163", true)]
    [InlineData("V", "\u2164", true)]
    [InlineData("VI", "\u2165", true)]
    [InlineData("VII", "\u2166", true)]
    [InlineData("VIII", "\u2167", true)]
    [InlineData("IX", "\u2168", true)]
    [InlineData("X", "\u2169", true)]
    [InlineData("XL", "\u2169\u216C", true)]
    [InlineData("L", "\u216C", true)]
    [InlineData("XC", "\u2169\u216D", true)]
    [InlineData("C", "\u216D", true)]
    [InlineData("CD", "\u216D\u216E", true)]
    [InlineData("D", "\u216E", true)]
    [InlineData("CM", "\u216D\u216F", true)]
    [InlineData("M", "\u216F", true)]
    [InlineData("II", "\u2160", false)]
    [InlineData("III", "\u2160", false)]
    [InlineData("IV", "\u2160", false)]
    [InlineData("V", "\u2160", false)]
    [InlineData("VI", "\u2160", false)]
    [InlineData("VII", "\u2160", false)]
    [InlineData("VIII", "\u2160", false)]
    [InlineData("IX", "\u2160", false)]
    [InlineData("X", "\u2160", false)]
    [InlineData("XL", "\u2160", false)]
    [InlineData("L", "\u2160", false)]
    [InlineData("XC", "\u2160", false)]
    [InlineData("C", "\u2160", false)]
    [InlineData("CD", "\u2160", false)]
    [InlineData("D", "\u2160", false)]
    [InlineData("CM", "\u2160", false)]
    [InlineData("M", "\u2160", false)]
    [InlineData("I", "\u216F", false)]
    [InlineData("II", "\u216F", false)]
    [InlineData("III", "\u216F", false)]
    [InlineData("IV", "\u216F", false)]
    [InlineData("V", "\u216F", false)]
    [InlineData("VI", "\u216F", false)]
    [InlineData("VII", "\u216F", false)]
    [InlineData("VIII", "\u216F", false)]
    [InlineData("IX", "\u216F", false)]
    [InlineData("X", "\u216F", false)]
    [InlineData("XL", "\u216F", false)]
    [InlineData("L", "\u216F", false)]
    [InlineData("XC", "\u216F", false)]
    [InlineData("C", "\u216F", false)]
    [InlineData("CD", "\u216F", false)]
    [InlineData("D", "\u216F", false)]
    [InlineData("CM", "\u216F", false)]
    public void OperatorEquals_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left == right;

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", "\u2160", false)]
    [InlineData("II", "\u2161", false)]
    [InlineData("III", "\u2162", false)]
    [InlineData("IV", "\u2163", false)]
    [InlineData("V", "\u2164", false)]
    [InlineData("VI", "\u2165", false)]
    [InlineData("VII", "\u2166", false)]
    [InlineData("VIII", "\u2167", false)]
    [InlineData("IX", "\u2168", false)]
    [InlineData("X", "\u2169", false)]
    [InlineData("XL", "\u2169\u216C", false)]
    [InlineData("L", "\u216C", false)]
    [InlineData("XC", "\u2169\u216D", false)]
    [InlineData("C", "\u216D", false)]
    [InlineData("CD", "\u216D\u216E", false)]
    [InlineData("D", "\u216E", false)]
    [InlineData("CM", "\u216D\u216F", false)]
    [InlineData("M", "\u216F", false)]
    [InlineData("II", "\u2160", true)]
    [InlineData("III", "\u2160", true)]
    [InlineData("IV", "\u2160", true)]
    [InlineData("V", "\u2160", true)]
    [InlineData("VI", "\u2160", true)]
    [InlineData("VII", "\u2160", true)]
    [InlineData("VIII", "\u2160", true)]
    [InlineData("IX", "\u2160", true)]
    [InlineData("X", "\u2160", true)]
    [InlineData("XL", "\u2160", true)]
    [InlineData("L", "\u2160", true)]
    [InlineData("XC", "\u2160", true)]
    [InlineData("C", "\u2160", true)]
    [InlineData("CD", "\u2160", true)]
    [InlineData("D", "\u2160", true)]
    [InlineData("CM", "\u2160", true)]
    [InlineData("M", "\u2160", true)]
    [InlineData("I", "\u216F", false)]
    [InlineData("II", "\u216F", false)]
    [InlineData("III", "\u216F", false)]
    [InlineData("IV", "\u216F", false)]
    [InlineData("V", "\u216F", false)]
    [InlineData("VI", "\u216F", false)]
    [InlineData("VII", "\u216F", false)]
    [InlineData("VIII", "\u216F", false)]
    [InlineData("IX", "\u216F", false)]
    [InlineData("X", "\u216F", false)]
    [InlineData("XL", "\u216F", false)]
    [InlineData("L", "\u216F", false)]
    [InlineData("XC", "\u216F", false)]
    [InlineData("C", "\u216F", false)]
    [InlineData("CD", "\u216F", false)]
    [InlineData("D", "\u216F", false)]
    [InlineData("CM", "\u216F", false)]
    public void OperatorGreaterThan_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left > right;

        // Assert
        result.Should().Be(expected);
    }


    [Theory]
    [InlineData("I", "\u2160", true)]
    [InlineData("II", "\u2161", true)]
    [InlineData("III", "\u2162", true)]
    [InlineData("IV", "\u2163", true)]
    [InlineData("V", "\u2164", true)]
    [InlineData("VI", "\u2165", true)]
    [InlineData("VII", "\u2166", true)]
    [InlineData("VIII", "\u2167", true)]
    [InlineData("IX", "\u2168", true)]
    [InlineData("X", "\u2169", true)]
    [InlineData("XL", "\u2169\u216C", true)]
    [InlineData("L", "\u216C", true)]
    [InlineData("XC", "\u2169\u216D", true)]
    [InlineData("C", "\u216D", true)]
    [InlineData("CD", "\u216D\u216E", true)]
    [InlineData("D", "\u216E", true)]
    [InlineData("CM", "\u216D\u216F", true)]
    [InlineData("M", "\u216F", true)]
    [InlineData("II", "\u2160", true)]
    [InlineData("III", "\u2160", true)]
    [InlineData("IV", "\u2160", true)]
    [InlineData("V", "\u2160", true)]
    [InlineData("VI", "\u2160", true)]
    [InlineData("VII", "\u2160", true)]
    [InlineData("VIII", "\u2160", true)]
    [InlineData("IX", "\u2160", true)]
    [InlineData("X", "\u2160", true)]
    [InlineData("XL", "\u2160", true)]
    [InlineData("L", "\u2160", true)]
    [InlineData("XC", "\u2160", true)]
    [InlineData("C", "\u2160", true)]
    [InlineData("CD", "\u2160", true)]
    [InlineData("D", "\u2160", true)]
    [InlineData("CM", "\u2160", true)]
    [InlineData("M", "\u2160", true)]
    [InlineData("I", "\u216F", false)]
    [InlineData("II", "\u216F", false)]
    [InlineData("III", "\u216F", false)]
    [InlineData("IV", "\u216F", false)]
    [InlineData("V", "\u216F", false)]
    [InlineData("VI", "\u216F", false)]
    [InlineData("VII", "\u216F", false)]
    [InlineData("VIII", "\u216F", false)]
    [InlineData("IX", "\u216F", false)]
    [InlineData("X", "\u216F", false)]
    [InlineData("XL", "\u216F", false)]
    [InlineData("L", "\u216F", false)]
    [InlineData("XC", "\u216F", false)]
    [InlineData("C", "\u216F", false)]
    [InlineData("CD", "\u216F", false)]
    [InlineData("D", "\u216F", false)]
    [InlineData("CM", "\u216F", false)]
    public void OperatorGreaterOrEqualsThan_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left >= right;

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", "\u2160", false)]
    [InlineData("II", "\u2161", false)]
    [InlineData("III", "\u2162", false)]
    [InlineData("IV", "\u2163", false)]
    [InlineData("V", "\u2164", false)]
    [InlineData("VI", "\u2165", false)]
    [InlineData("VII", "\u2166", false)]
    [InlineData("VIII", "\u2167", false)]
    [InlineData("IX", "\u2168", false)]
    [InlineData("X", "\u2169", false)]
    [InlineData("XL", "\u2169\u216C", false)]
    [InlineData("L", "\u216C", false)]
    [InlineData("XC", "\u2169\u216D", false)]
    [InlineData("C", "\u216D", false)]
    [InlineData("CD", "\u216D\u216E", false)]
    [InlineData("D", "\u216E", false)]
    [InlineData("CM", "\u216D\u216F", false)]
    [InlineData("M", "\u216F", false)]
    [InlineData("II", "\u2160", false)]
    [InlineData("III", "\u2160", false)]
    [InlineData("IV", "\u2160", false)]
    [InlineData("V", "\u2160", false)]
    [InlineData("VI", "\u2160", false)]
    [InlineData("VII", "\u2160", false)]
    [InlineData("VIII", "\u2160", false)]
    [InlineData("IX", "\u2160", false)]
    [InlineData("X", "\u2160", false)]
    [InlineData("XL", "\u2160", false)]
    [InlineData("L", "\u2160", false)]
    [InlineData("XC", "\u2160", false)]
    [InlineData("C", "\u2160", false)]
    [InlineData("CD", "\u2160", false)]
    [InlineData("D", "\u2160", false)]
    [InlineData("CM", "\u2160", false)]
    [InlineData("M", "\u2160", false)]
    [InlineData("I", "\u216F", true)]
    [InlineData("II", "\u216F", true)]
    [InlineData("III", "\u216F", true)]
    [InlineData("IV", "\u216F", true)]
    [InlineData("V", "\u216F", true)]
    [InlineData("VI", "\u216F", true)]
    [InlineData("VII", "\u216F", true)]
    [InlineData("VIII", "\u216F", true)]
    [InlineData("IX", "\u216F", true)]
    [InlineData("X", "\u216F", true)]
    [InlineData("XL", "\u216F", true)]
    [InlineData("L", "\u216F", true)]
    [InlineData("XC", "\u216F", true)]
    [InlineData("C", "\u216F", true)]
    [InlineData("CD", "\u216F", true)]
    [InlineData("D", "\u216F", true)]
    [InlineData("CM", "\u216F", true)]
    public void OperatorLessThan_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left < right;

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("I", "\u2160", true)]
    [InlineData("II", "\u2161", true)]
    [InlineData("III", "\u2162", true)]
    [InlineData("IV", "\u2163", true)]
    [InlineData("V", "\u2164", true)]
    [InlineData("VI", "\u2165", true)]
    [InlineData("VII", "\u2166", true)]
    [InlineData("VIII", "\u2167", true)]
    [InlineData("IX", "\u2168", true)]
    [InlineData("X", "\u2169", true)]
    [InlineData("XL", "\u2169\u216C", true)]
    [InlineData("L", "\u216C", true)]
    [InlineData("XC", "\u2169\u216D", true)]
    [InlineData("C", "\u216D", true)]
    [InlineData("CD", "\u216D\u216E", true)]
    [InlineData("D", "\u216E", true)]
    [InlineData("CM", "\u216D\u216F", true)]
    [InlineData("M", "\u216F", true)]
    [InlineData("II", "\u2160", false)]
    [InlineData("III", "\u2160", false)]
    [InlineData("IV", "\u2160", false)]
    [InlineData("V", "\u2160", false)]
    [InlineData("VI", "\u2160", false)]
    [InlineData("VII", "\u2160", false)]
    [InlineData("VIII", "\u2160", false)]
    [InlineData("IX", "\u2160", false)]
    [InlineData("X", "\u2160", false)]
    [InlineData("XL", "\u2160", false)]
    [InlineData("L", "\u2160", false)]
    [InlineData("XC", "\u2160", false)]
    [InlineData("C", "\u2160", false)]
    [InlineData("CD", "\u2160", false)]
    [InlineData("D", "\u2160", false)]
    [InlineData("CM", "\u2160", false)]
    [InlineData("M", "\u2160", false)]
    [InlineData("I", "\u216F", true)]
    [InlineData("II", "\u216F", true)]
    [InlineData("III", "\u216F", true)]
    [InlineData("IV", "\u216F", true)]
    [InlineData("V", "\u216F", true)]
    [InlineData("VI", "\u216F", true)]
    [InlineData("VII", "\u216F", true)]
    [InlineData("VIII", "\u216F", true)]
    [InlineData("IX", "\u216F", true)]
    [InlineData("X", "\u216F", true)]
    [InlineData("XL", "\u216F", true)]
    [InlineData("L", "\u216F", true)]
    [InlineData("XC", "\u216F", true)]
    [InlineData("C", "\u216F", true)]
    [InlineData("CD", "\u216F", true)]
    [InlineData("D", "\u216F", true)]
    [InlineData("CM", "\u216F", true)]
    public void OperatorLessOrEqualsThan_StateUnderTest_ExpectedBehavior(string l, string r, bool expected)
    {
        // Arrange
        RomanNumeral left = new(l);
        RomanNumeral right = new(r);

        // Act
        var result = left <= right;

        // Assert
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("MIII", 1003)]
    public void ImplicitStringTo_StateUnderTest_ExpectedBehavior(string v, ulong e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216F\u2162", 1003)]
    public void ImplicitStringFrom_StateUnderTest_ExpectedBehavior(string v, ulong e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CCLV", 255)]
    public void ImplicitByteTo_StateUnderTest_ExpectedBehavior(string v, byte e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u216D\u216C\u2164", 255)]
    public void ImplicitByteFrom_StateUnderTest_ExpectedBehavior(string v, byte e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (sbyte)127)]
    public void ImplicitSByteTo_StateUnderTest_ExpectedBehavior(string v, sbyte e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (sbyte)127)]
    public void ImplicitSByteFrom_StateUnderTest_ExpectedBehavior(string v, sbyte e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (short)127)]
    public void ImplicitShortTo_StateUnderTest_ExpectedBehavior(string v, short e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (short)127)]
    public void ImplicitShortFrom_StateUnderTest_ExpectedBehavior(string v, short e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (ushort)127)]
    public void ImplicitUShortTo_StateUnderTest_ExpectedBehavior(string v, ushort e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (ushort)127)]
    public void ImplicitUShortFrom_StateUnderTest_ExpectedBehavior(string v, ushort e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (int)127)]
    public void ImplicitIntTo_StateUnderTest_ExpectedBehavior(string v, int e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (int)127)]
    public void ImplicitIntFrom_StateUnderTest_ExpectedBehavior(string v, int e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (uint)127)]
    public void ImplicitUIntTo_StateUnderTest_ExpectedBehavior(string v, uint e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (uint)127)]
    public void ImplicitUIntFrom_StateUnderTest_ExpectedBehavior(string v, uint e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);

    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (long)127)]
    public void ImplicitLongTo_StateUnderTest_ExpectedBehavior(string v, long e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (long)127)]
    public void ImplicitLongFrom_StateUnderTest_ExpectedBehavior(string v, long e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (ulong)127)]
    public void ImplicitULongTo_StateUnderTest_ExpectedBehavior(string v, ulong e)
    {
        RomanNumeral expected = new(e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (ulong)127)]
    public void ImplicitULongFrom_StateUnderTest_ExpectedBehavior(string v, ulong e)
    {
        RomanNumeral expected = new(e);

        v.Should().Be(expected);

    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (float)127)]
    public void ImplicitFloatTo_StateUnderTest_ExpectedBehavior(string v, float e)
    {
        RomanNumeral expected = new((long)e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (float)127)]
    public void ImplicitFloatFrom_StateUnderTest_ExpectedBehavior(string v, float e)
    {
        RomanNumeral expected = new((long)e);

        v.Should().Be(expected);
    }

    [Theory]
    [InlineData("III", 3)]
    [InlineData("CXXVII", (double)127)]
    public void ImplicitDoubleTo_StateUnderTest_ExpectedBehavior(string v, double e)
    {
        RomanNumeral expected = new((ulong)e);

        expected.Should().Be(v);
    }

    [Theory]
    [InlineData("\u2162", 3)]
    [InlineData("\u216D\u2169\u2169\u2166", (double)127)]
    public void ImplicitDoubleFrom_StateUnderTest_ExpectedBehavior(string v, double e)
    {
        RomanNumeral expected = new((ulong)e);

        v.Should().Be(expected);

    }

    [Theory]
    [MemberData(nameof(GetValues))]
    public void ExtensiveParsing_ExpectedBehavior((ulong value, string sValue) pair)
    {
        RomanNumeral left = new(pair.value);
        RomanNumeral right = new(pair.sValue);

        left.Should().Be(right);
        left.Should().Be(pair.value);
        left.Should().Be(pair.sValue);

        right.Should().Be(left);
        right.Should().Be(pair.value);
        right.Should().Be(pair.sValue);
    }

    public static IEnumerable<object[]> GetValues()
    {
        foreach((ulong, string) item in new (ulong, string)[] {
            (1, "I"),
            (2, "II"),
            (3, "III"),
            (4, "IV"),
            (5, "V"),
            (6, "VI"),
            (7, "VII"),
            (8, "VIII"),
            (9, "IX"),
            (10, "X"),
            (11, "XI"),
            (12, "XII"),
            (13, "XIII"),
            (14, "XIV"),
            (15, "XV"),
            (16, "XVI"),
            (17, "XVII"),
            (18, "XVIII"),
            (19, "XIX"),
            (20, "XX"),
            (21, "XXI"),
            (22, "XXII"),
            (23, "XXIII"),
            (24, "XXIV"),
            (25, "XXV"),
            (30, "XXX"),
            (35, "XXXV"),
            (40, "XL"),
            (45, "XLV"),
            (50, "L"),
            (55, "LV"),
            (60, "LX"),
            (65, "LXV"),
            (70, "LXX"),
            (75, "LXXV"),
            (80, "LXXX"),
            (85, "LXXXV"),
            (90, "XC"),
            (95, "XCV"),
            (100, "C"),
            (105, "CV"),
            (110, "CX"),
            (115, "CXV"),
            (120, "CXX"),
            (125, "CXXV"),
            (130, "CXXX"),
            (135, "CXXXV"),
            (140, "CXL"),
            (150, "CL"),
            (175, "CLXXV"),
            (200, "CC"),
            (225, "CCXXV"),
            (250, "CCL"),
            (275, "CCLXXV"),
            (300, "CCC"),
            (325, "CCCXXV"),
            (350, "CCCL"),
            (375, "CCCLXXV"),
            (400, "CD"),
            (425, "CDXXV"),
            (450, "CDL"),
            (475, "CDLXXV"),
            (500, "D"),
            (525, "DXXV"),
            (550, "DL"),
            (575, "DLXXV"),
            (600, "DC"),
            (625, "DCXXV"),
            (650, "DCL"),
            (675, "DCLXXV"),
            (700, "DCC"),
            (750, "DCCL"),
            (825, "DCCCXXV"),
            (900, "CM"),
            (975, "CMLXXV"),
            (1050, "ML"),
            (1125, "MCXXV"),
            (1200, "MCC"),
            (1275, "MCCLXXV"),
            (1350, "MCCCL"),
            (1425, "MCDXXV"),
            (1500, "MD"),
            (1575, "MDLXXV"),
            (1650, "MDCL"),
            (1725, "MDCCXXV"),
            (1800, "MDCCC"),
            (1875, "MDCCCLXXV"),
            (1950, "MCML"),
            (2025, "MMXXV"),
            (2100, "MMC"),
            (2175, "MMCLXXV"),
            (2250, "MMCCL"),
            (2325, "MMCCCXXV"),
            (2400, "MMCD"),
            (2475, "MMCDLXXV"),
            (2550, "MMDL") })
        {
            yield return new object[] { item };
        }
    }
}
