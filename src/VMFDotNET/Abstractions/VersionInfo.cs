using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMFDotNET.Linq;

namespace VMFDotNET.Abstractions
{
	public class VersionInfo
	{
		public int EditorVersion { get; set; }
		public int EditorBuild { get; set; }
		public int MapVersion { get; set; }
		public int FormatVersion { get; set; } = 100;
		public int Prefab { get; set; }
	}
}
