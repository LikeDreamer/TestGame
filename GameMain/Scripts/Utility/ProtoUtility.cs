using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;

namespace AltarOfSword
{
    public static class ProtoUtility
    {
		public static bool ContainsElement<T>(this RepeatedField<T> source)
		{
			return source != null && source.Count > 0;
		}
		public static bool TryGetValue<T>(this RepeatedField<T> source, out T result, Func<T, bool> selector)
		{
			foreach (T item in source)
			{
				if (selector(item))
				{
					result = item;
					return true;
				}
			}
			result = default(T);
			return false;
		}

		public static bool IsNone<T>(this RepeatedField<T> source)
		{
			if (source == null) return true;
			return source.Count <= 0;
		}
	}
}
