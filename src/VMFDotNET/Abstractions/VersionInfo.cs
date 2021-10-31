using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class VersionInfo : VmfContainer
	{
		[VmfName("editorversion")]
		public int EditorVersion { get; set; }

		[VmfName("editorbuild")]
		public int EditorBuild { get; set; }

		[VmfName("mapversion")]
		public int MapVersion { get; set; }

		[VmfName("formatversion")]
		public int FormatVersion { get; set; } = 100;

		[VmfName("prefab")]
		public int Prefab { get; set; }
	}
}