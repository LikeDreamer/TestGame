//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.DataTable;
using Google.Protobuf.Collections;
using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace AltarOfSword
{
    public static class DataTableExtension
    {
        private const string DataRowClassPrefixName = "AltarOfSword.DR";
        internal static readonly char[] DataSplitSeparators = new char[] { '\t' };
        internal static readonly char[] DataTrimSeparators = new char[] { '\"' };

        public static void LoadDataTable(this DataTableComponent dataTableComponent, string dataTableName, string dataTableAssetName, object userData)
        {
            if (string.IsNullOrEmpty(dataTableName))
            {
                Log.Warning("表名不能为空！");
                return;
            }

            string[] splitedNames = dataTableName.Split('_');
            if (splitedNames.Length > 2)
            {
                Log.Warning("表名无效");
                return;
            }

            string dataRowClassName = DataRowClassPrefixName + splitedNames[0];
            Type dataRowType = Type.GetType(dataRowClassName);
            if (dataRowType == null)
            {
                Log.Warning("Can not get data row type with class name '{0}'.", dataRowClassName);
                return;
            }

            string name = splitedNames.Length > 1 ? splitedNames[1] : null;
            DataTableBase dataTable = dataTableComponent.CreateDataTable(dataRowType, name);
            dataTable.ReadData(dataTableAssetName, Constant.AssetPriority.DataTableAsset, userData);
        }

        public static Color32 ParseColor32(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Color32(byte.Parse(splitedValue[0]), byte.Parse(splitedValue[1]), byte.Parse(splitedValue[2]), byte.Parse(splitedValue[3]));
        }

        public static Color ParseColor(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Color(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Quaternion ParseQuaternion(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Quaternion(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Rect ParseRect(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Rect(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static Vector2 ParseVector2(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector2(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]));
        }

        public static Vector3 ParseVector3(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector3(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]));
        }

        public static Vector4 ParseVector4(string value)
        {
            string[] splitedValue = value.Split(',');
            return new Vector4(float.Parse(splitedValue[0]), float.Parse(splitedValue[1]), float.Parse(splitedValue[2]), float.Parse(splitedValue[3]));
        }

        public static RepeatedField<DatasetI1> ParseDatasetI1(string value)
        {
            return ParseRepeatedField(value, ParseDatase);
            DatasetI1 ParseDatase(string[] splitedStr)
            {
                if (splitedStr.Length < 1) return null;
                DatasetI1 dataset = new DatasetI1();
                dataset.Data0 = int.Parse(splitedStr[0]);
                return dataset;
            }
        }

        public static RepeatedField<DatasetI2> ParseDatasetI2(string value)
        {
            return ParseRepeatedField(value, ParseDatase);
            DatasetI2 ParseDatase(string[] splitedStr)
            {
                if (splitedStr.Length < 2) return null;
                DatasetI2 dataset = new DatasetI2();
                dataset.Data0 = int.Parse(splitedStr[0]);
                dataset.Data1 = int.Parse(splitedStr[1]);
                return dataset;
            }
        }

        private static RepeatedField<T> ParseRepeatedField<T>(string value,Func<string[],T> parseDatase) where T:class,Google.Protobuf.IMessage<T>,new()
        {
            string[] splitedValue = value.Split(';');
            RepeatedField<T> datasets = new RepeatedField<T>();
            for (int i = 0; i < splitedValue.Length; i++)
            {
                datasets.Add(parseDatase(splitedValue[i].Split('_')));
            }
            return datasets;
        }
    }
}
