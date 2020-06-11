using CareStream.LoggerService;
using CareStream.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CareStream.Utility
{
    public class CountryService
    {
        private readonly ILoggerManager _logger;
        public CountryService(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task<Countries> GetCountries()
        {
            Countries retVal = null;
            try
            {
                CountryJson countryJson = null;
                HttpClient httpClient = new HttpClient();
                var response = await httpClient.GetAsync(new Uri("https://api.first.org/data/v1/countries?limit=255&pretty=true"));

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                     countryJson = JsonConvert.DeserializeObject<CountryJson>(data);

                }

                
                if(countryJson != null)
                {
                    if(countryJson.data != null)
                    {
                        retVal = new Countries();
                        retVal.CountryModel = new List<Country>();

                        #region Country
                        if (countryJson.data.AD != null) { retVal.CountryModel.Add(new Country { CountryCode = "AD", CountryName = countryJson.data.AD.country, Region = countryJson.data.AD.region }); }
                        if (countryJson.data.AE != null) { retVal.CountryModel.Add(new Country { CountryCode = "AE", CountryName = countryJson.data.AE.country, Region = countryJson.data.AE.region }); }
                        if (countryJson.data.AF != null) { retVal.CountryModel.Add(new Country { CountryCode = "AF", CountryName = countryJson.data.AF.country, Region = countryJson.data.AF.region }); }
                        if (countryJson.data.AG != null) { retVal.CountryModel.Add(new Country { CountryCode = "AG", CountryName = countryJson.data.AG.country, Region = countryJson.data.AG.region }); }
                        if (countryJson.data.AI != null) { retVal.CountryModel.Add(new Country { CountryCode = "AI", CountryName = countryJson.data.AI.country, Region = countryJson.data.AI.region }); }
                        if (countryJson.data.AL != null) { retVal.CountryModel.Add(new Country { CountryCode = "AL", CountryName = countryJson.data.AL.country, Region = countryJson.data.AL.region }); }
                        if (countryJson.data.AM != null) { retVal.CountryModel.Add(new Country { CountryCode = "AM", CountryName = countryJson.data.AM.country, Region = countryJson.data.AM.region }); }
                        if (countryJson.data.AN != null) { retVal.CountryModel.Add(new Country { CountryCode = "AN", CountryName = countryJson.data.AN.country, Region = countryJson.data.AN.region }); }
                        if (countryJson.data.AO != null) { retVal.CountryModel.Add(new Country { CountryCode = "AO", CountryName = countryJson.data.AO.country, Region = countryJson.data.AO.region }); }
                        if (countryJson.data.AQ != null) { retVal.CountryModel.Add(new Country { CountryCode = "AQ", CountryName = countryJson.data.AQ.country, Region = countryJson.data.AQ.region }); }
                        if (countryJson.data.AR != null) { retVal.CountryModel.Add(new Country { CountryCode = "AR", CountryName = countryJson.data.AR.country, Region = countryJson.data.AR.region }); }
                        if (countryJson.data.AS != null) { retVal.CountryModel.Add(new Country { CountryCode = "AS", CountryName = countryJson.data.AS.country, Region = countryJson.data.AS.region }); }
                        if (countryJson.data.AT != null) { retVal.CountryModel.Add(new Country { CountryCode = "AT", CountryName = countryJson.data.AT.country, Region = countryJson.data.AT.region }); }
                        if (countryJson.data.AU != null) { retVal.CountryModel.Add(new Country { CountryCode = "AU", CountryName = countryJson.data.AU.country, Region = countryJson.data.AU.region }); }
                        if (countryJson.data.AW != null) { retVal.CountryModel.Add(new Country { CountryCode = "AW", CountryName = countryJson.data.AW.country, Region = countryJson.data.AW.region }); }
                        if (countryJson.data.AX != null) { retVal.CountryModel.Add(new Country { CountryCode = "AX", CountryName = countryJson.data.AX.country, Region = countryJson.data.AX.region }); }
                        if (countryJson.data.AZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "AZ", CountryName = countryJson.data.AZ.country, Region = countryJson.data.AZ.region }); }
                        if (countryJson.data.BA != null) { retVal.CountryModel.Add(new Country { CountryCode = "BA", CountryName = countryJson.data.BA.country, Region = countryJson.data.BA.region }); }
                        if (countryJson.data.BB != null) { retVal.CountryModel.Add(new Country { CountryCode = "BB", CountryName = countryJson.data.BB.country, Region = countryJson.data.BB.region }); }
                        if (countryJson.data.BD != null) { retVal.CountryModel.Add(new Country { CountryCode = "BD", CountryName = countryJson.data.BD.country, Region = countryJson.data.BD.region }); }
                        if (countryJson.data.BE != null) { retVal.CountryModel.Add(new Country { CountryCode = "BE", CountryName = countryJson.data.BE.country, Region = countryJson.data.BE.region }); }
                        if (countryJson.data.BF != null) { retVal.CountryModel.Add(new Country { CountryCode = "BF", CountryName = countryJson.data.BF.country, Region = countryJson.data.BF.region }); }
                        if (countryJson.data.BG != null) { retVal.CountryModel.Add(new Country { CountryCode = "BG", CountryName = countryJson.data.BG.country, Region = countryJson.data.BG.region }); }
                        if (countryJson.data.BH != null) { retVal.CountryModel.Add(new Country { CountryCode = "BH", CountryName = countryJson.data.BH.country, Region = countryJson.data.BH.region }); }
                        if (countryJson.data.BI != null) { retVal.CountryModel.Add(new Country { CountryCode = "BI", CountryName = countryJson.data.BI.country, Region = countryJson.data.BI.region }); }
                        if (countryJson.data.BJ != null) { retVal.CountryModel.Add(new Country { CountryCode = "BJ", CountryName = countryJson.data.BJ.country, Region = countryJson.data.BJ.region }); }
                        if (countryJson.data.BL != null) { retVal.CountryModel.Add(new Country { CountryCode = "BL", CountryName = countryJson.data.BL.country, Region = countryJson.data.BL.region }); }
                        if (countryJson.data.BM != null) { retVal.CountryModel.Add(new Country { CountryCode = "BM", CountryName = countryJson.data.BM.country, Region = countryJson.data.BM.region }); }
                        if (countryJson.data.BN != null) { retVal.CountryModel.Add(new Country { CountryCode = "BN", CountryName = countryJson.data.BN.country, Region = countryJson.data.BN.region }); }
                        if (countryJson.data.BO != null) { retVal.CountryModel.Add(new Country { CountryCode = "BO", CountryName = countryJson.data.BO.country, Region = countryJson.data.BO.region }); }
                        if (countryJson.data.BQ != null) { retVal.CountryModel.Add(new Country { CountryCode = "BQ", CountryName = countryJson.data.BQ.country, Region = countryJson.data.BQ.region }); }
                        if (countryJson.data.BR != null) { retVal.CountryModel.Add(new Country { CountryCode = "BR", CountryName = countryJson.data.BR.country, Region = countryJson.data.BR.region }); }
                        if (countryJson.data.BS != null) { retVal.CountryModel.Add(new Country { CountryCode = "BS", CountryName = countryJson.data.BS.country, Region = countryJson.data.BS.region }); }
                        if (countryJson.data.BT != null) { retVal.CountryModel.Add(new Country { CountryCode = "BT", CountryName = countryJson.data.BT.country, Region = countryJson.data.BT.region }); }
                        if (countryJson.data.BV != null) { retVal.CountryModel.Add(new Country { CountryCode = "BV", CountryName = countryJson.data.BV.country, Region = countryJson.data.BV.region }); }
                        if (countryJson.data.BW != null) { retVal.CountryModel.Add(new Country { CountryCode = "BW", CountryName = countryJson.data.BW.country, Region = countryJson.data.BW.region }); }
                        if (countryJson.data.BY != null) { retVal.CountryModel.Add(new Country { CountryCode = "BY", CountryName = countryJson.data.BY.country, Region = countryJson.data.BY.region }); }
                        if (countryJson.data.BZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "BZ", CountryName = countryJson.data.BZ.country, Region = countryJson.data.BZ.region }); }
                        if (countryJson.data.CA != null) { retVal.CountryModel.Add(new Country { CountryCode = "CA", CountryName = countryJson.data.CA.country, Region = countryJson.data.CA.region }); }
                        if (countryJson.data.CC != null) { retVal.CountryModel.Add(new Country { CountryCode = "CC", CountryName = countryJson.data.CC.country, Region = countryJson.data.CC.region }); }
                        if (countryJson.data.CD != null) { retVal.CountryModel.Add(new Country { CountryCode = "CD", CountryName = countryJson.data.CD.country, Region = countryJson.data.CD.region }); }
                        if (countryJson.data.CF != null) { retVal.CountryModel.Add(new Country { CountryCode = "CF", CountryName = countryJson.data.CF.country, Region = countryJson.data.CF.region }); }
                        if (countryJson.data.CG != null) { retVal.CountryModel.Add(new Country { CountryCode = "CG", CountryName = countryJson.data.CG.country, Region = countryJson.data.CG.region }); }
                        if (countryJson.data.CH != null) { retVal.CountryModel.Add(new Country { CountryCode = "CH", CountryName = countryJson.data.CH.country, Region = countryJson.data.CH.region }); }
                        if (countryJson.data.CI != null) { retVal.CountryModel.Add(new Country { CountryCode = "CI", CountryName = countryJson.data.CI.country, Region = countryJson.data.CI.region }); }
                        if (countryJson.data.CK != null) { retVal.CountryModel.Add(new Country { CountryCode = "CK", CountryName = countryJson.data.CK.country, Region = countryJson.data.CK.region }); }
                        if (countryJson.data.CL != null) { retVal.CountryModel.Add(new Country { CountryCode = "CL", CountryName = countryJson.data.CL.country, Region = countryJson.data.CL.region }); }
                        if (countryJson.data.CM != null) { retVal.CountryModel.Add(new Country { CountryCode = "CM", CountryName = countryJson.data.CM.country, Region = countryJson.data.CM.region }); }
                        if (countryJson.data.CN != null) { retVal.CountryModel.Add(new Country { CountryCode = "CN", CountryName = countryJson.data.CN.country, Region = countryJson.data.CN.region }); }
                        if (countryJson.data.CO != null) { retVal.CountryModel.Add(new Country { CountryCode = "CO", CountryName = countryJson.data.CO.country, Region = countryJson.data.CO.region }); }
                        if (countryJson.data.CR != null) { retVal.CountryModel.Add(new Country { CountryCode = "CR", CountryName = countryJson.data.CR.country, Region = countryJson.data.CR.region }); }
                        if (countryJson.data.CU != null) { retVal.CountryModel.Add(new Country { CountryCode = "CU", CountryName = countryJson.data.CU.country, Region = countryJson.data.CU.region }); }
                        if (countryJson.data.CV != null) { retVal.CountryModel.Add(new Country { CountryCode = "CV", CountryName = countryJson.data.CV.country, Region = countryJson.data.CV.region }); }
                        if (countryJson.data.CW != null) { retVal.CountryModel.Add(new Country { CountryCode = "CW", CountryName = countryJson.data.CW.country, Region = countryJson.data.CW.region }); }
                        if (countryJson.data.CX != null) { retVal.CountryModel.Add(new Country { CountryCode = "CX", CountryName = countryJson.data.CX.country, Region = countryJson.data.CX.region }); }
                        if (countryJson.data.CY != null) { retVal.CountryModel.Add(new Country { CountryCode = "CY", CountryName = countryJson.data.CY.country, Region = countryJson.data.CY.region }); }
                        if (countryJson.data.CZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "CZ", CountryName = countryJson.data.CZ.country, Region = countryJson.data.CZ.region }); }
                        if (countryJson.data.DE != null) { retVal.CountryModel.Add(new Country { CountryCode = "DE", CountryName = countryJson.data.DE.country, Region = countryJson.data.DE.region }); }
                        if (countryJson.data.DJ != null) { retVal.CountryModel.Add(new Country { CountryCode = "DJ", CountryName = countryJson.data.DJ.country, Region = countryJson.data.DJ.region }); }
                        if (countryJson.data.DK != null) { retVal.CountryModel.Add(new Country { CountryCode = "DK", CountryName = countryJson.data.DK.country, Region = countryJson.data.DK.region }); }
                        if (countryJson.data.DM != null) { retVal.CountryModel.Add(new Country { CountryCode = "DM", CountryName = countryJson.data.DM.country, Region = countryJson.data.DM.region }); }
                        if (countryJson.data.DO != null) { retVal.CountryModel.Add(new Country { CountryCode = "DO", CountryName = countryJson.data.DO.country, Region = countryJson.data.DO.region }); }
                        if (countryJson.data.DZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "DZ", CountryName = countryJson.data.DZ.country, Region = countryJson.data.DZ.region }); }
                        if (countryJson.data.EC != null) { retVal.CountryModel.Add(new Country { CountryCode = "EC", CountryName = countryJson.data.EC.country, Region = countryJson.data.EC.region }); }
                        if (countryJson.data.EE != null) { retVal.CountryModel.Add(new Country { CountryCode = "EE", CountryName = countryJson.data.EE.country, Region = countryJson.data.EE.region }); }
                        if (countryJson.data.EG != null) { retVal.CountryModel.Add(new Country { CountryCode = "EG", CountryName = countryJson.data.EG.country, Region = countryJson.data.EG.region }); }
                        if (countryJson.data.EH != null) { retVal.CountryModel.Add(new Country { CountryCode = "EH", CountryName = countryJson.data.EH.country, Region = countryJson.data.EH.region }); }
                        if (countryJson.data.ER != null) { retVal.CountryModel.Add(new Country { CountryCode = "ER", CountryName = countryJson.data.ER.country, Region = countryJson.data.ER.region }); }
                        if (countryJson.data.ES != null) { retVal.CountryModel.Add(new Country { CountryCode = "ES", CountryName = countryJson.data.ES.country, Region = countryJson.data.ES.region }); }
                        if (countryJson.data.ET != null) { retVal.CountryModel.Add(new Country { CountryCode = "ET", CountryName = countryJson.data.ET.country, Region = countryJson.data.ET.region }); }
                        if (countryJson.data.EU != null) { retVal.CountryModel.Add(new Country { CountryCode = "EU", CountryName = countryJson.data.EU.country, Region = countryJson.data.EU.region }); }
                        if (countryJson.data.FI != null) { retVal.CountryModel.Add(new Country { CountryCode = "FI", CountryName = countryJson.data.FI.country, Region = countryJson.data.FI.region }); }
                        if (countryJson.data.FJ != null) { retVal.CountryModel.Add(new Country { CountryCode = "FJ", CountryName = countryJson.data.FJ.country, Region = countryJson.data.FJ.region }); }
                        if (countryJson.data.FK != null) { retVal.CountryModel.Add(new Country { CountryCode = "FK", CountryName = countryJson.data.FK.country, Region = countryJson.data.FK.region }); }
                        if (countryJson.data.FM != null) { retVal.CountryModel.Add(new Country { CountryCode = "FM", CountryName = countryJson.data.FM.country, Region = countryJson.data.FM.region }); }
                        if (countryJson.data.FO != null) { retVal.CountryModel.Add(new Country { CountryCode = "FO", CountryName = countryJson.data.FO.country, Region = countryJson.data.FO.region }); }
                        if (countryJson.data.FR != null) { retVal.CountryModel.Add(new Country { CountryCode = "FR", CountryName = countryJson.data.FR.country, Region = countryJson.data.FR.region }); }
                        if (countryJson.data.GA != null) { retVal.CountryModel.Add(new Country { CountryCode = "GA", CountryName = countryJson.data.GA.country, Region = countryJson.data.GA.region }); }
                        if (countryJson.data.GB != null) { retVal.CountryModel.Add(new Country { CountryCode = "GB", CountryName = countryJson.data.GB.country, Region = countryJson.data.GB.region }); }
                        if (countryJson.data.GD != null) { retVal.CountryModel.Add(new Country { CountryCode = "GD", CountryName = countryJson.data.GD.country, Region = countryJson.data.GD.region }); }
                        if (countryJson.data.GE != null) { retVal.CountryModel.Add(new Country { CountryCode = "GE", CountryName = countryJson.data.GE.country, Region = countryJson.data.GE.region }); }
                        if (countryJson.data.GF != null) { retVal.CountryModel.Add(new Country { CountryCode = "GF", CountryName = countryJson.data.GF.country, Region = countryJson.data.GF.region }); }
                        if (countryJson.data.GG != null) { retVal.CountryModel.Add(new Country { CountryCode = "GG", CountryName = countryJson.data.GG.country, Region = countryJson.data.GG.region }); }
                        if (countryJson.data.GH != null) { retVal.CountryModel.Add(new Country { CountryCode = "GH", CountryName = countryJson.data.GH.country, Region = countryJson.data.GH.region }); }
                        if (countryJson.data.GI != null) { retVal.CountryModel.Add(new Country { CountryCode = "GI", CountryName = countryJson.data.GI.country, Region = countryJson.data.GI.region }); }
                        if (countryJson.data.GL != null) { retVal.CountryModel.Add(new Country { CountryCode = "GL", CountryName = countryJson.data.GL.country, Region = countryJson.data.GL.region }); }
                        if (countryJson.data.GM != null) { retVal.CountryModel.Add(new Country { CountryCode = "GM", CountryName = countryJson.data.GM.country, Region = countryJson.data.GM.region }); }
                        if (countryJson.data.GN != null) { retVal.CountryModel.Add(new Country { CountryCode = "GN", CountryName = countryJson.data.GN.country, Region = countryJson.data.GN.region }); }
                        if (countryJson.data.GP != null) { retVal.CountryModel.Add(new Country { CountryCode = "GP", CountryName = countryJson.data.GP.country, Region = countryJson.data.GP.region }); }
                        if (countryJson.data.GQ != null) { retVal.CountryModel.Add(new Country { CountryCode = "GQ", CountryName = countryJson.data.GQ.country, Region = countryJson.data.GQ.region }); }
                        if (countryJson.data.GR != null) { retVal.CountryModel.Add(new Country { CountryCode = "GR", CountryName = countryJson.data.GR.country, Region = countryJson.data.GR.region }); }
                        if (countryJson.data.GS != null) { retVal.CountryModel.Add(new Country { CountryCode = "GS", CountryName = countryJson.data.GS.country, Region = countryJson.data.GS.region }); }
                        if (countryJson.data.GT != null) { retVal.CountryModel.Add(new Country { CountryCode = "GT", CountryName = countryJson.data.GT.country, Region = countryJson.data.GT.region }); }
                        if (countryJson.data.GU != null) { retVal.CountryModel.Add(new Country { CountryCode = "GU", CountryName = countryJson.data.GU.country, Region = countryJson.data.GU.region }); }
                        if (countryJson.data.GW != null) { retVal.CountryModel.Add(new Country { CountryCode = "GW", CountryName = countryJson.data.GW.country, Region = countryJson.data.GW.region }); }
                        if (countryJson.data.GY != null) { retVal.CountryModel.Add(new Country { CountryCode = "GY", CountryName = countryJson.data.GY.country, Region = countryJson.data.GY.region }); }
                        if (countryJson.data.HK != null) { retVal.CountryModel.Add(new Country { CountryCode = "HK", CountryName = countryJson.data.HK.country, Region = countryJson.data.HK.region }); }
                        if (countryJson.data.HM != null) { retVal.CountryModel.Add(new Country { CountryCode = "HM", CountryName = countryJson.data.HM.country, Region = countryJson.data.HM.region }); }
                        if (countryJson.data.HN != null) { retVal.CountryModel.Add(new Country { CountryCode = "HN", CountryName = countryJson.data.HN.country, Region = countryJson.data.HN.region }); }
                        if (countryJson.data.HR != null) { retVal.CountryModel.Add(new Country { CountryCode = "HR", CountryName = countryJson.data.HR.country, Region = countryJson.data.HR.region }); }
                        if (countryJson.data.HT != null) { retVal.CountryModel.Add(new Country { CountryCode = "HT", CountryName = countryJson.data.HT.country, Region = countryJson.data.HT.region }); }
                        if (countryJson.data.HU != null) { retVal.CountryModel.Add(new Country { CountryCode = "HU", CountryName = countryJson.data.HU.country, Region = countryJson.data.HU.region }); }
                        if (countryJson.data.ID != null) { retVal.CountryModel.Add(new Country { CountryCode = "ID", CountryName = countryJson.data.ID.country, Region = countryJson.data.ID.region }); }
                        if (countryJson.data.IE != null) { retVal.CountryModel.Add(new Country { CountryCode = "IE", CountryName = countryJson.data.IE.country, Region = countryJson.data.IE.region }); }
                        if (countryJson.data.IL != null) { retVal.CountryModel.Add(new Country { CountryCode = "IL", CountryName = countryJson.data.IL.country, Region = countryJson.data.IL.region }); }
                        if (countryJson.data.IM != null) { retVal.CountryModel.Add(new Country { CountryCode = "IM", CountryName = countryJson.data.IM.country, Region = countryJson.data.IM.region }); }
                        if (countryJson.data.IN != null) { retVal.CountryModel.Add(new Country { CountryCode = "IN", CountryName = countryJson.data.IN.country, Region = countryJson.data.IN.region }); }
                        if (countryJson.data.IO != null) { retVal.CountryModel.Add(new Country { CountryCode = "IO", CountryName = countryJson.data.IO.country, Region = countryJson.data.IO.region }); }
                        if (countryJson.data.IQ != null) { retVal.CountryModel.Add(new Country { CountryCode = "IQ", CountryName = countryJson.data.IQ.country, Region = countryJson.data.IQ.region }); }
                        if (countryJson.data.IR != null) { retVal.CountryModel.Add(new Country { CountryCode = "IR", CountryName = countryJson.data.IR.country, Region = countryJson.data.IR.region }); }
                        if (countryJson.data.IS != null) { retVal.CountryModel.Add(new Country { CountryCode = "IS", CountryName = countryJson.data.IS.country, Region = countryJson.data.IS.region }); }
                        if (countryJson.data.IT != null) { retVal.CountryModel.Add(new Country { CountryCode = "IT", CountryName = countryJson.data.IT.country, Region = countryJson.data.IT.region }); }
                        if (countryJson.data.JE != null) { retVal.CountryModel.Add(new Country { CountryCode = "JE", CountryName = countryJson.data.JE.country, Region = countryJson.data.JE.region }); }
                        if (countryJson.data.JM != null) { retVal.CountryModel.Add(new Country { CountryCode = "JM", CountryName = countryJson.data.JM.country, Region = countryJson.data.JM.region }); }
                        if (countryJson.data.JO != null) { retVal.CountryModel.Add(new Country { CountryCode = "JO", CountryName = countryJson.data.JO.country, Region = countryJson.data.JO.region }); }
                        if (countryJson.data.JP != null) { retVal.CountryModel.Add(new Country { CountryCode = "JP", CountryName = countryJson.data.JP.country, Region = countryJson.data.JP.region }); }
                        if (countryJson.data.KE != null) { retVal.CountryModel.Add(new Country { CountryCode = "KE", CountryName = countryJson.data.KE.country, Region = countryJson.data.KE.region }); }
                        if (countryJson.data.KG != null) { retVal.CountryModel.Add(new Country { CountryCode = "KG", CountryName = countryJson.data.KG.country, Region = countryJson.data.KG.region }); }
                        if (countryJson.data.KH != null) { retVal.CountryModel.Add(new Country { CountryCode = "KH", CountryName = countryJson.data.KH.country, Region = countryJson.data.KH.region }); }
                        if (countryJson.data.KI != null) { retVal.CountryModel.Add(new Country { CountryCode = "KI", CountryName = countryJson.data.KI.country, Region = countryJson.data.KI.region }); }
                        if (countryJson.data.KM != null) { retVal.CountryModel.Add(new Country { CountryCode = "KM", CountryName = countryJson.data.KM.country, Region = countryJson.data.KM.region }); }
                        if (countryJson.data.KN != null) { retVal.CountryModel.Add(new Country { CountryCode = "KN", CountryName = countryJson.data.KN.country, Region = countryJson.data.KN.region }); }
                        if (countryJson.data.KP != null) { retVal.CountryModel.Add(new Country { CountryCode = "KP", CountryName = countryJson.data.KP.country, Region = countryJson.data.KP.region }); }
                        if (countryJson.data.KR != null) { retVal.CountryModel.Add(new Country { CountryCode = "KR", CountryName = countryJson.data.KR.country, Region = countryJson.data.KR.region }); }
                        if (countryJson.data.KW != null) { retVal.CountryModel.Add(new Country { CountryCode = "KW", CountryName = countryJson.data.KW.country, Region = countryJson.data.KW.region }); }
                        if (countryJson.data.KY != null) { retVal.CountryModel.Add(new Country { CountryCode = "KY", CountryName = countryJson.data.KY.country, Region = countryJson.data.KY.region }); }
                        if (countryJson.data.KZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "KZ", CountryName = countryJson.data.KZ.country, Region = countryJson.data.KZ.region }); }
                        if (countryJson.data.LA != null) { retVal.CountryModel.Add(new Country { CountryCode = "LA", CountryName = countryJson.data.LA.country, Region = countryJson.data.LA.region }); }
                        if (countryJson.data.LB != null) { retVal.CountryModel.Add(new Country { CountryCode = "LB", CountryName = countryJson.data.LB.country, Region = countryJson.data.LB.region }); }
                        if (countryJson.data.LC != null) { retVal.CountryModel.Add(new Country { CountryCode = "LC", CountryName = countryJson.data.LC.country, Region = countryJson.data.LC.region }); }
                        if (countryJson.data.LI != null) { retVal.CountryModel.Add(new Country { CountryCode = "LI", CountryName = countryJson.data.LI.country, Region = countryJson.data.LI.region }); }
                        if (countryJson.data.LK != null) { retVal.CountryModel.Add(new Country { CountryCode = "LK", CountryName = countryJson.data.LK.country, Region = countryJson.data.LK.region }); }
                        if (countryJson.data.LR != null) { retVal.CountryModel.Add(new Country { CountryCode = "LR", CountryName = countryJson.data.LR.country, Region = countryJson.data.LR.region }); }
                        if (countryJson.data.LS != null) { retVal.CountryModel.Add(new Country { CountryCode = "LS", CountryName = countryJson.data.LS.country, Region = countryJson.data.LS.region }); }
                        if (countryJson.data.LT != null) { retVal.CountryModel.Add(new Country { CountryCode = "LT", CountryName = countryJson.data.LT.country, Region = countryJson.data.LT.region }); }
                        if (countryJson.data.LU != null) { retVal.CountryModel.Add(new Country { CountryCode = "LU", CountryName = countryJson.data.LU.country, Region = countryJson.data.LU.region }); }
                        if (countryJson.data.LV != null) { retVal.CountryModel.Add(new Country { CountryCode = "LV", CountryName = countryJson.data.LV.country, Region = countryJson.data.LV.region }); }
                        if (countryJson.data.LY != null) { retVal.CountryModel.Add(new Country { CountryCode = "LY", CountryName = countryJson.data.LY.country, Region = countryJson.data.LY.region }); }
                        if (countryJson.data.MA != null) { retVal.CountryModel.Add(new Country { CountryCode = "MA", CountryName = countryJson.data.MA.country, Region = countryJson.data.MA.region }); }
                        if (countryJson.data.MC != null) { retVal.CountryModel.Add(new Country { CountryCode = "MC", CountryName = countryJson.data.MC.country, Region = countryJson.data.MC.region }); }
                        if (countryJson.data.MD != null) { retVal.CountryModel.Add(new Country { CountryCode = "MD", CountryName = countryJson.data.MD.country, Region = countryJson.data.MD.region }); }
                        if (countryJson.data.ME != null) { retVal.CountryModel.Add(new Country { CountryCode = "ME", CountryName = countryJson.data.ME.country, Region = countryJson.data.ME.region }); }
                        if (countryJson.data.MF != null) { retVal.CountryModel.Add(new Country { CountryCode = "MF", CountryName = countryJson.data.MF.country, Region = countryJson.data.MF.region }); }
                        if (countryJson.data.MG != null) { retVal.CountryModel.Add(new Country { CountryCode = "MG", CountryName = countryJson.data.MG.country, Region = countryJson.data.MG.region }); }
                        if (countryJson.data.MH != null) { retVal.CountryModel.Add(new Country { CountryCode = "MH", CountryName = countryJson.data.MH.country, Region = countryJson.data.MH.region }); }
                        if (countryJson.data.MK != null) { retVal.CountryModel.Add(new Country { CountryCode = "MK", CountryName = countryJson.data.MK.country, Region = countryJson.data.MK.region }); }
                        if (countryJson.data.ML != null) { retVal.CountryModel.Add(new Country { CountryCode = "ML", CountryName = countryJson.data.ML.country, Region = countryJson.data.ML.region }); }
                        if (countryJson.data.MM != null) { retVal.CountryModel.Add(new Country { CountryCode = "MM", CountryName = countryJson.data.MM.country, Region = countryJson.data.MM.region }); }
                        if (countryJson.data.MN != null) { retVal.CountryModel.Add(new Country { CountryCode = "MN", CountryName = countryJson.data.MN.country, Region = countryJson.data.MN.region }); }
                        if (countryJson.data.MO != null) { retVal.CountryModel.Add(new Country { CountryCode = "MO", CountryName = countryJson.data.MO.country, Region = countryJson.data.MO.region }); }
                        if (countryJson.data.MP != null) { retVal.CountryModel.Add(new Country { CountryCode = "MP", CountryName = countryJson.data.MP.country, Region = countryJson.data.MP.region }); }
                        if (countryJson.data.MQ != null) { retVal.CountryModel.Add(new Country { CountryCode = "MQ", CountryName = countryJson.data.MQ.country, Region = countryJson.data.MQ.region }); }
                        if (countryJson.data.MR != null) { retVal.CountryModel.Add(new Country { CountryCode = "MR", CountryName = countryJson.data.MR.country, Region = countryJson.data.MR.region }); }
                        if (countryJson.data.MS != null) { retVal.CountryModel.Add(new Country { CountryCode = "MS", CountryName = countryJson.data.MS.country, Region = countryJson.data.MS.region }); }
                        if (countryJson.data.MT != null) { retVal.CountryModel.Add(new Country { CountryCode = "MT", CountryName = countryJson.data.MT.country, Region = countryJson.data.MT.region }); }
                        if (countryJson.data.MU != null) { retVal.CountryModel.Add(new Country { CountryCode = "MU", CountryName = countryJson.data.MU.country, Region = countryJson.data.MU.region }); }
                        if (countryJson.data.MV != null) { retVal.CountryModel.Add(new Country { CountryCode = "MV", CountryName = countryJson.data.MV.country, Region = countryJson.data.MV.region }); }
                        if (countryJson.data.MW != null) { retVal.CountryModel.Add(new Country { CountryCode = "MW", CountryName = countryJson.data.MW.country, Region = countryJson.data.MW.region }); }
                        if (countryJson.data.MX != null) { retVal.CountryModel.Add(new Country { CountryCode = "MX", CountryName = countryJson.data.MX.country, Region = countryJson.data.MX.region }); }
                        if (countryJson.data.MY != null) { retVal.CountryModel.Add(new Country { CountryCode = "MY", CountryName = countryJson.data.MY.country, Region = countryJson.data.MY.region }); }
                        if (countryJson.data.MZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "MZ", CountryName = countryJson.data.MZ.country, Region = countryJson.data.MZ.region }); }
                        if (countryJson.data.NA != null) { retVal.CountryModel.Add(new Country { CountryCode = "NA", CountryName = countryJson.data.NA.country, Region = countryJson.data.NA.region }); }
                        if (countryJson.data.NC != null) { retVal.CountryModel.Add(new Country { CountryCode = "NC", CountryName = countryJson.data.NC.country, Region = countryJson.data.NC.region }); }
                        if (countryJson.data.NE != null) { retVal.CountryModel.Add(new Country { CountryCode = "NE", CountryName = countryJson.data.NE.country, Region = countryJson.data.NE.region }); }
                        if (countryJson.data.NF != null) { retVal.CountryModel.Add(new Country { CountryCode = "NF", CountryName = countryJson.data.NF.country, Region = countryJson.data.NF.region }); }
                        if (countryJson.data.NG != null) { retVal.CountryModel.Add(new Country { CountryCode = "NG", CountryName = countryJson.data.NG.country, Region = countryJson.data.NG.region }); }
                        if (countryJson.data.NI != null) { retVal.CountryModel.Add(new Country { CountryCode = "NI", CountryName = countryJson.data.NI.country, Region = countryJson.data.NI.region }); }
                        if (countryJson.data.NL != null) { retVal.CountryModel.Add(new Country { CountryCode = "NL", CountryName = countryJson.data.NL.country, Region = countryJson.data.NL.region }); }
                        if (countryJson.data.NO != null) { retVal.CountryModel.Add(new Country { CountryCode = "NO", CountryName = countryJson.data.NO.country, Region = countryJson.data.NO.region }); }
                        if (countryJson.data.NP != null) { retVal.CountryModel.Add(new Country { CountryCode = "NP", CountryName = countryJson.data.NP.country, Region = countryJson.data.NP.region }); }
                        if (countryJson.data.NR != null) { retVal.CountryModel.Add(new Country { CountryCode = "NR", CountryName = countryJson.data.NR.country, Region = countryJson.data.NR.region }); }
                        if (countryJson.data.NU != null) { retVal.CountryModel.Add(new Country { CountryCode = "NU", CountryName = countryJson.data.NU.country, Region = countryJson.data.NU.region }); }
                        if (countryJson.data.NZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "NZ", CountryName = countryJson.data.NZ.country, Region = countryJson.data.NZ.region }); }
                        if (countryJson.data.OM != null) { retVal.CountryModel.Add(new Country { CountryCode = "OM", CountryName = countryJson.data.OM.country, Region = countryJson.data.OM.region }); }
                        if (countryJson.data.PA != null) { retVal.CountryModel.Add(new Country { CountryCode = "PA", CountryName = countryJson.data.PA.country, Region = countryJson.data.PA.region }); }
                        if (countryJson.data.PE != null) { retVal.CountryModel.Add(new Country { CountryCode = "PE", CountryName = countryJson.data.PE.country, Region = countryJson.data.PE.region }); }
                        if (countryJson.data.PF != null) { retVal.CountryModel.Add(new Country { CountryCode = "PF", CountryName = countryJson.data.PF.country, Region = countryJson.data.PF.region }); }
                        if (countryJson.data.PG != null) { retVal.CountryModel.Add(new Country { CountryCode = "PG", CountryName = countryJson.data.PG.country, Region = countryJson.data.PG.region }); }
                        if (countryJson.data.PH != null) { retVal.CountryModel.Add(new Country { CountryCode = "PH", CountryName = countryJson.data.PH.country, Region = countryJson.data.PH.region }); }
                        if (countryJson.data.PK != null) { retVal.CountryModel.Add(new Country { CountryCode = "PK", CountryName = countryJson.data.PK.country, Region = countryJson.data.PK.region }); }
                        if (countryJson.data.PL != null) { retVal.CountryModel.Add(new Country { CountryCode = "PL", CountryName = countryJson.data.PL.country, Region = countryJson.data.PL.region }); }
                        if (countryJson.data.PM != null) { retVal.CountryModel.Add(new Country { CountryCode = "PM", CountryName = countryJson.data.PM.country, Region = countryJson.data.PM.region }); }
                        if (countryJson.data.PN != null) { retVal.CountryModel.Add(new Country { CountryCode = "PN", CountryName = countryJson.data.PN.country, Region = countryJson.data.PN.region }); }
                        if (countryJson.data.PR != null) { retVal.CountryModel.Add(new Country { CountryCode = "PR", CountryName = countryJson.data.PR.country, Region = countryJson.data.PR.region }); }
                        if (countryJson.data.PS != null) { retVal.CountryModel.Add(new Country { CountryCode = "PS", CountryName = countryJson.data.PS.country, Region = countryJson.data.PS.region }); }
                        if (countryJson.data.PT != null) { retVal.CountryModel.Add(new Country { CountryCode = "PT", CountryName = countryJson.data.PT.country, Region = countryJson.data.PT.region }); }
                        if (countryJson.data.PW != null) { retVal.CountryModel.Add(new Country { CountryCode = "PW", CountryName = countryJson.data.PW.country, Region = countryJson.data.PW.region }); }
                        if (countryJson.data.PY != null) { retVal.CountryModel.Add(new Country { CountryCode = "PY", CountryName = countryJson.data.PY.country, Region = countryJson.data.PY.region }); }
                        if (countryJson.data.QA != null) { retVal.CountryModel.Add(new Country { CountryCode = "QA", CountryName = countryJson.data.QA.country, Region = countryJson.data.QA.region }); }
                        if (countryJson.data.RE != null) { retVal.CountryModel.Add(new Country { CountryCode = "RE", CountryName = countryJson.data.RE.country, Region = countryJson.data.RE.region }); }
                        if (countryJson.data.RO != null) { retVal.CountryModel.Add(new Country { CountryCode = "RO", CountryName = countryJson.data.RO.country, Region = countryJson.data.RO.region }); }
                        if (countryJson.data.RS != null) { retVal.CountryModel.Add(new Country { CountryCode = "RS", CountryName = countryJson.data.RS.country, Region = countryJson.data.RS.region }); }
                        if (countryJson.data.RU != null) { retVal.CountryModel.Add(new Country { CountryCode = "RU", CountryName = countryJson.data.RU.country, Region = countryJson.data.RU.region }); }
                        if (countryJson.data.RW != null) { retVal.CountryModel.Add(new Country { CountryCode = "RW", CountryName = countryJson.data.RW.country, Region = countryJson.data.RW.region }); }
                        if (countryJson.data.SA != null) { retVal.CountryModel.Add(new Country { CountryCode = "SA", CountryName = countryJson.data.SA.country, Region = countryJson.data.SA.region }); }
                        if (countryJson.data.SB != null) { retVal.CountryModel.Add(new Country { CountryCode = "SB", CountryName = countryJson.data.SB.country, Region = countryJson.data.SB.region }); }
                        if (countryJson.data.SC != null) { retVal.CountryModel.Add(new Country { CountryCode = "SC", CountryName = countryJson.data.SC.country, Region = countryJson.data.SC.region }); }
                        if (countryJson.data.SD != null) { retVal.CountryModel.Add(new Country { CountryCode = "SD", CountryName = countryJson.data.SD.country, Region = countryJson.data.SD.region }); }
                        if (countryJson.data.SE != null) { retVal.CountryModel.Add(new Country { CountryCode = "SE", CountryName = countryJson.data.SE.country, Region = countryJson.data.SE.region }); }
                        if (countryJson.data.SG != null) { retVal.CountryModel.Add(new Country { CountryCode = "SG", CountryName = countryJson.data.SG.country, Region = countryJson.data.SG.region }); }
                        if (countryJson.data.SH != null) { retVal.CountryModel.Add(new Country { CountryCode = "SH", CountryName = countryJson.data.SH.country, Region = countryJson.data.SH.region }); }
                        if (countryJson.data.SI != null) { retVal.CountryModel.Add(new Country { CountryCode = "SI", CountryName = countryJson.data.SI.country, Region = countryJson.data.SI.region }); }
                        if (countryJson.data.SJ != null) { retVal.CountryModel.Add(new Country { CountryCode = "SJ", CountryName = countryJson.data.SJ.country, Region = countryJson.data.SJ.region }); }
                        if (countryJson.data.SK != null) { retVal.CountryModel.Add(new Country { CountryCode = "SK", CountryName = countryJson.data.SK.country, Region = countryJson.data.SK.region }); }
                        if (countryJson.data.SL != null) { retVal.CountryModel.Add(new Country { CountryCode = "SL", CountryName = countryJson.data.SL.country, Region = countryJson.data.SL.region }); }
                        if (countryJson.data.SM != null) { retVal.CountryModel.Add(new Country { CountryCode = "SM", CountryName = countryJson.data.SM.country, Region = countryJson.data.SM.region }); }
                        if (countryJson.data.SN != null) { retVal.CountryModel.Add(new Country { CountryCode = "SN", CountryName = countryJson.data.SN.country, Region = countryJson.data.SN.region }); }
                        if (countryJson.data.SO != null) { retVal.CountryModel.Add(new Country { CountryCode = "SO", CountryName = countryJson.data.SO.country, Region = countryJson.data.SO.region }); }
                        if (countryJson.data.SR != null) { retVal.CountryModel.Add(new Country { CountryCode = "SR", CountryName = countryJson.data.SR.country, Region = countryJson.data.SR.region }); }
                        if (countryJson.data.SS != null) { retVal.CountryModel.Add(new Country { CountryCode = "SS", CountryName = countryJson.data.SS.country, Region = countryJson.data.SS.region }); }
                        if (countryJson.data.ST != null) { retVal.CountryModel.Add(new Country { CountryCode = "ST", CountryName = countryJson.data.ST.country, Region = countryJson.data.ST.region }); }
                        if (countryJson.data.SV != null) { retVal.CountryModel.Add(new Country { CountryCode = "SV", CountryName = countryJson.data.SV.country, Region = countryJson.data.SV.region }); }
                        if (countryJson.data.SX != null) { retVal.CountryModel.Add(new Country { CountryCode = "SX", CountryName = countryJson.data.SX.country, Region = countryJson.data.SX.region }); }
                        if (countryJson.data.SY != null) { retVal.CountryModel.Add(new Country { CountryCode = "SY", CountryName = countryJson.data.SY.country, Region = countryJson.data.SY.region }); }
                        if (countryJson.data.SZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "SZ", CountryName = countryJson.data.SZ.country, Region = countryJson.data.SZ.region }); }
                        if (countryJson.data.TC != null) { retVal.CountryModel.Add(new Country { CountryCode = "TC", CountryName = countryJson.data.TC.country, Region = countryJson.data.TC.region }); }
                        if (countryJson.data.TD != null) { retVal.CountryModel.Add(new Country { CountryCode = "TD", CountryName = countryJson.data.TD.country, Region = countryJson.data.TD.region }); }
                        if (countryJson.data.TF != null) { retVal.CountryModel.Add(new Country { CountryCode = "TF", CountryName = countryJson.data.TF.country, Region = countryJson.data.TF.region }); }
                        if (countryJson.data.TG != null) { retVal.CountryModel.Add(new Country { CountryCode = "TG", CountryName = countryJson.data.TG.country, Region = countryJson.data.TG.region }); }
                        if (countryJson.data.TH != null) { retVal.CountryModel.Add(new Country { CountryCode = "TH", CountryName = countryJson.data.TH.country, Region = countryJson.data.TH.region }); }
                        if (countryJson.data.TJ != null) { retVal.CountryModel.Add(new Country { CountryCode = "TJ", CountryName = countryJson.data.TJ.country, Region = countryJson.data.TJ.region }); }
                        if (countryJson.data.TK != null) { retVal.CountryModel.Add(new Country { CountryCode = "TK", CountryName = countryJson.data.TK.country, Region = countryJson.data.TK.region }); }
                        if (countryJson.data.TL != null) { retVal.CountryModel.Add(new Country { CountryCode = "TL", CountryName = countryJson.data.TL.country, Region = countryJson.data.TL.region }); }
                        if (countryJson.data.TM != null) { retVal.CountryModel.Add(new Country { CountryCode = "TM", CountryName = countryJson.data.TM.country, Region = countryJson.data.TM.region }); }
                        if (countryJson.data.TN != null) { retVal.CountryModel.Add(new Country { CountryCode = "TN", CountryName = countryJson.data.TN.country, Region = countryJson.data.TN.region }); }
                        if (countryJson.data.TO != null) { retVal.CountryModel.Add(new Country { CountryCode = "TO", CountryName = countryJson.data.TO.country, Region = countryJson.data.TO.region }); }
                        if (countryJson.data.TR != null) { retVal.CountryModel.Add(new Country { CountryCode = "TR", CountryName = countryJson.data.TR.country, Region = countryJson.data.TR.region }); }
                        if (countryJson.data.TT != null) { retVal.CountryModel.Add(new Country { CountryCode = "TT", CountryName = countryJson.data.TT.country, Region = countryJson.data.TT.region }); }
                        if (countryJson.data.TV != null) { retVal.CountryModel.Add(new Country { CountryCode = "TV", CountryName = countryJson.data.TV.country, Region = countryJson.data.TV.region }); }
                        if (countryJson.data.TW != null) { retVal.CountryModel.Add(new Country { CountryCode = "TW", CountryName = countryJson.data.TW.country, Region = countryJson.data.TW.region }); }
                        if (countryJson.data.TZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "TZ", CountryName = countryJson.data.TZ.country, Region = countryJson.data.TZ.region }); }
                        if (countryJson.data.UA != null) { retVal.CountryModel.Add(new Country { CountryCode = "UA", CountryName = countryJson.data.UA.country, Region = countryJson.data.UA.region }); }
                        if (countryJson.data.UG != null) { retVal.CountryModel.Add(new Country { CountryCode = "UG", CountryName = countryJson.data.UG.country, Region = countryJson.data.UG.region }); }
                        if (countryJson.data.UM != null) { retVal.CountryModel.Add(new Country { CountryCode = "UM", CountryName = countryJson.data.UM.country, Region = countryJson.data.UM.region }); }
                        if (countryJson.data.US != null) { retVal.CountryModel.Add(new Country { CountryCode = "US", CountryName = countryJson.data.US.country, Region = countryJson.data.US.region }); }
                        if (countryJson.data.UY != null) { retVal.CountryModel.Add(new Country { CountryCode = "UY", CountryName = countryJson.data.UY.country, Region = countryJson.data.UY.region }); }
                        if (countryJson.data.UZ != null) { retVal.CountryModel.Add(new Country { CountryCode = "UZ", CountryName = countryJson.data.UZ.country, Region = countryJson.data.UZ.region }); }
                        if (countryJson.data.VA != null) { retVal.CountryModel.Add(new Country { CountryCode = "VA", CountryName = countryJson.data.VA.country, Region = countryJson.data.VA.region }); }
                        if (countryJson.data.VC != null) { retVal.CountryModel.Add(new Country { CountryCode = "VC", CountryName = countryJson.data.VC.country, Region = countryJson.data.VC.region }); }
                        if (countryJson.data.VE != null) { retVal.CountryModel.Add(new Country { CountryCode = "VE", CountryName = countryJson.data.VE.country, Region = countryJson.data.VE.region }); }
                        if (countryJson.data.VG != null) { retVal.CountryModel.Add(new Country { CountryCode = "VG", CountryName = countryJson.data.VG.country, Region = countryJson.data.VG.region }); }
                        if (countryJson.data.VI != null) { retVal.CountryModel.Add(new Country { CountryCode = "VI", CountryName = countryJson.data.VI.country, Region = countryJson.data.VI.region }); }
                        if (countryJson.data.VN != null) { retVal.CountryModel.Add(new Country { CountryCode = "VN", CountryName = countryJson.data.VN.country, Region = countryJson.data.VN.region }); }
                        if (countryJson.data.VU != null) { retVal.CountryModel.Add(new Country { CountryCode = "VU", CountryName = countryJson.data.VU.country, Region = countryJson.data.VU.region }); }
                        if (countryJson.data.WF != null) { retVal.CountryModel.Add(new Country { CountryCode = "WF", CountryName = countryJson.data.WF.country, Region = countryJson.data.WF.region }); }
                        if (countryJson.data.WS != null) { retVal.CountryModel.Add(new Country { CountryCode = "WS", CountryName = countryJson.data.WS.country, Region = countryJson.data.WS.region }); }
                        if (countryJson.data.YE != null) { retVal.CountryModel.Add(new Country { CountryCode = "YE", CountryName = countryJson.data.YE.country, Region = countryJson.data.YE.region }); }
                        if (countryJson.data.YT != null) { retVal.CountryModel.Add(new Country { CountryCode = "YT", CountryName = countryJson.data.YT.country, Region = countryJson.data.YT.region }); }
                        if (countryJson.data.ZA != null) { retVal.CountryModel.Add(new Country { CountryCode = "ZA", CountryName = countryJson.data.ZA.country, Region = countryJson.data.ZA.region }); }
                        if (countryJson.data.ZM != null) { retVal.CountryModel.Add(new Country { CountryCode = "ZM", CountryName = countryJson.data.ZM.country, Region = countryJson.data.ZM.region }); }
                        if (countryJson.data.ZW != null) { retVal.CountryModel.Add(new Country { CountryCode = "ZW", CountryName = countryJson.data.ZW.country, Region = countryJson.data.ZW.region }); }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("CountryService-GetCountries: Exception occured....");
                _logger.LogError(ex);
            }
            return retVal;
        }
    }
}
