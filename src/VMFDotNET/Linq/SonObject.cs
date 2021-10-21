using System.Collections.Generic;
using System.Linq;

namespace VMFDotNET.Linq
{
	public class SonObject
	{
		public string Name { get; internal set; }

		public SonObject Parent { get; internal set; }

		public IEnumerable<SonObject> Children => _children;

		public IEnumerable<SonProperty> Properties => _properties
			.Select(p => new SonProperty
			{
				Name = p.Key,
				Value = p.Value,
			});

		public string GetValue(string property) => _properties[property];

		public bool HasValue(string property) => _properties.ContainsKey(property);

		internal void Add(SonObject obj) => _children.Add(obj);

		internal void AddProperty(string name, string value) => _properties.Add(name, value);

		private readonly Dictionary<string, string> _properties = new();

		private readonly List<SonObject> _children = new();
	}
}
