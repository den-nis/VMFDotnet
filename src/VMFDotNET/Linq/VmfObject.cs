using System.Collections.Generic;
using System.Linq;

namespace VMFDotNET.Linq
{
	public class VmfObject
	{
		public string Name { get; internal set; }

		public VmfObject Parent { get; internal set; }

		public IEnumerable<VmfObject> Children => _children;

		public IEnumerable<VmfProperty> Properties => _allProperties;

		private readonly List<VmfProperty> _allProperties = new();
		private readonly Dictionary<string, string> _propertyLookup = new();
		private readonly List<VmfObject> _children = new();

		public string GetValue(string property) => _propertyLookup[property];

		public bool HasProperty(string property) => _propertyLookup.ContainsKey(property);

		public void Add(VmfObject obj) => _children.Add(obj);

		public void AddProperty(string name, string value)
		{
			_propertyLookup.TryAdd(name, value);
			_allProperties.Add(new VmfProperty
			{
				Name = name,
				Value = value,
			});
		}

		public void RemoveProperty(string name)
		{
			if (HasProperty(name))
			{
				_propertyLookup.Remove(name);
				_allProperties.RemoveAll(p => p.Name == name);
			}
		}

		public void RemovePropertyWithValue(string name, string value)
		{
			if (HasProperty(name))
			{
				if (_propertyLookup[name] == value) 
				{
					_propertyLookup.Remove(name);
					var property = _allProperties.First(p => p.Name == name && p.Value == value);
					_propertyLookup.Add(name, property.Value);
				}	
				else
				{
					_allProperties.RemoveAll(p => p.Name == name && p.Value == value);
				}
			}
		}
	}
}
