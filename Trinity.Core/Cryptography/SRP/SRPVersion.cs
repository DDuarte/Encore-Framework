using System;

namespace Trinity.Core.Cryptography.SRP
{
    /// <summary>
    /// SRP algorithm version.
    /// </summary>
    [Serializable]
    public enum SRPVersion : byte
    {
        /// <summary>
        /// Secure Remote Password version 6.
        /// </summary>
        SRP6,
        /// <summary>
        /// Secure Remote Password version 6a.
        /// 
        /// It defines k = H(N, g) rather than k = 3.
        /// </summary>
        SRP6A,
    }
}
