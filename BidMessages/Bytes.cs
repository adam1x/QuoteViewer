using System;
using System.Net;

namespace BidMessages
{
    /// <summary>
    /// Provides helper methods related to byte arrays.
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        /// Converts bytes from a specific index a given byte array to an int value.
        /// </summary>
        /// <param name="bytes">the source array.</param>
        /// <param name="offset">the starting index in <c>bytes</c>.</param>
        /// <returns>An int value encoded in <c>bytes</c>.</returns>
        public static int ToInt32(this byte[] bytes, int offset)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(bytes, offset));
        }

        /// <summary>
        /// Converts bytes from a specific index a given byte array to a uint value.
        /// </summary>
        /// <param name="bytes">the source array.</param>
        /// <param name="offset">the starting index in <c>bytes</c>.</param>
        /// <returns>A uint value encoded in <c>bytes</c>.</returns>
        public static uint ToUInt32(this byte[] bytes, int offset)
        {
            return NetworkToHostOrder(BitConverter.ToUInt32(bytes, offset));
        }

        /// <summary>
        /// Converts bytes from a specific index a given byte array to a ushort value.
        /// </summary>
        /// <param name="bytes">the source array.</param>
        /// <param name="offset">the starting index in <c>bytes</c>.</param>
        /// <returns>A ushort value encoded in <c>bytes</c>.</returns>
        public static ushort ToUInt16(this byte[] bytes, int offset)
        {
            return NetworkToHostOrder(BitConverter.ToUInt16(bytes, offset));
        }

        /// <summary>
        /// Converts a ushort value to network order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same ushort in network order.</returns>
        public static ushort HostToNetworkOrder(ushort host)
        {
            return (ushort)IPAddress.NetworkToHostOrder((short)host);
        }

        /// <summary>
        /// Converts a uint value to host order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same uint in host order.</returns>
        public static uint NetworkToHostOrder(uint net)
        {
            return (uint)IPAddress.NetworkToHostOrder((int)net);
        }

        /// <summary>
        /// Converts a ushort value to host order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same ushort in host order.</returns>
        public static ushort NetworkToHostOrder(ushort net)
        {
            return (ushort)IPAddress.NetworkToHostOrder((short)net);
        }

        /// <summary>
        /// Writes an int value into a byte array at a given index.
        /// </summary>
        /// <param name="value">the int value to be converted.</param>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the index at which this method writes.</param>
        public static void GetBytes(this int value, byte[] target, int offset)
        {
            int temp = value;
            for (int i = 0; i < sizeof(int); i++)
            {
                target[offset + i] = (byte)(temp & 0xFF);
                temp >>= 8;
            }
        }

        /// <summary>
        /// Writes a uint value into a byte array at a given index.
        /// </summary>
        /// <param name="value">the uint value to be converted.</param>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the index at which this method writes.</param>
        public static void GetBytes(this uint value, byte[] target, int offset)
        {
            uint temp = value;
            for (int i = 0; i < sizeof(uint); i++)
            {
                target[offset + i] = (byte)(temp & 0xFF);
                temp >>= 8;
            }
        }

        /// <summary>
        /// Writes a ushort value into a byte array at a given index.
        /// </summary>
        /// <param name="value">the ushort value to be converted.</param>
        /// <param name="target">the target byte array.</param>
        /// <param name="offset">the index at which this method writes.</param>
        public static void GetBytes(this ushort value, byte[] target, int offset)
        {
            int temp = value;
            for (int i = 0; i < sizeof(ushort); i++)
            {
                target[offset + i] = (byte)(temp & 0xFF);
                temp >>= 8;
            }
        }
    }
}
