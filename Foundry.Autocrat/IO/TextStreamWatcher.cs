using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Foundry.Autocrat.IO
{
    public class TextStreamWatcher : IDisposable
    {
        protected Stream WatchedStream { get; private set; }

        public TextStreamWatcher(string file) :
            this(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) { }

        public TextStreamWatcher(Stream stream)
        {
            WatchedStream = stream;
            if (WatchedStream.CanSeek) WatchedStream.Seek(0, SeekOrigin.End);
        }

        public string ReadLine()
        {
            string line = "";

            while (true)
            {
                int i = WatchedStream.ReadByte();
                if (i == -1) break;

                line += (char)i;
                if (line.EndsWith("\r\n"))
                {
                    line = line.Substring(0, line.Length - 2);

                    return line;
                }
            }

            return "";
        }

        #region Dispose implementation

        private bool disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources
                    WatchedStream.Close();
                }

            }
            disposed = true;
        }

        ~TextStreamWatcher()
        {
            Dispose(false);
        }

        #endregion

    }
}
