using System.Linq;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SettlementLandData", order = 5)]
public class CityStructureData : SerializedScriptableObject
{
    public class SettlementLandData
    {
        public EnumCityClasses CityClass;
        public Dictionary<EnumCityStructure, int> CityParts = new Dictionary<EnumCityStructure, int>();
    }

    public List<SettlementLandData> settlementLandDatas;
    public Dictionary<EnumCityStructure, int> GetDict(EnumCityClasses type)
    {
        return settlementLandDatas.Where(x => x.CityClass == type & x.CityClass == type).Select(c => c.CityParts).FirstOrDefault();
    }
}
