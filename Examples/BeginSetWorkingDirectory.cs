﻿using System;
using System.Net;
using System.Net.FtpClient;
using System.Threading;

namespace Examples {
    public static class BeginSetWorkingDirectoryExample {
        static ManualResetEvent m_reset = new ManualResetEvent(false);

        public static void BeginSetWorkingDirectory() {
            using (FtpClient conn = new FtpClient()) {
                m_reset.Reset();

                conn.Host = "localhost";
                conn.Credentials = new NetworkCredential("ftptest", "ftptest");
                conn.BeginSetWorkingDirectory("/", new AsyncCallback(BeginSetWorkingDirectoryCallback), conn);

                m_reset.WaitOne();
                conn.Disconnect();
            }
        }

        static void BeginSetWorkingDirectoryCallback(IAsyncResult ar) {
            FtpClient conn = ar.AsyncState as FtpClient;

            try {
                if (conn == null)
                    throw new InvalidOperationException("The FtpControlConnection object is null!");

                conn.EndSetWorkingDirectory(ar);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            finally {
                m_reset.Set();
            }
        }
    }
}
