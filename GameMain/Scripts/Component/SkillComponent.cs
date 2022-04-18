using GameFramework.Resource;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    /// <summary>
    /// 技能组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Skill")]
    public sealed partial class SkillComponent : GameFrameworkComponent
    {
        public ActorSkillData ActorSkillData { get; private set; }
        public AssetCurve Curves { get; private set; }
        //private string path = "Assets/GameMain/SkillData/SkillData.bytes";
        private string path = "Assets/GameMain/SkillData/SkillDataNew.json";
        private string curvesPath = "Assets/GameMain/CustomAssets/SkillAssetCurve.asset";
        public void Initialize()
        {
            LoadAssetCallbacks loadAssetCallbacks = new LoadAssetCallbacks(LoadAssetSuccessCallback, LoadAssetFailureCallback);
            GameEntry.Resource.LoadAsset(path, loadAssetCallbacks);
            SkillExecutor.Init();

            DatasetI1Array ddd=new DatasetI1Array();
            ddd.Data.Add(2);
            ddd.Data.Add(31);
            UnityEngine.Debug.Log(ddd.ToString());

            DatasetI2Array dddd3333=new DatasetI2Array();
            DatasetI2 datasetI2=new DatasetI2()
            {
                Data0=1,
                Data1=2
            };
            dddd3333.Data.Add(datasetI2);

            datasetI2=new DatasetI2()
            {
                Data0=3,
                Data1=4
            };
            dddd3333.Data.Add(datasetI2);
            UnityEngine.Debug.Log(dddd3333.ToString());

            loadAssetCallbacks = new LoadAssetCallbacks(LoadCurvesSuccessCallback, LoadCurvesFailureCallback);
            GameEntry.Resource.LoadAsset(curvesPath, loadAssetCallbacks);
        }

        public SkillData GetSkillData(int skillID)
        {
            ActorSkillData.SkillDatas.TryGetValue(out SkillData skillData, p => p.SkillID == skillID);
            return skillData;
        }

        private void LoadAssetSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            TextAsset textAsset = asset as TextAsset;

            string json=textAsset.text;
            Log.Info($"技能数据加载成功 {json.Length}");
            //ActorSkillData = ActorSkillData.Parser.ParseJson(json);
            JsonDeserializeObject(json);
            //byte[] byteArray = textAsset.bytes;
            //Log.Info($"技能数据加载成功 {byteArray.Length}");
            //ActorSkillData = ActorSkillData.Parser.ParseFrom(byteArray);

            foreach (SkillData item in ActorSkillData.SkillDatas)
            {
                Log.Info($"技能 {item.SkillID}  {item.SkillFrames.Count}");
            }
        }

        private void LoadAssetFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("加载失败", path, assetName, errorMessage);
        }

        private void LoadCurvesSuccessCallback(string assetName, object asset, float duration, object userData)
        {
            Curves = asset as AssetCurve;
            Log.Info($"曲线加载成功 {Curves.curveInfos.Length}");
        }

        private void LoadCurvesFailureCallback(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("曲线加载成功", curvesPath, assetName, errorMessage);
        }



        private void JsonDeserializeObject(string json)
        {
            ActorSkillData = new ActorSkillData();
            JObject jSkillDataset = JObject.Parse(json);
            JProperty jSkillMap = jSkillDataset.Property("SkillMaps");
            UnityEngine.Debug.Log(jSkillMap.Value.ToString());
            List<SkillMap> skillMap=JsonConvert.DeserializeObject<List<SkillMap>>(jSkillMap.Value.ToString());
            ActorSkillData.SkillMaps.AddRange(skillMap);
            JProperty jSkillData = jSkillDataset.Property("SkillDatas");
            //ActorSkillData.AllSkillData = JsonConvert.DeserializeObject<AllSkillData>(jSkillData.Value.ToString());
            JArray jDataArray = jSkillData.Value as JArray;

            //jProperty = new JProperty("allSkillData");
            //JObject jObjData = new JObject();
            //JProperty jPropertyData = new JProperty("data");
            //JArray jArrayData = new JArray();
            //jPropertyData.Value = jArrayData;
            //jObjData.Add(jPropertyData);
            //jProperty.Value = jObjData;
            //jObj.Add(jProperty);

            //AllSkillData allSkillData = new AllSkillData();
            //allSkillData.Data.AddRange(); = new Google.Protobuf.Collections.RepeatedField<SkillData>();
            List<SkillData> skillDatas = new List<SkillData>(jDataArray.Count);
            SkillData skillData;
            foreach (JToken item in jDataArray)
            {
                skillData = new SkillData();
                skillDatas.Add(skillData);

                skillData.SkillID = (int)item.SelectToken("SkillID");
                //JToken jConfig= item.SelectToken("SkillConfigs");
                //skillData1.Config =item.SelectToken("SkillConfigs");
                JArray jSkillFrames = item.SelectToken("SkillFrames") as JArray;

                List<SkillFrame> skillFrames = new List<SkillFrame>(skillData.SkillFrames.Count);

                foreach (JToken item1 in jSkillFrames)
                {
                    SkillFrame skillFrame = new SkillFrame();
                    skillFrames.Add(skillFrame);
                    skillFrame.StartNum = (int)item1.SelectToken("StartNum");

                    List<SkillFrameItem> skillFrameItems = new List<SkillFrameItem>();

                    JArray jSkillFrameItems = item1.SelectToken("SkillFrameItems") as JArray;

                    foreach (JToken item2 in jSkillFrameItems)
                    {
                        SkillFrameItem skillFrameUnit = new SkillFrameItem();
                        skillFrameItems.Add(skillFrameUnit);
                        skillFrameUnit.Duration= (int)item2.SelectToken("Duration");
                        skillFrameUnit.Number = (int)item2.SelectToken("Number");
                        skillFrameUnit.IsReverse = (bool)item2.SelectToken("IsReverse");

                        JArray jSwitch = item2.SelectToken("Conditions") as JArray;
                        List<DataUnit> skillUnits=JTokenToSkillUnit(jSwitch, true);
                        if(skillUnits!=null&& skillUnits.Count>0)
                        {
                            skillFrameUnit.Conditions.Clear();
                            skillFrameUnit.Conditions.AddRange(skillUnits);
                        }
                       

                        jSwitch = item2.SelectToken("TrueBehaviours") as JArray;
                        skillUnits=JTokenToSkillUnit(jSwitch, false);
                        if (skillUnits != null && skillUnits.Count > 0)
                        {
                            skillFrameUnit.TrueBehaviours.Clear();
                            skillFrameUnit.TrueBehaviours.AddRange(skillUnits);
                        }

                        jSwitch = item2.SelectToken("FalseBehaviours") as JArray;
                        skillUnits=JTokenToSkillUnit(jSwitch, false);
                        if (skillUnits != null && skillUnits.Count > 0)
                        {
                            skillFrameUnit.FalseBehaviours.Clear();
                            skillFrameUnit.FalseBehaviours.AddRange(skillUnits);
                        }
                    }
                    skillFrame.SkillFrameItems.Clear();
                    skillFrame.SkillFrameItems.Add(skillFrameItems);
                }
                skillData.SkillFrames.Clear();
                skillData.SkillFrames.AddRange(skillFrames);
            }
            ActorSkillData.SkillDatas.Clear();
            ActorSkillData.SkillDatas.AddRange(skillDatas);

            List<DataUnit> JTokenToSkillUnit(JArray jSwitch,bool isSwitch)
            {
                if (jSwitch == null || jSwitch.Count <= 0) return null;
                List<DataUnit> jDataUnits = new List<DataUnit>(jSwitch.Count);
                foreach (JToken item in jSwitch)
                {
                    DataUnit dataUnit = new DataUnit();
                    jDataUnits.Add(dataUnit);
                    dataUnit.Type = (int)item.SelectToken("Type");
                    dataUnit.IsReverse= (bool)item.SelectToken("IsReverse");
                    JToken jtByteStr = item.SelectToken("Message");
                    //JObject byteStr = jtByteStr as JObject;
                    string jsonStr = jtByteStr.ToString();
                    UnityEngine.Debug.Log(jsonStr);
                    IMessage message = isSwitch?SkillUnit1(dataUnit.Type,jsonStr) : SkillUnit2(dataUnit.Type,jsonStr);
                    if(message!=null)
                    {
                        dataUnit.SaveDataset(message);
                        UnityEngine.Debug.LogError($"Type :  {message.GetType()} ");
                    }
                    UnityEngine.Debug.LogError($"Type :  {jsonStr} ");
                }
                return jDataUnits;
            }

            IMessage SkillUnit2(int type,string json)
            {
                if (string.IsNullOrEmpty(json)) return null;
                return type switch
                {
                    1 => JsonConvert.DeserializeObject<DatasetB2S1>(json),
                    2 => JsonConvert.DeserializeObject<DatasetI4>(json),
                    3 => JsonConvert.DeserializeObject<DatasetI1Array>(json),
                    4 => JsonConvert.DeserializeObject<DatasetF3>(json),
                    5 => JsonConvert.DeserializeObject<DatasetB1>(json),
                    6 => JsonConvert.DeserializeObject<DatasetI3B1>(json),
                    7 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    8 => JsonConvert.DeserializeObject<DatasetF2>(json),
                    9 => JsonConvert.DeserializeObject<DatasetF1>(json),
                    10 => JsonConvert.DeserializeObject<DatasetI8>(json),
                    11 => JsonConvert.DeserializeObject<DatasetI2Array>(json),
                    12 => JsonConvert.DeserializeObject<DatasetI1Array>(json),
                    13 => JsonConvert.DeserializeObject<DatasetI5B1>(json),
                    14 => JsonConvert.DeserializeObject<DatasetI2F2>(json),
                    15 => JsonConvert.DeserializeObject<DatasetF2>(json),
                    16 => JsonConvert.DeserializeObject<DatasetF2B2>(json),
                    17 => JsonConvert.DeserializeObject<DatasetF5I1>(json),
                    18 => JsonConvert.DeserializeObject<DatasetI1F1S1>(json),
                    20 => JsonConvert.DeserializeObject<DatasetF4I1>(json),
                    21 => JsonConvert.DeserializeObject<DatasetI3F1>(json),
                    22 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    23 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    25 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    26 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    27 => JsonConvert.DeserializeObject<DatasetI1S1>(json),
                    28 => JsonConvert.DeserializeObject<DatasetI1B1>(json),
                    29 => JsonConvert.DeserializeObject<DatasetF4I1B1>(json),
                    30 => JsonConvert.DeserializeObject<DatasetF4>(json),
                    31 => JsonConvert.DeserializeObject<DatasetS8>(json),
                    32 => JsonConvert.DeserializeObject<DatasetI8>(json),
                    _ => null
                };
            }

            IMessage SkillUnit1(int type, string json)
            {
                UnityEngine.Debug.Log(type);
                return type switch
                {
                    2 => JsonConvert.DeserializeObject<DatasetI2F2>(json),
                    5 => JsonConvert.DeserializeObject<DatasetF2I1>(json),
                    8 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    9 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    11 => JsonConvert.DeserializeObject<DatasetI1>(json),
                    _ => null
                };
            }
        }
    }
}
