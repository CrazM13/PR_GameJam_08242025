using Godot;
using Godot.Collections;
using Godot.NativeInterop;
using System;

namespace DataVault {
	public class Vault {

		#region VaultCodes
		private const string LABEL_VAULT_NAME = "vault_name";
		private const string LABEL_DATA = "data";
		private const string LABEL_CHECK_SUM = "check_sum";
		private const string LABEL_IS_MODIFIED = "is_modified";
		#endregion

		public string VaultName { get; set; }

		private Dictionary<string, Variant> data;

		public Vault(string name) {
			this.VaultName = name;
			data = new Dictionary<string, Variant>();
		}

		public Vault(Json data) {
			this.VaultName = data.Data.AsGodotDictionary()[LABEL_VAULT_NAME].AsString();
			this.data = data.Data.AsGodotDictionary()[LABEL_DATA].AsGodotDictionary<string, Variant>();

			// CheckSum
			int checkSum = data.Data.AsGodotDictionary()[LABEL_CHECK_SUM].As<int>();
			if (checkSum != Checksum.GetChecksum(this.data)) {
				if (this.data.ContainsKey(LABEL_IS_MODIFIED)) {
					this.data[LABEL_IS_MODIFIED] = true;
				} else {
					this.data.Add(LABEL_IS_MODIFIED, true);
				}
			}
		}

		public Variant GetValue(string key) {
			return data[key];
		}

		public T GetValue<[MustBeVariant] T>(string key) {
			return data[key].As<T>();
		}

		public void SetValue(string key, Variant value) {
			if (data.ContainsKey(key)) {
				data[key] = value;
			} else {
				data.Add(key, value);
			}
		}

		public Json ToJSON() {

			Dictionary jsonData = new Dictionary {
				{ LABEL_VAULT_NAME, VaultName },
				{ LABEL_DATA, data },
				{ LABEL_CHECK_SUM, Checksum.GetChecksum(data) }
			};

			Json json = new() {
				Data = jsonData
			};

			return json;
		}

		public override string ToString() {

			Dictionary<string, Variant> jsonData = new Dictionary<string, Variant> {
				{ LABEL_VAULT_NAME, VaultName },
				{ LABEL_DATA, data },
				{ LABEL_CHECK_SUM, Checksum.GetChecksum(data) }
			};

			return Json.Stringify(jsonData);
		}

	}
}
