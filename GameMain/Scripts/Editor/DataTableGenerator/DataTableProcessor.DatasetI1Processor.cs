using Google.Protobuf.Collections;
using System;
using System.Data;
using System.IO;
using UnityEngine;

namespace AltarOfSword.Editor.DataTableTools
{
    public sealed partial class DataTableProcessor
    {
        private sealed class DatasetI1Processor : GenericDataProcessor<RepeatedField<DatasetI1>>
        {
            public override Type Type
            {
                get
                {
                    return typeof(DatasetI1);
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
                    return "Google.Protobuf.Collections.RepeatedField<DatasetI1>";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "DatasetI1",
                };
            }

            public override RepeatedField<DatasetI1> Parse(string value)
            {
                return DataTableExtension.ParseDatasetI1(value);
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                RepeatedField<DatasetI1> datasets = Parse(value);
                int length = datasets.Count;
                if (length <= 0) return;
                binaryWriter.Write(length);
                foreach (DatasetI1 item in datasets)
                {
                    binaryWriter.Write(item.Data0);
                }
            }
        }
    }
}
