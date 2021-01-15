using System;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace GraphicApparelDetour
{
	// Token: 0x02000002 RID: 2
	public static class Detours
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public unsafe static bool TryDetourFromTo(MethodInfo source, MethodInfo destination)
		{
			var flag = source == null;
			bool result;
			if (flag)
			{
				Log.Message("Apparel Stuff Color source is null", false);
				result = false;
			}
			else
			{
				var flag2 = destination == null;
				if (flag2)
				{
					Log.Message("Apparel Stuff Color target is null", false);
					result = false;
				}
				else
				{
					Log.Message("Apparel Stuff Color detours loaded, hopefully correct", false);
					var item = string.Concat(new string[]
					{
						source.DeclaringType.FullName,
						".",
						source.Name,
						" @ 0x",
						source.MethodHandle.GetFunctionPointer().ToString("X" + (IntPtr.Size * 2).ToString())
					});
					var item2 = string.Concat(new string[]
					{
						destination.DeclaringType.FullName,
						".",
						destination.Name,
						" @ 0x",
						destination.MethodHandle.GetFunctionPointer().ToString("X" + (IntPtr.Size * 2).ToString())
					});
					var flag3 = Detours.detoured.Contains(item);
					if (flag3)
					{
					}
					Detours.detoured.Add(item);
					Detours.destinations.Add(item2);
					var flag4 = IntPtr.Size == 8;
					if (flag4)
					{
						var num = source.MethodHandle.GetFunctionPointer().ToInt64();
						var num2 = destination.MethodHandle.GetFunctionPointer().ToInt64();
						var ptr = (byte*)num;
						var ptr2 = (long*)(ptr + 2);
						*ptr = 72;
						ptr[1] = 184;
						*ptr2 = num2;
						ptr[10] = byte.MaxValue;
						ptr[11] = 224;
					}
					else
					{
						var num3 = source.MethodHandle.GetFunctionPointer().ToInt32();
						var num4 = destination.MethodHandle.GetFunctionPointer().ToInt32();
						var ptr3 = (byte*)num3;
						var ptr4 = (int*)(ptr3 + 1);
						var num5 = num4 - num3 - 5;
						*ptr3 = 233;
						*ptr4 = num5;
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		private static readonly List<string> detoured = new List<string>();

		// Token: 0x04000002 RID: 2
		private static readonly List<string> destinations = new List<string>();
	}
}
