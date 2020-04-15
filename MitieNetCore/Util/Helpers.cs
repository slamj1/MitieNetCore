using MitieNetCore.Wrappers;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MitieNetCore.Util
{
    /// <summary>
    /// 
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Extracts a string List from the char** returned from Mitie.
        /// </summary>
        /// <param name="charArrayPtr">The character array PTR.</param>
        /// <returns></returns>
        public static List<string> IntPtrToStringArrayAnsi(ref IntPtr charArrayPtr,
                                                           bool freeUnmanaged = false)
        {
            var tokenList = new List<string>();
            IntPtr tempPtr = charArrayPtr;

            while (true)
            {
                var stringPtr = (IntPtr)Marshal.PtrToStructure(tempPtr, typeof(IntPtr));
                var managedString = Marshal.PtrToStringAnsi(stringPtr);

                if (!string.IsNullOrEmpty(managedString))
                {
                    tokenList.Add(managedString);

                    tempPtr = IntPtr.Add(tempPtr, IntPtr.Size);

                    // Note: Another way to increment pointer. Above is likely faster:
                    // charArrayPtr = new IntPtr(charArrayPtr.ToInt64() + IntPtr.Size);
                }
                else
                {
                    break;
                }
            }

            if (freeUnmanaged == true)
            {
                MarshaledWrapper.Exec_mitie_free(charArrayPtr);
                charArrayPtr = IntPtr.Zero;
            }

            return tokenList;
        }

        /// <summary>
        /// Unsafes the long array to uint list.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="len">The length.</param>
        /// <returns></returns>
        public static unsafe List<uint> UnsafeLongArrayToUintList(uint* items, 
                                                                  uint len)
        {
            List<uint> list = new List<uint>();

            for (int i = 0; i < len; i++)
            {
                list.Add(items[i]);
            }

            return list;
        }

        /// <summary>
        /// Marshals the long array to uint list.
        /// </summary>
        /// <param name="ptr">The PTR.</param>
        /// <param name="length">The length.</param>
        /// <param name="freeUnmanaged">if set to <c>true</c> [free unmanaged].</param>
        /// <returns></returns>
        public static List<uint> MarshalLongArrayToUintList(ref IntPtr ptr, 
                                                            uint length,
                                                            bool freeUnmanaged = false)
        {
            List<uint> list = new List<uint>();
            IntPtr tempPtr = ptr;

            for (int i = 0; i < length; i++)
            {
                list.Add((uint)Marshal.PtrToStructure(tempPtr, typeof(uint)));
                tempPtr = IntPtr.Add(tempPtr, IntPtr.Size);
            }

            if (freeUnmanaged == true)
            {
                MarshaledWrapper.Exec_mitie_free(ptr);
                ptr = IntPtr.Zero;
            }

            return list;
        }
    }
}
