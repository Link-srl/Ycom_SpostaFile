using OfficeOpenXml;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.ClientServices;
using System.Web.UI.Design.WebControls;
using System.Windows.Forms;
using Zero5.Data.Layer;
using Zero5.Util;
using static Zero5.Data.Model.mDatiReportProduzione_DettaglioScarti;

namespace ScambioDati
{
    class MoveFile
    {
        public void EseguiMoveFile()
        {
            string sourceFolder = @"C:\Share\WENZEL\TO_Phase\"; // percorso del file di origine
            string destFolder = @"C:\Share\WENZEL\FROM_Phase\"; // percorso del file di destinazione
            Zero5.Util.Log.WriteLog("From folder: " + sourceFolder);
            Zero5.Util.Log.WriteLog("Destination folder : " + destFolder);
            string[] files = Directory.GetFiles(sourceFolder); // elenco dei file nella cartella di origine

            if (files.Length == 0)
            {
                Zero5.Util.Log.WriteLog("Nessun file trovato nella cartella di origine.");
                return;
            }

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file); // nome del file
                Zero5.Util.Log.WriteLog("Nome file che sta venendo elaborato: " + fileName);
                string destinationFile = Path.Combine(destFolder, fileName); // percorso completo del file di destinazione
                File.Move(file, destinationFile); // sposta il file nella cartella di destinazione
            }

            Zero5.Util.Log.WriteLog("Spostamento completato.");
            Console.ReadLine();
        }    
    }
}