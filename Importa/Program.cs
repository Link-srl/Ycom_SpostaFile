using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace ScambioDati
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                if (!Zero5.Threading.SingleInstance.ImAloneWithinSystem())
                {
                    Zero5.Util.Log.WriteLog("...double istance, closing.");
                    return;
                }

                MoveFile moveFile = new MoveFile();

                Zero5.Util.Log.WriteLog("START");
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                try
                {
                    System.Threading.Thread.Sleep(1500);
                    moveFile.EseguiMoveFile();
                }
                catch (Exception ex)
                {
                    Zero5.Util.Log.WriteLog(ex.Message);
                }

                sw.Stop();
                Zero5.Util.Log.WriteLog("DONE. Elapsed: " + sw.Elapsed.ToString(@"dd\.hh\:mm\:ss"));

            }
            catch (Exception ex)
            {
                Zero5.Util.Log.WriteLog("Errore Generico: " + ex.Message);
                Zero5.Util.Log.WriteLog(ex.StackTrace);
            }
        }
    }
}
