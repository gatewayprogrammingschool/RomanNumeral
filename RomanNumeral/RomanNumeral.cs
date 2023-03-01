using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace GPS.RomanNumerals;

public struct RomanNumeral : INumber<RomanNumeral>
{
    private const string NAN = "Roman numerals do not support zero.";

    private readonly static ulong[] _values = new ulong[]
        {
            1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
            40, 50, 90, 100, 400, 500, 900, 1000
        };

    private readonly static char[][] _chars = {
        new char[] { (char)0x2160, },
        new char[] { (char)0x2161, },
        new char[] { (char)0x2162, },
        new char[] { (char)0x2163, },
        new char[] { (char)0x2164, },
        new char[] { (char)0x2165, },
        new char[] { (char)0x2166, },
        new char[] { (char)0x2167, },
        new char[] { (char)0x2168, },
        new char[] { (char)0x2169, }, // X
        new char[] { (char)0x2169, (char)0x216C,  }, // XL 40
        new char[] { (char)0x216C, }, // L 50
        new char[] { (char)0x2169, (char)0x216D,  }, // XC 90
        new char[] { (char)0x216D, }, // C 100
        new char[] { (char)0x216D, (char)0x216E,  }, // CD 400
        new char[] { (char)0x216E, }, // D 500
        new char[] { (char)0x216D, (char)0x216F,  }, // CM 900
        new char[] { (char)0x216F, }, // M 1000
    };

    private readonly static Dictionary<ulong, string> _romanNumerals = new()
    {
        {1000,"M" },
        {900,"CM" },
        {500,"D" },
        {400,"CD" },
        {100,"C" },
        {90,"XC" },
        {50,"L" },
        {40,"XL" },
        {10,"X" },
        {9,"IX" },
        {5,"V" },
        {4,"IV" },
        {1,"I" }
    };

    private readonly static RomanNumeral _one = new((char)0x2160);

    private readonly ulong _value;
    private readonly bool _nan = false;

    public readonly static RomanNumeral NaN = new(-1);

    public RomanNumeral(long value)
    {
        if (value < 1)
        {
            _nan = true;
        }
        else
        {
            _value = (ulong)value;
        }
    }

    public RomanNumeral(ulong value) => _value = Normalize(value);

    public RomanNumeral(char unicodeIndex)
        => _value = FromUnicodeCharacter(unicodeIndex);

    public RomanNumeral(string stringValue)
        => _value = Parse(stringValue.ToCharArray());

    private static ulong Parse(char[] chars)
        => !((int)chars.First() is >= 0x2160 and <= 0x216F)
            ? ParseString(chars)
            : ParseUnicode(chars);

    private static ulong ParseString(char[] chars)
    {
        ulong value = 0;

        string str = new(chars);

        do
        {
            string start = str;

            List<KeyValuePair<ulong, string>> pairs = _romanNumerals.ToList();

            foreach (KeyValuePair<ulong, string> pair in pairs)
            {
                if (str.StartsWith(pair.Value))
                {
                    value += pair.Key;
                    str = str[pair.Value.Length..];
                    break;
                }
            }

            if (start == str)
            {
                break;
            }
        } while (str.Length > 0);

        return Normalize(value);
    }

    private static ulong ParseUnicode(char[] chars)
    {
        string sValue = new(chars);
        ulong value = 0;

        while (sValue.Length > 0)
        {
            (char l, char r) pair = (sValue[0], sValue.Length > 1 ? sValue[1] : sValue[0]);
            if (pair.l == 0x2160)
            {
            }
            ulong v = (pair switch
            {
                ( >= (char)0x2160 and < (char)0x2169, _) => (/*pair.l - (char)0x2160,*/ (ulong)pair.l - (ulong)0x2160 + 1),
                ((char)0x2169, (char)0x2169) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x2169 && (ulong)(v.Last()) is (ulong)0x2169),*/ (ulong)10),
                ((char)0x2169, (char)0x216c) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x2169 && (ulong)(v.Last()) is (ulong)0x216c),*/ (ulong)40),
                ((char)0x216c, (char)0x216c) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216c && (ulong)(v.Last()) is (ulong)0x216c),*/ (ulong)50),
                ((char)0x2169, (char)0x216d) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x2169 && (ulong)(v.Last()) is (ulong)0x216d),*/ (ulong)90),
                ((char)0x216d, (char)0x216d) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216d && (ulong)(v.Last()) is (ulong)0x216d),*/ (ulong)100),
                ((char)0x216d, (char)0x216e) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216d && (ulong)(v.Last()) is (ulong)0x216e),*/ (ulong)400),
                ((char)0x216e, (char)0x216e) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216e && (ulong)(v.Last()) is (ulong)0x216e),*/ (ulong)500),
                ((char)0x216d, (char)0x216f) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216d && (ulong)(v.Last()) is (ulong)0x216f),*/ (ulong)900),
                ((char)0x216f, (char)0x216f) => (/*Array.FindIndex<char[]>(_chars, v => (ulong)(v.First()) is (ulong)0x216f && (ulong)(v.Last()) is (ulong)0x216f),*/ (ulong)1000),
                _ => 0,
            });

            value += v;

            if (v is 0)
            {
                return Normalize(value);
            }

            var str = (pair.l == pair.r) switch
            {
                true => new string(pair.l, 1),
                false => new string(pair.l, 1) + new string(pair.r, 1),
            };

            if (sValue.StartsWith(str))
            {
                sValue = sValue.Remove(0, str.Length);
            }
            else
            {
                throw new ArgumentOutOfRangeException(NAN);
            }

        }

        return Normalize(value);
    }

    private static ulong Normalize(ulong value) =>
                value < 1
                    ? throw new ArgumentOutOfRangeException(NAN)
                    : value;

    private static ulong FromUnicodeCharacter(char unicodeIndex)
        => unicodeIndex switch
        {
            >= (char)0x2160 and <= (char)0x216B => (ulong)(unicodeIndex - 0x2160 + 1),
            _ => throw new ArgumentOutOfRangeException(NAN)
        };

    public static RomanNumeral One => _one;

    public static int Radix
    {
        get;
    }

    public static RomanNumeral Zero
        => throw new ArgumentOutOfRangeException(NAN);

    public static RomanNumeral AdditiveIdentity => Zero;

    public static RomanNumeral MultiplicativeIdentity => One;

    public static RomanNumeral Abs(RomanNumeral value) => value;
    public static bool IsCanonical(RomanNumeral value) => true;
    public static bool IsComplexNumber(RomanNumeral value) => false;
    public static bool IsEvenInteger(RomanNumeral value) => value._value % 2 == 0;
    public static bool IsFinite(RomanNumeral value) => true;
    public static bool IsImaginaryNumber(RomanNumeral value) => false;
    public static bool IsInfinity(RomanNumeral value) => false;
    public static bool IsInteger(RomanNumeral value) => true;
    public static bool IsNaN(RomanNumeral value) => value._nan;
    public static bool IsNegative(RomanNumeral value) => false;
    public static bool IsNegativeInfinity(RomanNumeral value) => false;
    public static bool IsNormal(RomanNumeral value) => true;
    public static bool IsOddInteger(RomanNumeral value) => value._value % 2 == 1;
    public static bool IsPositive(RomanNumeral value) => true;
    public static bool IsPositiveInfinity(RomanNumeral value) => false;
    public static bool IsRealNumber(RomanNumeral value) => true;
    public static bool IsSubnormal(RomanNumeral value) => false;
    public static bool IsZero(RomanNumeral value) => false;
    public static RomanNumeral MaxMagnitude(RomanNumeral x, RomanNumeral y) => x._value >= y._value ? x : y;
    public static RomanNumeral MaxMagnitudeNumber(RomanNumeral x, RomanNumeral y) => x._value >= y._value ? y : x;
    public static RomanNumeral MinMagnitude(RomanNumeral x, RomanNumeral y) => x._value < y._value ? x : y;
    public static RomanNumeral MinMagnitudeNumber(RomanNumeral x, RomanNumeral y) => x._value < y._value ? y : x;
    public static RomanNumeral Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => new(Parse(s.ToArray()));
    public static RomanNumeral Parse(string s, NumberStyles style, IFormatProvider? provider) => new(Parse(s.ToCharArray()));
    public static RomanNumeral Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => new(Parse(s.ToArray()));
    public static RomanNumeral Parse(string s, IFormatProvider? provider) => new(Parse(s.ToCharArray()));
    public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out RomanNumeral result)
    {
        result = RomanNumeral.NaN;

        try
        {
            result = new(Parse(s.ToArray()));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    public static bool TryParse([NotNullWhen(true)] string? s, NumberStyles style, IFormatProvider? provider, [MaybeNullWhen(false)] out RomanNumeral result)
    {
        result = RomanNumeral.NaN;

        try
        {
            result = new(Parse(s?.ToArray() ?? "".ToCharArray()));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, [MaybeNullWhen(false)] out RomanNumeral result)
    {
        result = RomanNumeral.NaN;

        try
        {
            result = new(Parse(s.ToArray()));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out RomanNumeral result)
    {
        result = RomanNumeral.NaN;

        try
        {
            result = new(Parse((s ?? "").ToArray()));
            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    static bool INumberBase<RomanNumeral>.TryConvertFromChecked<TOther>(TOther value, out RomanNumeral result)
    {
        result = new(value switch
        {
            INumberBase<byte> b => (byte)(object)b,
            INumberBase<sbyte> b => (sbyte)(object)b,
            INumberBase<short> b => (short)(object)b,
            INumberBase<ushort> b => (ushort)(object)b,
            INumberBase<int> b => (int)(object)b,
            INumberBase<uint> b => (uint)(object)b,
            INumberBase<long> b => (int)(object)b,
            INumberBase<ulong> b => (uint)(object)b,
            INumberBase<float> b => (float)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<double> b => (double)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<decimal> b => (decimal)(object)b == (long)(object)b ? (long)(object)b : -1L,
            _ => -1L,
        });

        return result._nan
            ? throw new ArgumentOutOfRangeException(NAN)
            : true;
    }

    static bool INumberBase<RomanNumeral>.TryConvertFromSaturating<TOther>(TOther value, out RomanNumeral result)
    {
        result = new(value switch
        {
            INumberBase<byte> b => (byte)(object)b,
            INumberBase<sbyte> b => (sbyte)(object)b,
            INumberBase<short> b => (short)(object)b,
            INumberBase<ushort> b => (ushort)(object)b,
            INumberBase<int> b => (int)(object)b,
            INumberBase<uint> b => (uint)(object)b,
            INumberBase<long> b => (int)(object)b,
            INumberBase<ulong> b => (uint)(object)b,
            INumberBase<float> b => (float)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<double> b => (double)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<decimal> b => (decimal)(object)b == (long)(object)b ? (long)(object)b : -1L,
            _ => -1L,
        });

        return !result._nan;
    }

    static bool INumberBase<RomanNumeral>.TryConvertFromTruncating<TOther>(TOther value, out RomanNumeral result)
    {
        result = new(value switch
        {
            INumberBase<byte> b => (byte)(object)b,
            INumberBase<sbyte> b => (sbyte)(object)b,
            INumberBase<short> b => (short)(object)b,
            INumberBase<ushort> b => (ushort)(object)b,
            INumberBase<int> b => (int)(object)b,
            INumberBase<uint> b => (uint)(object)b,
            INumberBase<long> b => (int)(object)b,
            INumberBase<ulong> b => (uint)(object)b,
            INumberBase<float> b => (float)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<double> b => (double)(object)b == (long)(object)b ? (long)(object)b : -1L,
            INumberBase<decimal> b => (decimal)(object)b == (long)(object)b ? (long)(object)b : -1L,
            _ => -1L,
        });

        return !result._nan;
    }

    static bool INumberBase<RomanNumeral>.TryConvertToChecked<TOther>(RomanNumeral value, out TOther result)
    {
        try
        {
            result = (TOther)Convert.ChangeType(value._value, typeof(TOther));
            return true;
        }
        catch
        {
            throw new OverflowException();
        }
    }

    static bool INumberBase<RomanNumeral>.TryConvertToSaturating<TOther>(RomanNumeral value, out TOther result)
    {
        try
        {
            result = (TOther)Convert.ChangeType(value._value, typeof(TOther));
            return true;
        }
        catch
        {
            result = default(TOther)!;
            return false;
        }
    }

    static bool INumberBase<RomanNumeral>.TryConvertToTruncating<TOther>(RomanNumeral value, out TOther result)
    {
        TOther? r = typeof(TOther) switch
        {
            INumberBase<byte> b => (TOther)(object)Math.Min(byte.MaxValue, value._value),
            INumberBase<sbyte> b => (TOther)(object)Math.Min((ulong)sbyte.MaxValue, value._value),
            INumberBase<short> b => (TOther)(object)Math.Min((ulong)short.MaxValue, value._value),
            INumberBase<ushort> b => (TOther)(object)Math.Min(ushort.MaxValue, value._value),
            INumberBase<int> b => (TOther)(object)Math.Min(int.MaxValue, value._value),
            INumberBase<uint> b => (TOther)(object)Math.Min(uint.MaxValue, value._value),
            INumberBase<long> b => (TOther)(object)Math.Min(int.MaxValue, value._value),
            INumberBase<ulong> b => (TOther)(object)Math.Min(uint.MaxValue, value._value),
            INumberBase<float> b => (TOther)(object)Math.Min(float.MaxValue, value._value),
            INumberBase<double> b => (TOther)(object)Math.Min(double.MaxValue, value._value),
            INumberBase<decimal> b => (TOther)(object)Math.Min(decimal.MaxValue, value._value),
            _ => default(TOther)
        };

        result = r ?? (TOther)(object)0;

        return r is not null;
    }

    public int CompareTo(object? obj) => (int)(obj switch
    {
        INumberBase<byte> b => this - new RomanNumeral((byte)b),
        INumberBase<sbyte> b => this - new RomanNumeral((ulong)(sbyte)b),
        INumberBase<short> b => this - new RomanNumeral((short)b),
        INumberBase<ushort> b => this - new RomanNumeral((ulong)(ushort)b),
        INumberBase<int> b => this - new RomanNumeral((int)b),
        INumberBase<uint> b => this - new RomanNumeral((ulong)(uint)b),
        INumberBase<long> b => this - new RomanNumeral((long)b),
        INumberBase<ulong> b => this - new RomanNumeral((ulong)b),
        INumberBase<float> b => this - new RomanNumeral((long)(float)b),
        INumberBase<double> b => this - new RomanNumeral((long)(double)b),
        INumberBase<decimal> b => this - new RomanNumeral((long)(decimal)b),
        _ => RomanNumeral.NaN,
    })._value;

    public int CompareTo(RomanNumeral? other) => (int)(_value - (other?._value ?? 0));
    public bool Equals(RomanNumeral? other) => _value.Equals(other?._value ?? 0);
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        string result = ToString()!;
        charsWritten = result.Length;

        return result.ToCharArray().AsSpan().TryCopyTo(destination);
    }
    int IComparable<RomanNumeral>.CompareTo(RomanNumeral other) => CompareTo(other);
    bool IEquatable<RomanNumeral>.Equals(RomanNumeral other) => Equals(other);

    public static RomanNumeral operator +(RomanNumeral value) => value + value;
    public static RomanNumeral operator +(RomanNumeral left, RomanNumeral right) => new(left._value + right._value);
    public static RomanNumeral operator -(RomanNumeral value) => value - value;
    public static RomanNumeral operator -(RomanNumeral left, RomanNumeral right) => new(left._value - right._value);
    public static RomanNumeral operator ++(RomanNumeral value) => new(value._value + 1);
    public static RomanNumeral operator --(RomanNumeral value) => new(value._value - 1);
    public static RomanNumeral operator *(RomanNumeral left, RomanNumeral right) => new(left._value * right._value);
    public static RomanNumeral operator /(RomanNumeral left, RomanNumeral right) => new(left._value / right._value);
    public static RomanNumeral operator %(RomanNumeral left, RomanNumeral right) => new(left._value % right._value);
    public static bool operator ==(RomanNumeral? left, RomanNumeral? right) => left.Equals(right);
    static bool IEqualityOperators<RomanNumeral, RomanNumeral, bool>.operator ==(RomanNumeral left, RomanNumeral right) => left.Equals(right);
    public static bool operator !=(RomanNumeral? left, RomanNumeral? right) => !left.Equals(right);
    static bool IEqualityOperators<RomanNumeral, RomanNumeral, bool>.operator !=(RomanNumeral left, RomanNumeral right) => !left.Equals(right);
    public static bool operator <(RomanNumeral left, RomanNumeral right) => left._value < right._value;
    public static bool operator >(RomanNumeral left, RomanNumeral right) => left._value > right._value;
    public static bool operator <=(RomanNumeral left, RomanNumeral right) => left._value <= right._value;
    public static bool operator >=(RomanNumeral left, RomanNumeral right) => left._value >= right._value;

    public string ToString(string? format, IFormatProvider? formatProvider) => ToString();

    public override string ToString()
    {
        StringBuilder sb = new();
        ulong remaining = _value;
        for (int i = _values.Length - 1; i >= 0; --i)
        {
            ulong current = _values[i];

            int count = (int)(remaining / current);

            if (count > 0)
            {
                remaining -= (ulong)((ulong)count * current);

                for (int j = 0; j < count; ++j)
                {
                    sb.Append(new string(_chars[i]));
                }

                if (remaining is 0)
                {
                    return sb.ToString();
                }
            }
        }

        return sb.ToString();
    }

    public override bool Equals([NotNullWhen(true)] object? obj) => _value.Equals(((RomanNumeral?)obj)?._value ?? 0);

    public override int GetHashCode() => _value.GetHashCode();

    public static implicit operator ulong(RomanNumeral value) => value._value;
    public static implicit operator long(RomanNumeral value) => Convert.ToInt64(value._value);
    public static implicit operator uint(RomanNumeral value) => Convert.ToUInt32(value._value);
    public static implicit operator int(RomanNumeral value) => Convert.ToInt32(value._value);
    public static implicit operator ushort(RomanNumeral value) => Convert.ToUInt16(value._value);
    public static implicit operator short(RomanNumeral value) => Convert.ToInt16(value._value);
    public static implicit operator byte(RomanNumeral value) => Convert.ToByte(value._value);
    public static implicit operator sbyte(RomanNumeral value) => Convert.ToSByte(value._value);
    public static implicit operator float(RomanNumeral value) => Convert.ToSingle(value._value);
    public static implicit operator double(RomanNumeral value) => Convert.ToDouble(value._value);
    public static implicit operator decimal(RomanNumeral value) => Convert.ToDecimal(value._value);

    public static implicit operator RomanNumeral(ulong value) => new(value);
    public static implicit operator RomanNumeral(long value) => new(value);
    public static implicit operator RomanNumeral(uint value) => new(value);
    public static implicit operator RomanNumeral(int value) => new(value);
    public static implicit operator RomanNumeral(ushort value) => new(value);
    public static implicit operator RomanNumeral(short value) => new(value);
    public static implicit operator RomanNumeral(byte value) => new(value);
    public static implicit operator RomanNumeral(sbyte value) => new(value);
    public static implicit operator RomanNumeral(float value) => new(Convert.ToUInt64(value));
    public static implicit operator RomanNumeral(double value) => new(Convert.ToUInt64(value));
    public static implicit operator RomanNumeral(decimal value) => new(Convert.ToUInt64(value));

    public static implicit operator string(RomanNumeral value) => value.ToString();
    public static implicit operator RomanNumeral(string value) => new(value);
}
