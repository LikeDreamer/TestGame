using Google.Protobuf;
using System;
using System.Reflection;

namespace AltarOfSword
{
    public sealed partial class DataUnit
    {
        private IMessage message;
        public IMessage Message => message;
        public T GetDataset<T>() where T : class, IMessage<T>, new()
        {
            if (message == null) InitDataset<T>();
            return Message as T;
        }

        public bool GetDataset<T>(out T dataset) where T : class, IMessage<T>, new()
        {
            if (message == null) InitDataset<T>();
            dataset = Message as T;
            return dataset != null;
        }
        public void InitDataset<T>() where T : class, IMessage<T>, new()
        {
            if (Dataset == null) return;
            Type type = typeof(T);
            Type parserType = typeof(MessageParser<>).MakeGenericType(type);
            object parser = type.GetProperty("Parser", parserType).GetValue(null);
            MethodInfo parseFrom = parserType.GetMethod("ParseFrom", new Type[] { typeof(ByteString) });
            object returnObj = parseFrom.Invoke(parser, new object[] { Dataset });
            message = returnObj as T;
        }

        public void SaveDataset<T>(T dataset) where T : class, IMessage
        {
            message = dataset;
            Dataset = ByteString.CopyFrom(dataset.ToByteArray());
        }

        public void SaveDataset()
        {
            if (message == null) return;
            Dataset = ByteString.CopyFrom(Message.ToByteArray());
        }
    }
}

