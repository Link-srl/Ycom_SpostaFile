using Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zero5.Data.Layer;

namespace Esporta
{
    class RecordEsportazioneAvanzamentiERP
    {
        public eTipoRecord_ERP V01_ERP_TipoRecord_TrueTesta_FalseRiga = eTipoRecord_ERP.Riga;
        public eTipoOperazione_ERP V01_ERP_TipoOperazioneAvanzamento = eTipoOperazione_ERP.Sconosciuto;
        public DateTime V01_ERP_DataRegistrazione = DateTime.MinValue;
        public string V01_ERP_RiferimentoOrdineProduzione = "";
        public string V01_ERP_CodiceArticolo = "";
        public string V01_ERP_CodiceVarianteArticolo = "";
        public double V01_ERP_QuantitaPrincipale = 0;
        public double V01_ERP_QuantitaScartoPrimaScelta = 0;
        public double V01_ERP_NumeroFase = 0;
        public int V01_ERP_CodMacchinaReale = 0;
        public string V01_ERP_CodMacchinaUff = "";
        public double V01_ERP_TConsMachRealeUltima = 0;
        public int V01_ERP_CodiceRisorsaMacchina = 0;
        public string V01_ERP_CodiceRisorsaUomo = "";
        public double V01_ERP_TConsUomooRealeTot = 0;
        public bool V01_ERP_RigaSaldata = false;
        public string V01_ERP_Commessa = "";
        public int V01_ERP_CodiceRisorsaUomo1 = 0;
        public string V01_ERP_CodiceUomo = "";
        public double V01_ERP_MinutiLavoratiUomo = 0;
        public double V01_ERP_MinutiLavoratiMacchina = 0;
        public string V01_ERP_CodiceCausale = "";
        public double V01_ERP_TConsUomoRealeTot = 0;
        public string V01_ERP_Descrizione = "";
        public double V02_ERP_QuantitaScartoSecondaScelta_DaRilavorare = 0;

        public int Phase_IdArticolo = 0;
        public int Phase_IdCausale = 0;
        public int Phase_IdRisorsa = 0;
        public int Phase_idRigaDistinta = 0;
        public int Phase_IdFaseProduzione = 0;

        public List<int> Phase_lstTransazioniCoinvolte = new List<int>();
        public List<int> Phase_lstMovimentiCoinvolti = new List<int>();
        public bool Esportata;

        public string FormatToCsvString(eVersioneFormatoEsportazione formato)
        {



            if (formato == eVersioneFormatoEsportazione.V01_14Campi)
            {
                Esportata = true;
                return 
                        V01_ERP_DataRegistrazione.ToString("dd/MM/yyyy") + ";" +
                        V01_ERP_Commessa + ";" +
                        V01_ERP_RiferimentoOrdineProduzione + ";" +
                        V01_ERP_CodiceArticolo + ";" +
                        V01_ERP_NumeroFase + ";" +
                        V01_ERP_CodMacchinaUff + ";" +
                        V01_ERP_TConsMachRealeUltima + ";" +
                        V01_ERP_CodiceRisorsaUomo + ";" +
                        V01_ERP_TConsUomoRealeTot + ";";
                       
            }

            throw new Exception("Formato esportazione sconosciuto");
        }

        /// <summary>
        /// Crea un record esportazione a partire da una transazione. NB: non somma il valore della transazione corrente.
        /// </summary>
        /// <param name="transazioniDaEsportare"></param>
        /// <param name="tipoRiga"></param>
        public RecordEsportazioneAvanzamentiERP(Zero5.Data.Layer.vOrdiniProduzioneFasiProduzioneTransazioni transazioniDaEsportare)
        {
                    V01_ERP_DataRegistrazione = transazioniDaEsportare.Ordine_FineReale;
                    V01_ERP_Commessa = transazioniDaEsportare.Ordine_Commessa;
                    V01_ERP_RiferimentoOrdineProduzione = transazioniDaEsportare.Ordine_Codice;
                    V01_ERP_CodiceArticolo = transazioniDaEsportare.Ordine_Articolo;
                    V01_ERP_NumeroFase = transazioniDaEsportare.Fase_NumeroFase;
                    //V01_ERP_CodMacchinaReale = transazioniDaEsportare.Fase_IDRisorsaMacchinaRealeUltima;
                    //if(transazioniDaEsportare.)
                    V01_ERP_CodMacchinaReale = transazioniDaEsportare.Transazione_IDRisorsaMacchina;
                    //V01_ERP_CodMacchinaReale = macchinePhase.CodiceEsterno;
                    V01_ERP_CodiceRisorsaUomo1 = transazioniDaEsportare.Transazione_IDRisorsaUomo;
                    V01_ERP_TConsMachRealeUltima = Math.Round(transazioniDaEsportare.Fase_TConsMachTotCiclo);
                    V01_ERP_TConsUomoRealeTot = Math.Round(transazioniDaEsportare.Fase_TConsUomoTotLavorazione);
                   /* if(transazioniDaEsportare.Transazione_Causale == 205)
                    {
                        V01_ERP_MinutiLavoratiMacchina = transazioniDaEsportare.Transazione_Minuti;
                    }
                    else
                    {
                        V01_ERP_MinutiLavoratiUomo = transazioniDaEsportare.Transazione_Minuti;
                    }
                   */
        }

        public void MarcaEsportateTransazioniCoinvolte()
        {
            try
            {
                if (Phase_lstTransazioniCoinvolte.Count > 0)
                {
                    List<List<int>> multipleList = Zero5.Util.Common.SplitList(Phase_lstTransazioniCoinvolte, 300);
                    foreach (List<int> lstTransazioni in multipleList)
                    {
                        Zero5.Data.Layer.Transazioni transUpdate = new Zero5.Data.Layer.Transazioni();
                        transUpdate.Load(transUpdate.Fields.IDTransazione.FilterIn(lstTransazioni));

                        while (!transUpdate.EOF)
                        {
                            transUpdate.Esportato = 1;
                            transUpdate.MoveNext();
                        }
                        transUpdate.Save();
                    }
                }

                if (V01_ERP_RigaSaldata)
                {
                    Zero5.Data.Layer.FasiProduzione fp = new Zero5.Data.Layer.FasiProduzione();
                    fp.Load(fp.Fields.IDFaseProduzione == Phase_IdFaseProduzione);

                    fp.RiferimentoNumerico3 = (double)eStatoRiga_eSOLVER.Terminato + 100;
                    fp.Save();
                }
            }
            catch (Exception ex)
            {
                Zero5.Util.Log.WriteLog("Eccezione salvataggio esportato = 1 per transazioni ID: " +
                    Zero5.Util.StringConverters.IntListToString(Phase_lstTransazioniCoinvolte) + Environment.NewLine + "Exc. " + ex.Message);
                Zero5.Util.Log.WriteLog("ERRORE_ESPORTAZIONE", "Eccezione salvataggio esportato = 1 per transazioni ID: " + Zero5.Util.StringConverters.IntListToString(Phase_lstTransazioniCoinvolte) + Environment.NewLine + "Exc. " + ex.Message);
            }
        }
    }
}
