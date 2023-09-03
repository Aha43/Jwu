namespace Jwu.Constants;

/// <summary>
/// Values used to indicate what part of key pair one references.
/// </summary>
public enum KeyPart
{
    /// <summary>No part</summary>
    None = 0,

    /// <summary>The private part.</summary>
    Private = 1,

    /// <summary>The public part.</summary>
    Public = 2,

    /// <summary>oBoth the private and public part.</summary>
    Both = 3
}
