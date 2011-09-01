﻿// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.f
// See the License for the specific language governing permissions and
// limitations under the License.

#if !SILVERLIGHT
namespace Castle.Components.DictionaryAdapter.Xml
{
	using System.Xml;
	using System.Xml.XPath;
	using System;

	public class XmlDefaultBehaviorAccessor : XmlNodeAccessor
	{
		private readonly string localName;

		public XmlDefaultBehaviorAccessor(PropertyDescriptor property, IXmlKnownTypeMap knownTypes)
			: base(property.PropertyType, knownTypes)
		{
			this.localName = XmlConvert.EncodeLocalName(property.PropertyName);
		}

		public override string LocalName
		{
			get { return localName; }
		}

		public override IXmlCollectionAccessor GetCollectionAccessor(Type itemType)
		{
			return new XmlElementBehaviorAccessor(itemType);
		}

		protected internal override XmlIterator SelectPropertyNode(XPathNavigator node, bool create)
		{
			return Serializer.CanSerializeAsAttribute
				? new XmlNodeIterator   (node, this, false)
				: new XmlElementIterator(node, this, false);
		}

		protected internal override XmlIterator SelectCollectionNode(XPathNavigator node, bool create)
		{
			return Serializer.CanSerializeAsAttribute
				? new XmlNodeIterator   (node, this, false)
				: new XmlElementIterator(node, this, false);
		}

		protected internal override XmlIterator SelectCollectionItems(XPathNavigator node, bool create)
		{
			throw Error.NotSupported();
		}

		protected override bool IsMatch(XPathNavigator node)
		{
			return node.HasNameLike(localName);
		}
	}
}
#endif