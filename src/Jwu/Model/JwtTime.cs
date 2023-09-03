using Jwu.Constants;
using Jwu.Exceptions;

namespace Jwu.Model;

/// <summary>
/// Used to specify when from creation time of a Json Web Token (JWT) or Json Web Key (JWK) the exp and nbf properties is.
/// </summary>
public readonly struct JwtTime
{
    public JwtTime() { }

    public KeyPart KeyPart { get; init; } = KeyPart.None;

    /// <summary>Time to pass from UTC now to the point in time this represent.</summary>
    public int Time { get; init; } = 0;

    /// <summary>Unit of Time.</summary>
    public JwtTimeUnit TimeUnit { get; init; } = JwtTimeUnit.Year; 

    /// <returns>UTC time from now to the point in time this represent</returns>
    public readonly DateTimeOffset ToUtc()
    {
        return TimeUnit switch
        {
            JwtTimeUnit.Year => DateTimeOffset.UtcNow.AddYears(Time),
            JwtTimeUnit.Month => DateTimeOffset.UtcNow.AddMonths(Time),
            JwtTimeUnit.Day => DateTimeOffset.UtcNow.AddDays(Time),
            JwtTimeUnit.Hour => DateTimeOffset.UtcNow.AddHours(Time),
            JwtTimeUnit.Minute => DateTimeOffset.UtcNow.AddMinutes(Time),
            JwtTimeUnit.Second => DateTimeOffset.UtcNow.AddSeconds(Time),
            _ => throw new ShouldNotHappenException()
        };
    }

    /// <returns>Unixtime in seconds from now to the point in time this represent. This is the value used in JWT and JWK json</returns>
    public readonly long ToUnixtimeSeconds() => ToUtc().ToUnixTimeSeconds();

}
