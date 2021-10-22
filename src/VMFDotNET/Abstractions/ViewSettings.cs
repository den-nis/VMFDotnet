using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFDotNET.Abstractions
{
	public class ViewSettings
	{
		public bool SnapToGrid { get; set; }
		public bool ShowGrid { get; set; }
		public bool ShowLogicalGrid { get; set; }
		public int  GridSpacing { get; set; }
		public bool Show3DGrid { get; set; }
	}
}
