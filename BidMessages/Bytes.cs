﻿using System;
using System.Net;

namespace BidMessages
{
	// [Xu Linqiu] Bytes类是此项目内部使用的静态辅助类，一般先定义为internal，而不是public。

    /// <summary>
    /// Class <c>Bytes</c> encapsulates helper methods related to byte arrays.
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        /// This method converts bytes from a specific index a given byte array to a uint value.
        /// </summary>
        /// <param name="bytes">the source array.</param>
        /// <param name="startIndex">the starting index in <c>bytes</c>.</param>
		/// <returns>A uint value encoded in <c>bytes</c>.</returns>
        public static uint ToUInt32(this byte[] bytes, int startIndex)
        {
            return NetworkToHostOrder(BitConverter.ToUInt32(bytes, startIndex));
        }

        /// <summary>
        /// This method converts bytes from a specific index a given byte array to a ushort value.
        /// </summary>
        /// <param name="bytes">the source array.</param>
        /// <param name="startIndex">the starting index in <c>bytes</c>.</param>
        /// <returns>A ushort value encoded in <c>bytes</c>.</returns>
        public static ushort ToUInt16(this byte[] bytes, int startIndex)
        {
            return NetworkToHostOrder(BitConverter.ToUInt16(bytes, startIndex));
        }

        /// <summary>
        /// This method converts a uint value to host order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same uint in host order.</returns>
        public static uint NetworkToHostOrder(uint net)
        {
            return (uint)IPAddress.NetworkToHostOrder((int)net);
        }

        /// <summary>
        /// This method converts a ushort value to host order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same ushort in host order.</returns>
        public static ushort NetworkToHostOrder(ushort net)
        {
            return (ushort)IPAddress.NetworkToHostOrder((short)net);
        }

        /// <summary>
        /// This method converts a uint value to network order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same uint in network order.</returns>
        public static uint HostToNetworkOrder(uint host)
        {
            return (uint)IPAddress.NetworkToHostOrder((int)host);
        }

        /// <summary>
        /// This method converts a ushort value to network order if necessary.
        /// </summary>
        /// <param name="net">the original uint value.</param>
        /// <returns>The same ushort in network order.</returns>
        public static ushort HostToNetworkOrder(ushort host)
        {
            return (ushort)IPAddress.NetworkToHostOrder((short)host);
        }

        /// <summary>
        /// This method writes a uint value into a byte array at a given index.
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
        /// This method writes a ushort value into a byte array at a given index.
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
