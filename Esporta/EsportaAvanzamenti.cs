using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Zero5.Data.Layer;
using System.Globalization;
using Shared;
using System.Collections.Concurrent;

namespace Esporta
{
    class EsportaAvanzamenti
    {
        public bool Esportata;
        public void Esportazione()
        {
            InternalEsporta();
        }

        private void InternalEsporta()
        {
            Zero5.Util.Log.WriteLog("Formato esportazione: " + (int)Configurazioni.Esportazione_Formato);

            StringBuilder sb = new StringBuilder();
            SortedDictionary<string, RecordEsportazioneAvanzamentiERP> lstRecords = new SortedDictionary<string, RecordEsportazioneAvanzamentiERP>();
            //TODO: QUI
            CalcolaRecordEsportazione_Avanzamenti_DaTransazioni(lstRecords);

            ImpostaDatiAggiuntiviRecord(lstRecords);
            string prevRif = "";
            //DateTime prevData = DateTime.MinValue;
            //string prevRUomo = "";
            //string prevRMacchina = "";
           // double prevTempoMacchina = 0;
           // double prevTempoUomo = 0;

            foreach (KeyValuePair<string, RecordEsportazioneAvanzamentiERP> kvp in lstRecords)
            {

                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif)
                    //&& prevRUomo != kvp.Value.V01_ERP_CodiceRisorsaUomo)
                {
                    kvp.Value.Esportata = true;
                    //sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));
                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    
                    //prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    //prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    //prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    //Esportata = true;
                }
                /*
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRMacchina != kvp.Value.V01_ERP_CodMacchinaUff)
                {
                    sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));
                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    Esportata = true;
                }
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRUomo == kvp.Value.V01_ERP_CodiceRisorsaUomo && prevData != kvp.Value.V01_ERP_DataRegistrazione)
                {
                    sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));

                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    Esportata = true;
                }
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRMacchina == kvp.Value.V01_ERP_CodMacchinaUff && prevData != kvp.Value.V01_ERP_DataRegistrazione)
                {
                    sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));
                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    Esportata = true;
                }
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRUomo == kvp.Value.V01_ERP_CodiceRisorsaUomo && prevData == kvp.Value.V01_ERP_DataRegistrazione)
                {
                    prevTempoMacchina += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    prevTempoUomo += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    Esportata = true;
                }
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRMacchina == kvp.Value.V01_ERP_CodMacchinaUff && prevData == kvp.Value.V01_ERP_DataRegistrazione)
                {
                    prevTempoMacchina += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    prevTempoUomo += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    Esportata = true;
                }

                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRUomo == kvp.Value.V01_ERP_CodiceRisorsaUomo && prevRMacchina == kvp.Value.V01_ERP_CodMacchinaUff && prevData == kvp.Value.V01_ERP_DataRegistrazione)
                {
                    prevTempoMacchina += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    prevTempoUomo += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    Esportata = true;
                }
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione == prevRif && prevRUomo == kvp.Value.V01_ERP_CodiceRisorsaUomo && prevRMacchina == kvp.Value.V01_ERP_CodMacchinaUff && prevData != kvp.Value.V01_ERP_DataRegistrazione)
                {
                    sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));
                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    prevTempoMacchina += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    prevTempoUomo += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    Esportata = true;
                }
                */
                if (kvp.Value.V01_ERP_RiferimentoOrdineProduzione != prevRif)
                {
                    sb.AppendLine(kvp.Value.FormatToCsvString(Configurazioni.Esportazione_Formato));
                    kvp.Value.Esportata = true;
                    prevRif = kvp.Value.V01_ERP_RiferimentoOrdineProduzione;
                    Esportata = true;
                    // prevRUomo = kvp.Value.V01_ERP_CodiceRisorsaUomo;
                    //prevData = kvp.Value.V01_ERP_DataRegistrazione;
                    //prevRMacchina = kvp.Value.V01_ERP_CodMacchinaUff;
                    //prevTempoMacchina += kvp.Value.V01_ERP_TConsMachRealeUltima;
                    //prevTempoUomo += kvp.Value.V01_ERP_TConsMachRealeUltima;
                }
            }

            string content = sb.ToString();

            if (content.Length > 0)
            {
                if (Configurazioni.ModalitaIntegrazioneEsolver == eTipoScambioDatiEsolver.InCloud)
                {
                    if (!Common.POSTAvanzamentiAVP(content))
                        throw new Exception("Errore esportazione AVP via ws");
                }
                else
                {
                    //TODO: phasetmp negato in locale, da impostare uno personale
                    string fileEsportazioneAvanzamenti = "\\\\192.168.2.3\\phase\\IMPORT\\Importphase" + ".txt";
                    //string fileEsportazioneAvanzamenti = "C:\\Users\\VincenzoGiacalone\\Desktop\\JobsonTest\\JobsonPhase" + ".txt";
                    System.IO.File.AppendAllText(fileEsportazioneAvanzamenti, content);

                    if (System.IO.File.Exists(fileEsportazioneAvanzamenti))
                    {
                      Zero5.Util.Log.WriteLog("file già esistente nella cartella di destinazione");
                    }
                }

                foreach (KeyValuePair<string, RecordEsportazioneAvanzamentiERP> kvp in lstRecords)
                {
                    try
                    {
                        if (kvp.Value.Esportata)
                        {
                                kvp.Value.MarcaEsportateTransazioniCoinvolte();
                        }
                    }
                    catch (Exception ex)
                    {
                        Zero5.Util.Log.WriteLog("Eccezione salvataggio Esportato = 1 per " + kvp.Value.V01_ERP_RiferimentoOrdineProduzione + " :  " + ex.Message);
                    }
                }
            }
        }

        private void CalcolaRecordEsportazione_Avanzamenti_DaTransazioni(SortedDictionary<string, RecordEsportazioneAvanzamentiERP> records)
        {
            try
            {
                Zero5.Server.Produzione srvProd = new Zero5.Server.Produzione();
                List<int> lstCausaliDaEsportareDaConfigurazioniSistema = new List<int>();

                bool consideraAncheVersatiLogisticaEContabilizzati = false;
                try
                {
                    lstCausaliDaEsportareDaConfigurazioniSistema = new List<int>();
                    lstCausaliDaEsportareDaConfigurazioniSistema.AddRange(srvProd.CalcolaCausaliTransazioniDaEsportare());
                    if (lstCausaliDaEsportareDaConfigurazioniSistema.Count == 0)
                        lstCausaliDaEsportareDaConfigurazioniSistema.Add(-1);
                    consideraAncheVersatiLogisticaEContabilizzati = true;
                }
                catch (Exception ex)
                {
                    string versioneServer = Zero5.Data.Layer.Opzioni.helper.LoadStringValue(Opzioni.enumOpzioniID.InfoServer_VersionePhaseServer);
                    Zero5.Util.Log.WriteLog("Versione PhaseServer installata " + versioneServer + ". Aggiornare a >=605 per personalizzare le causali da esportare.");
                }

                Zero5.Data.Layer.CausaliAttivita causali = new CausaliAttivita();
                causali.Load(causali.Fields.IDCausaleAttivita != Common.CausaleVersamentoDaGestionale);

                List<int> lstCausaliPezzi = new List<int>();
                List<int> lstCausaliTempo = new List<int>();

                while (!causali.EOF)
                {
                    if (lstCausaliDaEsportareDaConfigurazioniSistema.Count == 0 || lstCausaliDaEsportareDaConfigurazioniSistema.Contains(causali.IDCausaleAttivita))
                    {

                        if (causali.QuantitaSuFase == CausaliAttivita.enumQuantitaSuFase.ContatiLavorazione ||
                            causali.QuantitaSuFase == CausaliAttivita.enumQuantitaSuFase.ContatiAvviamento ||
                            causali.QuantitaSuFase == CausaliAttivita.enumQuantitaSuFase.ContatiSetup ||
                            (consideraAncheVersatiLogisticaEContabilizzati && causali.QuantitaSuFase == CausaliAttivita.enumQuantitaSuFase.Contabilizzati) ||
                           (consideraAncheVersatiLogisticaEContabilizzati && causali.QuantitaSuFase == CausaliAttivita.enumQuantitaSuFase.Logistica))
                        {
                            lstCausaliPezzi.Add(causali.IDCausaleAttivita);
                        }

                        if (causali.TempiMacchinaSuFase == CausaliAttivita.enumTempiMacchinaSuFase.Avviamento ||
                            causali.TempiMacchinaSuFase == CausaliAttivita.enumTempiMacchinaSuFase.CicloFermoOperativo ||
                            causali.TempiMacchinaSuFase == CausaliAttivita.enumTempiMacchinaSuFase.CicloLavorazione |
                            causali.TempiMacchinaSuFase == CausaliAttivita.enumTempiMacchinaSuFase.FermoNonOperativo ||
                            causali.TempiUomoSuFase == CausaliAttivita.enumTempiUomoSuFase.Avviamento ||
                            causali.TempiUomoSuFase == CausaliAttivita.enumTempiUomoSuFase.CicloLavorazione ||
                            causali.TempiUomoSuFase == CausaliAttivita.enumTempiUomoSuFase.Setup)
                        {
                            lstCausaliTempo.Add(causali.IDCausaleAttivita);
                        }
                    }
                    causali.MoveNext();
                }

                if (lstCausaliPezzi.Count == 0)
                    lstCausaliPezzi.Add(-1);

                if (lstCausaliTempo.Count == 0)
                    lstCausaliTempo.Add(-1);

                Zero5.Data.Layer.vOrdiniProduzioneFasiProduzioneTransazioni transazioniDaEsportare = new Zero5.Data.Layer.vOrdiniProduzioneFasiProduzioneTransazioni();
                Zero5.Data.Filter.Filter filtro = new Zero5.Data.Filter.Filter();
                filtro.Add(transazioniDaEsportare.Fields.Transazione_Esportato == 0);
                filtro.Add(transazioniDaEsportare.Fields.Transazione_Fine.FilterNotIsNull());
                filtro.AddOrderBy(transazioniDaEsportare.Fields.Ordine_IDArticolo);
                filtro.AddOrderBy(transazioniDaEsportare.Fields.Ordine_IDOrdineProduzione);
                filtro.AddOrderBy(transazioniDaEsportare.Fields.Fase_IDFaseProduzione);
                filtro.AddOrderBy(transazioniDaEsportare.Fields.Transazione_Inizio);

                transazioniDaEsportare.Load(filtro);
                Zero5.Util.Log.WriteLog("trovate " + transazioniDaEsportare.RowCount + " transazioni da esportare");

                if (transazioniDaEsportare.RowCount > 10000)
                {
                    Zero5.Util.Log.WriteLog("CalcoloRecordEsportazione_Avanzamenti_DaTransazioni >10000 elementi :" + filtro.ToStringHumanized());
                }

                if (transazioniDaEsportare.EOF || transazioniDaEsportare.RowCount == 0)
                    return;

                while (!transazioniDaEsportare.EOF)
                {
                    try
                    {
                        string key = CalcolaChiaveRecord(transazioniDaEsportare);


                        if (!records.ContainsKey(key))
                            records.Add(key, new RecordEsportazioneAvanzamentiERP(transazioniDaEsportare));

/*
                               
                        if (transazioniDaEsportare.Transazione_Causale == 205)
                        {
                            records[key].V01_ERP_MinutiLavoratiMacchina += transazioniDaEsportare.Transazione_Minuti;
                        }
                        else
                        {
                            records[key].V01_ERP_MinutiLavoratiUomo += transazioniDaEsportare.Transazione_Minuti;
                        }
*/
                        if (!records[key].Phase_lstTransazioniCoinvolte.Contains(transazioniDaEsportare.Transazione_IDTransazione))
                            records[key].Phase_lstTransazioniCoinvolte.Add(transazioniDaEsportare.Transazione_IDTransazione);

                            //records[key].V01_ERP_MinutiLavorati.Add(transazioniDaEsportare.Transazione_Minuti);

                    }
                    catch (Exception ex)
                    {
                        Zero5.Util.Log.WriteLog("Exc. on CalcolaRecordEsportazione_Avanzamenti_DaTransazioni. Transazione " + transazioniDaEsportare.Transazione_IDTransazione + " " + ex.Message);
                    }
                    transazioniDaEsportare.MoveNext();
                }
            }
            catch (ArgumentException ex)
            {
                Zero5.Util.Log.WriteLog("Exc. on CalcolaRecordEsportazione_Avanzamenti_DaTransazioni. " + ex.Message);
            }
        }


        private string CalcolaChiaveRecord(Zero5.Data.Layer.vOrdiniProduzioneFasiProduzioneTransazioni transazione)
        {
            return transazione.Transazione_Inizio.ToString("ddMMyyyy") + "_" +
                                  transazione.Fase_IDFaseProduzione + "_" +                                  
                                  transazione.Transazione_IDRisorsaMacchina + "_" +
                                  transazione.Transazione_IDRisorsaUomo + "_" +
                                  transazione.Transazione_Causale;
        }

        private void ImpostaDatiAggiuntiviRecord(SortedDictionary<string, RecordEsportazioneAvanzamentiERP> lstRecord)
        {
            List<int> idArticoliCoinvolti = new List<int>();
            List<int> idRigheDistintaCoinvolte = new List<int>();
            foreach (KeyValuePair<string, RecordEsportazioneAvanzamentiERP> kvp in lstRecord)
            {
 
                    if (!idArticoliCoinvolti.Contains(kvp.Value.Phase_IdArticolo))
                        idArticoliCoinvolti.Add(kvp.Value.Phase_IdArticolo);

                    if (!idRigheDistintaCoinvolte.Contains(kvp.Value.Phase_idRigaDistinta))
                        idRigheDistintaCoinvolte.Add(kvp.Value.Phase_idRigaDistinta);
                
                
            }

            Zero5.Data.Layer.Risorse risorse = new Risorse();
            risorse.LoadAll();
            Zero5.Data.Layer.Risorse macchinePhase = new Risorse();
            macchinePhase.LoadAll();

            Zero5.Data.Layer.Articoli articoli = new Articoli();
            {
                Zero5.Data.Filter.Filter fil = new Zero5.Data.Filter.Filter();
                fil.Add(articoli.Fields.IDArticolo.FilterIn(idArticoliCoinvolti));
                articoli.Load(fil);
            }

            Zero5.Data.Layer.DistintaBase righeDistinta = new DistintaBase();
            {
                if (idRigheDistintaCoinvolte.Count > 0)
                {
                    Zero5.Data.Filter.Filter fil = new Zero5.Data.Filter.Filter();
                    fil.Add(righeDistinta.Fields.IDDistintaBase.FilterIn(idRigheDistintaCoinvolte));
                    righeDistinta.Load(fil);
                }
            }

            CausaliAttivita causaliTransazioni = new CausaliAttivita();
            causaliTransazioni.LoadAll();

            CausaliMovimento causaliMovimento = new CausaliMovimento();
            causaliMovimento.LoadAll();

            DateTime dtRef = DateTime.MinValue;
            int idFaseProd = 0;

            foreach (KeyValuePair<string, RecordEsportazioneAvanzamentiERP> kvp in lstRecord)
            {
                articoli.MoveToPrimaryKey(kvp.Value.Phase_IdArticolo);

                {
                    string[] tokenCodiceArticolo = articoli.CodiceArticolo.Split('_');
                    if (!Shared.Configurazioni.Esportazione_ForzaEsclusioneVariante
                        && tokenCodiceArticolo.Length > 1
                        )
                        kvp.Value.V01_ERP_CodiceVarianteArticolo = tokenCodiceArticolo[tokenCodiceArticolo.Length - 1];
                }

                risorse.MoveToPrimaryKey(kvp.Value.V01_ERP_CodiceRisorsaUomo1);
                kvp.Value.V01_ERP_CodiceRisorsaUomo = risorse.CodiceEsterno;
  //              records[key].V01_ERP_MinutiLavoratiUomo += transazioniDaEsportare.Transazione_Minuti;


                macchinePhase.MoveToPrimaryKey(kvp.Value.V01_ERP_CodMacchinaReale);
                kvp.Value.V01_ERP_CodMacchinaUff = macchinePhase.CodiceEsterno;
 //               records[key].V01_ERP_MinutiLavoratiMacchina += transazioniDaEsportare.Transazione_Minuti;



                if (kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.ConsumoMateriali ||
                    kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.Addebito ||
                    kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.Reso)
                {
                    causaliMovimento.MoveToPrimaryKey(kvp.Value.Phase_IdCausale);
                    kvp.Value.V01_ERP_CodiceCausale = causaliMovimento.CodiceEsterno;
                        
                    if (kvp.Value.Phase_idRigaDistinta != 0)
                    {
                        righeDistinta.MoveToPrimaryKey(kvp.Value.Phase_idRigaDistinta);
                        kvp.Value.V01_ERP_RiferimentoOrdineProduzione = righeDistinta.RiferimentoEsterno;
                    }
                    else if (kvp.Value.Phase_IdFaseProduzione != 0)
                    {
                        Zero5.Data.Layer.FasiProduzione fp = new FasiProduzione();
                        fp.LoadByPrimaryKey(kvp.Value.Phase_IdFaseProduzione);
                        kvp.Value.V01_ERP_RiferimentoOrdineProduzione = fp.CodiceEsterno;
                    }
                }
                else if (kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.AvanzamentoFase ||
                   kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.ConsuntivazioneOre)
                {
                    causaliTransazioni.MoveToPrimaryKey(kvp.Value.Phase_IdCausale);
                    kvp.Value.V01_ERP_CodiceCausale = causaliTransazioni.CodiceEsterno;
                }
                if (kvp.Value.V01_ERP_TipoOperazioneAvanzamento == eTipoOperazione_ERP.FlagSaldoFase)
                    kvp.Value.V01_ERP_TipoRecord_TrueTesta_FalseRiga = eTipoRecord_ERP.Testa;

                if (kvp.Value.V01_ERP_DataRegistrazione.Date != dtRef.Date || kvp.Value.Phase_IdFaseProduzione != idFaseProd)
                {
                    kvp.Value.V01_ERP_TipoRecord_TrueTesta_FalseRiga = eTipoRecord_ERP.Testa;
                    dtRef = kvp.Value.V01_ERP_DataRegistrazione;
                    idFaseProd = kvp.Value.Phase_IdFaseProduzione;
                }
            }
        }
    }
}
