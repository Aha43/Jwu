using Jwu.Constants;
using Jwu.Exceptions;

namespace Jwu.Model;

/// <summary>
/// Used to specify when from creation time of a Json Web Token (JWT) or Json Web Key (JWK) the exp and nbf properties is.
/// </summary>
public readonly struct JwtTime
{
    public JwtTime() { }

    /// <summary>Tells if this JWT time is used in key parts.</summary>
    public KeyPart KeyPart { get; init; }

    /// <summary>Time to pass from UTC now to the point in time this represent.</summary>
    public int Time { get; init; }

    /// <summary>Unit of Time.</summary>
    public JwtTimeUnit TimeUnit { get; init; }
    
    /// <summary>Calculate point in time this represent from given date.</summary>
    public readonly DateTimeOffset From(DateTimeOffset date)
    {
        return TimeUnit switch
        {
            JwtTimeUnit.Year => date.AddYears(Time),
            JwtTimeUnit.Month => date.AddMonths(Time),
            JwtTimeUnit.Day => date.AddDays(Time),
            JwtTimeUnit.Hour => date.AddHours(Time),
            JwtTimeUnit.Minute => date.AddMinutes(Time),
            JwtTimeUnit.Second => date.AddSeconds(Time),
            _ => throw new ShouldNotHappenException()
        };
    }

    /// <summary>Tells if defines time for key parts.</summary>
    public bool RepresentKeyTime() => KeyPart != KeyPart.None && Time > 0;

}
