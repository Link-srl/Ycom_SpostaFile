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
                string[] parts = fileName.Split(' ');
                string code = parts[0];
                string serial = parts[4];
                string pattern = @"\d+"; // match one or more digits
                string digits;
                string folderCode = destFolder + code;
                string folderSerial;

                #region cartella con codice

                Zero5.Util.Log.WriteLog("Checking if folder exists: " + folderCode);
                if (Directory.Exists(folderCode))
                {
                    Zero5.Util.Log.WriteLog("Folder already exists.");
                    // add a suffix to the folder name to create a new folder with a different name                   
                }
                else
                {
                    Zero5.Util.Log.WriteLog("Folder does not exist.");
                    Zero5.Util.Log.WriteLog("Creating folder: " + folderCode);
                    Directory.CreateDirectory(folderCode);
                }

                #endregion

                #region cartella con seriale

                Match match = Regex.Match(serial, pattern);
                if (match.Success)
                {
                    digits = match.Value;
                    folderSerial = folderCode + "\\" + digits;
                    Zero5.Util.Log.WriteLog(digits); // shows in log what we are taking from the serial number
                    Zero5.Util.Log.WriteLog("Checking if folder exists: " + folderSerial);
                    if (Directory.Exists(folderSerial))
                    {
                        Zero5.Util.Log.WriteLog("Folder already exists.");
                        // add a suffix to the folder name to create a new folder with a different name                       
                    }
                    else
                    {
                        Zero5.Util.Log.WriteLog("Folder does not exist.");
                        Zero5.Util.Log.WriteLog("Creating folder: " + folderSerial);
                        Directory.CreateDirectory(folderSerial);
                    }
                    string destinationFile = Path.Combine(folderSerial, fileName);

                    File.Move(file, destinationFile);
                }
                else
                {
                    Zero5.Util.Log.WriteLog("No digits found.");
                }

                #endregion
            }

            Zero5.Util.Log.WriteLog("Spostamento completato.");
            Console.ReadLine();
        }    
    }
}