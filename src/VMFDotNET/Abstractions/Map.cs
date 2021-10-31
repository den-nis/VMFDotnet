using System.Collections.Generic;
using VMFDotNET.Attributes;
using VMFDotNET.Serialization;

namespace VMFDotNET.Abstractions
{
	public class Map : VmfContainer
	{
		[VmfName("versioninfo")]
		[VmfIncludeIfNull]
		public VersionInfo VersionInfo { get; set; }

		[VmfName("visgroups")]
		[VmfIncludeIfNull]
		public VisGroups VisGroups { get; set; }

		[VmfName("viewsettings")]
		[VmfIncludeIfNull]
		public ViewSettings ViewSettings { get; set; }

		[VmfName("world")]
		[VmfIncludeIfNull]
		public World World { get; set; }

		[VmfName("cameras")]
		[VmfIncludeIfNull]
		public Cameras Cameras { get; set; }

		[VmfName("cordon")]
		[VmfIncludeIfNull]
		public Cordon Cordon { get; set; }

		[VmfName("entity")]
		public IList<Entity> Entities { get; set; }
	}
}
