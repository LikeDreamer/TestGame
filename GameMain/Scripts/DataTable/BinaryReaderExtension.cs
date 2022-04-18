//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using Google.Protobuf.Collections;
using System;
using System.IO;
using UnityEngine;

namespace AltarOfSword
{
    public static class BinaryReaderExtension
    {
        public static Color32 ReadColor32(this BinaryReader binaryReader)
        {
            return new Color32(binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte(), binaryReader.ReadByte());
        }

        public static Color ReadColor(this BinaryReader binaryReader)
        {
            return new Color(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static DateTime ReadDateTime(this BinaryReader binaryReader)
        {
            return new DateTime(binaryReader.ReadInt64());
        }

        public static Quaternion ReadQuaternion(this BinaryReader binaryReader)
        {
            return new Quaternion(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static Rect ReadRect(this BinaryReader binaryReader)
        {
            return new Rect(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static Vector2 ReadVector2(this BinaryReader binaryReader)
        {
            return new Vector2(binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static Vector3 ReadVector3(this BinaryReader binaryReader)
        {
            return new Vector3(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static Vector4 ReadVector4(this BinaryReader binaryReader)
        {
            return new Vector4(binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle(), binaryReader.ReadSingle());
        }

        public static RepeatedField<DatasetI1> ReadDatasetI1(this BinaryReader binaryReader)
        {
            return RepeatedField(binaryReader, ReadDataset);
            DatasetI1 ReadDataset(BinaryReader binaryReader)
            {
                DatasetI1 dataset = new DatasetI1();
                dataset.Data0 = binaryReader.ReadInt32();
                return dataset;
            }
        }

        public static RepeatedField<DatasetI2> ReadDatasetI2(this BinaryReader binaryReader)
        {
            return RepeatedField(binaryReader, ReadDataset);
            DatasetI2 ReadDataset(BinaryReader binaryReader)
            {
                DatasetI2 dataset = new DatasetI2();
                dataset.Data0 = binaryReader.ReadInt32();
                dataset.Data1 = binaryReader.ReadInt32();
                return dataset;
            }
        }

        public static RepeatedField<T> RepeatedField<T>(BinaryReader binaryReader,Func<BinaryReader,T> readDataset) where T : class, Google.Protobuf.IMessage<T>, new()
        {
            RepeatedField<T> datasets = new RepeatedField<T>();
            int count = binaryReader.ReadInt32();
            if (count <= 0) return datasets;
            for (int i = 0; i < count; i++)
            {
                datasets.Add(readDataset(binaryReader));
            }
            return datasets;
        }
    }
}
