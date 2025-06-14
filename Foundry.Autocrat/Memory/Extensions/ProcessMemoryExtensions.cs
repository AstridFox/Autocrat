using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Foundry.Autocrat.Extensions.Memory {
	public static class ProcessMemoryExtensions {

		#region INTEROP

		[DllImport("kernel32.dll")]
		internal static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, out uint lpNumberOfBytesRead);
		[DllImport("kernel32.dll")]
		internal static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, uint size, out uint lpNumberOfBytesRead);
		[DllImport("kernel32.dll")]
		internal static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, out uint lpNumberOfBytesWritten);
		[DllImport("kernel32.dll")]
		internal static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr buffer, uint size, out uint lpNumberOfBytesWritten);
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, ProtectionType flProtect);
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType dwFreeType);
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
		internal static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern IntPtr GetModuleHandle(string lpModuleName);
		[DllImport("kernel32.dll")]
		internal static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, ProtectionType flNewProtect, out ProtectionType lpflOldProtect);

		[Flags]
		public enum AllocationType : uint {
			Commit = 0x1000,
			Reserve = 0x2000,
			Decommit = 0x4000,
			Release = 0x8000,
			Reset = 0x80000,
			Physical = 0x400000,
			TopDown = 0x100000
		}
		[Flags]
		public enum ProtectionType : uint {
			Execute = 0x0010,
			ExecuteRead = 0x0020,
			ExecuteReadWrite = 0x0040,
			ExecuteWriteCopy = 0x0080,
			NoAccess = 0x0001,
			ReadOnly = 0x0002,
			ReadWrite = 0x0004,
			WriteCopy = 0x0008,
			Guard = 0x0100,
			NoCache = 0x0200,
			WriteCombine = 0x0400
		}

		#endregion

		/// <summary>
		/// Gets the size of a given type.
		/// </summary>
		/// <remarks><paramref name="structure"/> must be a value type.</remarks>
		/// <param name="structure">The type of the structure to retrieve the size for.</param>
		/// <returns>The size of the given structure.</returns>
		public static int GetNumStructBytes(this Type structure) {
			if (!structure.IsValueType)
				throw new ArgumentException("'structure' must be a ValueType");

			return Marshal.SizeOf(structure);
		}

		#region Memory Reading Methods

		/// <summary>
		/// Reads a byte buffer from the specified address.
		/// </summary>
		/// <param name="address">The address to read the byte buffer from</param>
		/// <param name="numBytes">The number of bytes to read</param>
		public static byte[] ReadBuffer(this Process proc, IntPtr address, int numBytes) {
			byte[] rBytes = new byte[numBytes];
			uint numBytesRead;
			ReadProcessMemory(proc.Handle, address, rBytes, (uint)numBytes, out numBytesRead);

			return rBytes;
		}

		/// <summary>
		/// Reads a byte buffer from the specified address.
		/// </summary>
		/// <param name="address">The address to read the byte buffer from</param>
		/// <param name="numBytes">The number of bytes to read</param>
		/// <param name="bytes">The byte array to fill</param>
		public static void ReadBuffer(this Process proc, IntPtr address, int numBytes, ref byte[] bytes) {
			uint numBytesRead;
			ReadProcessMemory(proc.Handle, address, bytes, (uint)numBytes, out numBytesRead);
		}

		/// <summary>
		/// Reads a string from the specified address.
		/// </summary>
		/// <param name="address">The address to read the string from</param>
		/// <param name="length">The length of the string in characters</param>
		/// <param name="encoding">The encoding used to decode the string</param>
		/// <remarks>
		///     String values may have variable byte lengths.
		///     Use <code><paramref name="encoding"/>.GetMaxByteCount(<paramref name="length"/>)</code>
		///     to determine the number of bytes that will be read.
		/// </remarks>
		public static string ReadString(this Process proc, IntPtr address, int length, Encoding encoding, bool nullTerminated) {
			int numBytes = encoding.GetMaxByteCount(length);
			string s = encoding.GetString(ReadBuffer(proc, address, numBytes));
            if (s.IndexOf('\0') < 0) return "";
			if (nullTerminated) s = s.Substring(0,s.IndexOf('\0'));
			return s;
		}

		/// <summary>
		/// Reads a structure from the specified address.
		/// </summary>
		/// <typeparam name="T">The type of the structure. Must be a value type</typeparam>
		/// <param name="address">The address to read the structure from.</param>
		/// <exception cref="ArgumentException">Occurs if <typeparamref name="T"/> is not a value type.</exception>
		/// <remarks>
		///     Structures may have variable byte lengths.
		///     Use <code>ProcessMemoryExtensions.NumStructBytes(<typeparamref name="T"/>)</code>
		///     to determine the number of bytes that will be read.
		/// </remarks>
		public static T Read<T>(this Process proc, IntPtr address) where T : struct {
			int numBytes = Marshal.SizeOf(typeof(T));
			uint numBytesRead;
			IntPtr structPointer = Marshal.AllocHGlobal(numBytes);

			ReadProcessMemory(proc.Handle, address, structPointer, (uint)numBytes, out numBytesRead);
			T r = (T)Marshal.PtrToStructure(structPointer, typeof(T));

			Marshal.FreeBSTR(structPointer);

			return r;
		}

		#endregion

		#region Memory Writing Methods

		/// <summary>
		/// Writes a buffer to the specified address.
		/// </summary>
		/// <param name="address">The address to write the buffer to</param>
		/// <param name="buffer">The buffer to write</param>
		public static void Write(this Process proc, IntPtr address, byte[] buffer) {
			uint numBytesWritten;
			WriteProcessMemory(proc.Handle, address, buffer, (uint)buffer.Length, out numBytesWritten);
		}

		/// <summary>
		/// Writes a string to the specified address.
		/// </summary>
		/// <param name="address">The address to write the string to</param>
		/// <param name="value">The string to write</param>
		/// <param name="encoding">The encoding used to encode the string</param>
		/// <remarks>
		///     String values may have variable byte lengths.
		///     Use <code>encoding.GetBytes(<paramref name="value"/>)</code>
		///     to determine the number of bytes that will be written.
		/// </remarks>
		public static void Write(this Process proc, IntPtr address, string value, Encoding encoding) {
			byte[] buffer = encoding.GetBytes(value);
			uint numBytesWritten;
			WriteProcessMemory(proc.Handle, address, buffer, (uint)buffer.Length, out numBytesWritten);
		}

		/// <summary>
		/// Writes a structure to the specified address.
		/// </summary>
		/// <typeparam name="T">The type of the structure. Must be a value type</typeparam>
		/// <param name="address">The address to write the structure to</param>
		/// <param name="structure">The instance of <typeparamref name="T"/> to write</param>
		/// <exception cref="ArgumentException">Occurs if <typeparamref name="T"/> is not a value type.</exception>
		/// <remarks>
		///     Structures may have variable byte lengths.
		///     Use <code>ProcessEx.NumStructBytes(<typeparamref name="T"/>)</code>
		///     to determine the number of bytes that will be written.
		/// </remarks>
		public static void Write<T>(this Process proc, IntPtr address, T structure) where T : struct {
			int numBytes = Marshal.SizeOf(typeof(T));
			IntPtr structPointer = Marshal.AllocHGlobal(numBytes);
			Marshal.StructureToPtr(structure, structPointer, false);
			uint numBytesWritten;

			WriteProcessMemory(proc.Handle, address, structPointer, (uint)numBytes, out numBytesWritten);

			Marshal.FreeHGlobal(structPointer);

			return;
		}

		#endregion

		#region Memory Allocation Methods

		/// <summary>
		///     Allocates memory in this process.
		///     Memory allocated via <code>AllocateMemory</code> must be deallocated using
		///     <code>DeallocateMemory</code> before the calling process finishes,
		///     or else a memory leak will occur.
		/// </summary>
		/// <param name="size">The minimum number of bytes to allocate</param>
		/// <param name="protection">The protection mode to use</param>
		/// <returns>An IntPtr containing the address to the newly allocated memory</returns>
		public static IntPtr AllocateMemory(this Process proc, uint size, ProtectionType protection) {
			return AllocateMemory(proc, IntPtr.Zero, size, protection);
		}

		/// <summary>
		///     Allocates memory in this process.
		///     Memory allocated via <code>AllocateMemory</code> must be deallocated using
		///     <code>DeallocateMemory</code> before the calling process finishes,
		///     or else a memory leak will occur.
		/// </summary>
		/// <param name="address">The desired address to allocate memory to</param>
		/// <param name="size">The minimum number of bytes to allocate</param>
		/// <param name="protection">The protection mode to use</param>
		/// <returns>An IntPtr containing the address to the newly allocated memory</returns>
		public static IntPtr AllocateMemory(this Process proc, IntPtr address, uint size, ProtectionType protection) {
			return VirtualAllocEx(proc.Handle, address, size, AllocationType.Commit, protection);
		}

		/// <summary>
		/// Deallocates previously-allocated memory in this process.
		/// </summary>
		/// <param name="address">The address of the memory to deallocate (returned from <code>AllocateMemory</code>)</param>
		/// <param name="size">The size in bytes to deallocate. Specify 0 to deallocate all previous-allocated memory.</param>
		public static void DeallocateMemory(this Process proc, IntPtr address, uint size) {
			VirtualFreeEx(proc.Handle, address, size, AllocationType.Decommit);
		}

		/// <summary>
		/// Changes the protection flags for the specified block of memory.
		/// </summary>
		/// <param name="address">The start address for the block to change protection flags for.</param>
		/// <param name="size">The size of the block to change protection flags for.</param>
		/// <param name="newProtection">The new protection flags.</param>
		/// <returns>The old protection flags that covered the specified block.</returns>
		public static uint ChangeProtection(this Process proc, IntPtr address, uint size, ProtectionType newProtection) {
			ProtectionType oldProtection;
			VirtualProtectEx(proc.Handle, address, size, newProtection, out oldProtection);
			return (uint)oldProtection;
		}

		#endregion
	}
}
