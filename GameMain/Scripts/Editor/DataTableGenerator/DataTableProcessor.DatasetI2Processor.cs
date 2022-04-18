using Google.Protobuf.Collections;
using System;
using System.Data;
using System.IO;
using UnityEngine;

namespace AltarOfSword.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class DatasetI2Processor : GenericDataProcessor<RepeatedField<DatasetI2>>
        {
            public override Type Type
            {
                get
                {
                    return typeof(DatasetI2);
                }
            }
            public override bool IsSystem
            {
                get
                {
                    return false;
                }
            }

            public override string LanguageKeyword
            {
                get
                {
                    return "Google.Protobuf.Collections.RepeatedField<DatasetI2>";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "DatasetI2",
                };
            }

            public override RepeatedField<DatasetI2> Parse(string value)
            {
                string[] splitedValue = value.Split(';');
                RepeatedField<DatasetI2> datasets = new RepeatedField<DatasetI2>();
                DatasetI2 dataset = null;
                string[] splitedStr = null;
                for (int i = 0; i < splitedValue.Length; i++)
                {
                    dataset = new DatasetI2();
                    splitedStr = splitedValue[i].Split('_');
                    if (splitedStr.Length < 2) continue;
                    dataset.Data0 = int.Parse(splitedStr[0]);
                    dataset.Data1 = int.Parse(splitedStr[1]);
                    datasets.Add(dataset);
                }
                return datasets;
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                RepeatedField<DatasetI2> datasets = Parse(value);
                int length = datasets.Count;
                if (length <= 0) return;
                binaryWriter.Write(length);
                foreach (DatasetI2 item in datasets)
                {
                    binaryWriter.Write(item.Data0);
                    binaryWriter.Write(item.Data1);
                }
            }
        }
    }
}
