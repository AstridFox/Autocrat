using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Schema;
using System.IO.Compression;

using Foundry.Autocrat.Extensions.Memory;
using System.Xml;
using System.IO;
using System.Reflection;

namespace Foundry.Autocrat.Memory {

	public class PointerLibrary {
		protected XDocument PointerDocument { get; set; }

		protected PointerLibrary() { }
		public PointerLibrary(XDocument pointerDocument) {
			Validate(pointerDocument);
			PointerDocument = pointerDocument;
			pointerPathCache = new Dictionary<string, Tuple<int, Queue<int>>>();
		}

		protected void Validate(XDocument pointerDocument) {
			var schemas = new XmlSchemaSet();
			using (Stream schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Foundry.Autocrat.Memory.pointers.xsd")) {
				schemas.Add("http://macrocrafter.com/pointers.xsd", XmlReader.Create(schemaStream));
			}

			List<string> errors = new List<string>();
			pointerDocument.Validate(schemas, (o, e) =>
			{
				errors.Add(e.Exception.Message);
			});

			if (errors.Count != 0) {
				throw new XmlSchemaException("Validation Errors: " + string.Join("\r\n", errors.ToArray()));
			}
		}

		public IntPtr Resolve(Process process, string pointerPath) {
			var pointerData = ParsePointerPath(pointerPath);

			int finalValue = pointerData.Value1;
			var offsets = pointerData.Value2;

			while (offsets.Count > 0) {
				int os = offsets.Dequeue();
				finalValue = process.Read<int>(new IntPtr(finalValue)) + os;
			}

			return new IntPtr(finalValue);
		}

		private Dictionary<string, Tuple<int, Queue<int>>> pointerPathCache;

		private Tuple<int, Queue<int>> ParsePointerPath(string pointerPath) {
			// Cache values so as not to parse the same path more than once; basepointers and offsets
			// never change because pointer libraries are immutable.
			//if (pointerPathCache.ContainsKey(pointerPath)) return pointerPathCache[pointerPath];
			
			string[] path = pointerPath.Split('/');
			var pointers = PointerDocument.Root;
			var bp = pointers.Elements(XName.Get("pointer","http://macrocrafter.com/pointers.xsd")).First(e => e.Attribute("name").Value == path[0]);

			int baseAddress = int.Parse(bp.Attribute("address").Value, System.Globalization.NumberStyles.HexNumber);

			Queue<int> offsets = new Queue<int>();

			var offset = bp;
			var xx = offset.Elements();

			for (int i = 1; i < path.Length; i++) {
				offset = offset.Elements(XName.Get("offset","http://macrocrafter.com/pointers.xsd")).First(e => e.Attribute("name").Value == path[i]);
				offsets.Enqueue(int.Parse(offset.Attribute("value").Value, System.Globalization.NumberStyles.HexNumber));
			}

			var r = Tuples.Tuple(baseAddress, offsets);
			//pointerPathCache.Add(pointerPath, r);
			return r;
		}

		public T Read<T>(Process process, string pointerPath) where T : struct {
			return process.Read<T>(Resolve(process, pointerPath));
		}

		public byte[] ReadBuffer(Process process, string pointerPath, int numBytes) {
			return process.ReadBuffer(Resolve(process, pointerPath), numBytes);
		}

		public string ReadString(Process process, string pointerPath, int length, Encoding encoding, bool nullTerminated) {
			return process.ReadString(Resolve(process, pointerPath), length, encoding, nullTerminated);
		}

		public void Write<T>(Process process, string pointerPath, T value) where T : struct {
			process.Write<T>(Resolve(process, pointerPath), value);
		}

		public void Write(Process process, string pointerPath, byte[] buffer) {
			process.Write(Resolve(process, pointerPath), buffer);
		}

		public void Write(Process process, string pointerPath, string value, Encoding encoding) {
			process.Write(Resolve(process, pointerPath), value, encoding);
		}

	}

}