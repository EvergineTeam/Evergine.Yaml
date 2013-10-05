using System;
using System.Collections.Generic;

namespace YamlDotNet.Serialization.Descriptors
{
	/// <summary>
	/// Provides a descriptor for a <see cref="System.Collections.ICollection"/>.
	/// </summary>
	public class CollectionDescriptor : ObjectDescriptor
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CollectionDescriptor" /> class.
		/// </summary>
		/// <param name="attributeRegistry">The attribute registry.</param>
		/// <param name="type">The type.</param>
		public CollectionDescriptor(IAttributeRegistry attributeRegistry, Type type)
			: base(attributeRegistry, type)
		{
			var member = this["Capacity"] as PropertyDescriptor;

			// Gets the element type
			var collectionType = type.GetInterface(typeof(ICollection<>));
			ElementType = (collectionType != null) ? collectionType.GetGenericArguments()[0] : typeof(object);

			// Finds if it is a pure list
			var listType = type.GetInterface(typeof(IList<>));
			IsPureCollection = (Count == 1 && member != null && listType != null);
		}

		/// <summary>
		/// Gets or sets the type of the element.
		/// </summary>
		/// <value>The type of the element.</value>
		public Type ElementType { get; set; }


		/// <summary>
		/// Gets a value indicating whether this instance is a pure collection (no public property/field)
		/// </summary>
		/// <value><c>true</c> if this instance is pure collection; otherwise, <c>false</c>.</value>
		public bool IsPureCollection { get; private set; }
	}
}