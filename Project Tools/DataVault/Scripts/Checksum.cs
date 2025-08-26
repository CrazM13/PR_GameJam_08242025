using Godot;
using Godot.Collections;
using System;

namespace DataVault {
	public static class Checksum {

		public static int GetChecksum(Dictionary<string, Variant> data) {
			int checksum = 0;

			foreach (Variant value in data.Values) {
				checksum += ConvertVariantToNumber(value);
			}

			return checksum;
		}

		private static int ConvertVariantToNumber(Variant variant) {
			return (variant.VariantType) switch {
				Variant.Type.Nil => 0,
				Variant.Type.Bool => variant.AsBool() ? 1 : 0,
				Variant.Type.String => ConvertStringToNumber(variant.AsString()),
				Variant.Type.Float => (int) variant.As<float>(),
				Variant.Type.Int => variant.As<int>(),
				Variant.Type.Vector2 => ConvertVectorToNumber(variant.AsVector2()),
				Variant.Type.Vector3 => ConvertVectorToNumber(variant.AsVector3()),
				Variant.Type.Vector2I => ConvertVectorToNumber(variant.AsVector2I()),
				Variant.Type.Vector3I => ConvertVectorToNumber(variant.AsVector3I()),
				_ => variant.GetHashCode()
			};
		}

		private static int ConvertStringToNumber(string str) {
			int result = 0;

			unchecked {
				for (int i = 0; i < str.Length; i++) {
					result += (i * 13) * str[i];
				}
			}

			return result;
		}

		private static int ConvertVectorToNumber(Vector2 vector) {
			int result = 0;

			unchecked {
				result += Mathf.FloorToInt(13 * vector.X);
				result += Mathf.FloorToInt(27 * vector.Y);
			}

			return result;
		}

		private static int ConvertVectorToNumber(Vector3 vector) {
			int result = 0;

			unchecked {
				result += Mathf.FloorToInt(13 * vector.X);
				result += Mathf.FloorToInt(27 * vector.Y);
				result += Mathf.FloorToInt(31 * vector.Z);
			}

			return result;
		}

	}
}