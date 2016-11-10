using UnityEngine;
using System;
using System.Collections;

namespace itsybitsy.Utilities
{
    public class BitUtility : MonoBehaviour
    {
        public static string GenerateHash(string input)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(input);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        /// <summary>
        /// Copies a number of bits from the source array starting from the given start index.
        /// </summary>
        /// <returns>The copied bits.</returns>
        /// <param name="source">Source bit array.</param>
        /// <param name="length">Number of bits to copy.</param>
        /// <param name="startIndex">Source bit array index to start copying from.</param>
        public static BitArray CopyBits(BitArray source, int length, int startIndex = 0)
        {
            int len = Mathf.Min(source.Length, length);
            BitArray copy = new BitArray(len);
            int maxIndex = len - 1;
            int copyIndex = 0;
            for (int i = startIndex; i <= maxIndex; i++)
            {
                copy[copyIndex++] = source[i];
            }
            return copy;
        }

        /// <summary>
        /// Concatenates the bits in the given bit arrays.
        /// </summary>
        /// <returns>The concatenated bits.</returns>
        /// <param name="bitSources">Bit arrays to join.</param>
        public static BitArray ConcatBits(BitArray[] bitSources)
        {
            int totalBits = 0;
            foreach (var bs in bitSources) totalBits += bs.Length;

            BitArray concat = new BitArray(totalBits);

            int index = 0;
            foreach (var bitSource in bitSources)
            {
                for (int i = 0; i < bitSource.Length; i++)
                {
                    concat[index++] = bitSource[i];
                }
            }

            return concat;
        }

        /// <summary>
        /// Reverses the order of the bits in the given array.
        /// source: http://stackoverflow.com/a/4791224
        /// </summary>
        /// <returns>The bits.</returns>
        /// <param name="bits">Bits.</param>
        public static BitArray ReverseBits(BitArray bits)
        {
            BitArray reversed = CopyBits(bits, bits.Length);

            int length = reversed.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = reversed[i];
                reversed[i] = reversed[length - i - 1];
                reversed[length - i - 1] = bit;
            }

            return reversed;
        }

        /// <summary>
        /// Gets the bytes from the given bit array.
        /// reference: http://stackoverflow.com/a/4619295
        /// </summary>
        /// <returns>The bytes.</returns>
        /// <param name="bits">Bits.</param>
        /// <param name="readLeftToRight">If set to <c>true</c> read the bit array from left to right.</param>
        public static byte[] GetBytes(BitArray bits, bool readLeftToRight = true)
        {
            // should reverse if we want to read from left to right since BitArray.CopyTo() reads bytes from right to left
            if (readLeftToRight) bits = ReverseBits(bits);

            byte[] bytes = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(bytes, 0);

            return bytes;
        }

        /// <summary>
        /// Get the bytes from given string.
        /// </summary>
        /// <param name="byteString"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string byteString)
        {
            byte[] bytes = new byte[byteString.Length];
            for (int x = 0; x < byteString.Length; x++)
            {
                bytes[x] = Convert.ToByte(byteString[x]);
            }

            return bytes;
        }

        /// <summary>
        /// Get the bits per given byte[]
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static BitArray[] GetBitArray(byte[] bytes, bool readLeftToRight = true)
        {
            BitArray[] bits = new BitArray[bytes.Length];

            for (int x = 0; x < bits.Length; x++)
            {
                byte[] newByte = new byte[1];
                newByte[0] = bytes[x];
                BitArray bit = new BitArray(newByte);

                if (readLeftToRight) bits[x] = ReverseBits(bit);
                else bits[x] = bit;
            }
                        
            return bits;
        }       
    
        /// <summary>
        /// Gets string representation of bit array.
        /// </summary>
        /// <returns>Bits string.</returns>
        /// <param name="bits">Bits.</param>
        public static string BitsToString(BitArray bits)
        {
            string stringBuilder = null;
            
            for (int i = 0; i < bits.Length; i++)
            {
                stringBuilder += bits[i] == true ? 1 : 0;
            }
            return stringBuilder;
        }

    }

}
