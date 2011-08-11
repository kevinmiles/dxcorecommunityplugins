using System;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using System.Text.RegularExpressions;

namespace CR_SuperSiblingNav
{
  public class ElementLocation
	{
		LanguageElementType _ElementType;
		bool _IsDetailNode;
		int _PreviousMatchingSiblings;
		ElementLocation _Child;
		CaretVector _Vector;

		private static int CalculatePreviousMatchingSiblings(ElementLocation elementLocation, LanguageElement testElement)
		{
			int previousSiblings = 0;
			NodeList nodes;
			LanguageElement testElementParent = testElement.Parent;
			if (testElementParent == null)
				return 0;

			if (testElement.IsDetailNode)
				nodes = testElementParent.DetailNodes;
			else
				nodes = testElementParent.Nodes;

			foreach (LanguageElement element in nodes)
			{
				if (element == testElement)
					break;
				if (element.ElementType == elementLocation._ElementType)
					previousSiblings++;
			}
			return previousSiblings;
		}
		private static void AddAllVectors(List<CaretVector> locations, LanguageElement element, SourcePoint target)
		{
			locations.Add(CaretVector.From(target, element.Range.Start, ElementPosition.Start));
			locations.Add(CaretVector.From(target, element.Range.End, ElementPosition.End));
			locations.Add(CaretVector.From(target, element.NameRange.Start, ElementPosition.NameRangeStart));
			locations.Add(CaretVector.From(target, element.NameRange.End, ElementPosition.NameRangeEnd));
			if (element is ICapableBlock)
			{
				ICapableBlock iCapableBlock = (ICapableBlock)element;
				if (iCapableBlock.HasDelimitedBlock)
				{
					locations.Add(CaretVector.From(target, iCapableBlock.BlockStart.End, ElementPosition.BlockBegin));
					locations.Add(CaretVector.From(target, iCapableBlock.BlockEnd.Start, ElementPosition.BlockEnd));
				}
			}
			if (element.FirstChild != null)
				locations.Add(CaretVector.From(target, element.FirstChild.Range.Start, ElementPosition.FirstChildStart));
			if (element.LastChild != null)
				locations.Add(CaretVector.From(target, element.LastChild.Range.End, ElementPosition.LastChildEnd));

			if (element.FirstDetail != null)
				locations.Add(CaretVector.From(target, element.FirstDetail.Range.Start, ElementPosition.FirstDetailStart));
			if (element.LastDetail != null)
				locations.Add(CaretVector.From(target, element.LastDetail.Range.End, ElementPosition.LastDetailEnd));
		}
		private static SourcePoint GetPosition(SourcePoint sourcePoint, CaretVector vector)
		{
			return sourcePoint.OffsetPoint(vector.LineDelta, vector.OffsetDelta);
		}
		private SourcePoint GetSourcePoint(LanguageElement element, CaretVector vector)
		{
			if (element == null || vector == null)
				return SourcePoint.Empty;
			ICapableBlock iCapableBlock = null;
      switch (vector.ElementPosition)
			{
				case ElementPosition.Start:
					return GetPosition(element.Range.Start, vector);
				case ElementPosition.End:
					return GetPosition(element.Range.End, vector);
				case ElementPosition.NameRangeStart:
					return GetPosition(element.NameRange.Start, vector);
				case ElementPosition.NameRangeEnd:
					return GetPosition(element.NameRange.End, vector);

				case ElementPosition.BlockBegin:
					iCapableBlock = element as ICapableBlock;
					if (iCapableBlock != null)
						return GetPosition(iCapableBlock.BlockStart.End, vector);
					else
						return GetPosition(element.Range.Start, vector);
				case ElementPosition.BlockEnd:
					iCapableBlock = element as ICapableBlock;
					if (iCapableBlock != null)
						return GetPosition(iCapableBlock.BlockEnd.Start, vector);
					else
						return GetPosition(element.Range.End, vector);
				case ElementPosition.FirstChildStart:
					LanguageElement firstChild = element.FirstChild;
					if (firstChild == null)
						return SourcePoint.Empty;
					return GetPosition(firstChild.Range.Start, vector);
				case ElementPosition.LastChildEnd:
					LanguageElement lastChild = element.LastChild;
					if (lastChild == null)
						return SourcePoint.Empty;
					return GetPosition(lastChild.Range.End, vector);
				case ElementPosition.FirstDetailStart:
					LanguageElement firstDetail = element.FirstDetail;
					if (firstDetail == null)
						return SourcePoint.Empty;
					return GetPosition(firstDetail.Range.Start, vector);
				case ElementPosition.LastDetailEnd:
					LanguageElement lastDetail = element.LastDetail;
					if (lastDetail == null)
						return SourcePoint.Empty;
					return GetPosition(lastDetail.Range.End, vector);
			}
			throw new Exception("Unexpected ElementPosition enum element.");
		}
		private LanguageElement FindNthElement(NodeList nodes, int count, LanguageElementType elementType)
		{
			LanguageElement lastElementFound = null;
      if (nodes == null)
				return null;
			int numFound = 0;
      foreach (LanguageElement element in nodes)
			{
				if (element.ElementType == elementType)
				{
					numFound++;
					if (numFound == count)
						return element;
					lastElementFound = element;
				}
			}
			return lastElementFound;
		}
    private LanguageElement GetChildElement(LanguageElement element, ElementLocation location)
		{
			if (element == null || location == null)
				return null;

			NodeList nodes;
			if (location._IsDetailNode)
				nodes = element.DetailNodes;
			else
				nodes = element.Nodes;

			return FindNthElement(nodes, location._PreviousMatchingSiblings + 1, location._ElementType);
		}
		private SourcePoint GetDefaultPosition(LanguageElement element)
		{
			return GetSourcePoint(element, new CaretVector(_Vector.ElementPosition));
		}
    public SourcePoint GetBestLocation(LanguageElement element)
		{
			int smallestLineDeltaSoFar = int.MaxValue;
			int smallestOffsetDeltaSoFar = int.MaxValue;

			ElementLocation location = this;
			SourcePoint result = SourcePoint.Empty;
			LanguageElement parent = null;
			while (location != null && element != null)
			{
				CaretVector vector = location._Vector;
				int lineDeltaSize = Math.Abs(vector.LineDelta);
				int offsetDeltaSize = Math.Abs(vector.OffsetDelta);

				if (lineDeltaSize < smallestLineDeltaSoFar || (lineDeltaSize == smallestLineDeltaSoFar && offsetDeltaSize < smallestOffsetDeltaSoFar))
				{
					// Found a smaller vector...
					SourcePoint newPosition = GetSourcePoint(element, vector);
					if (newPosition != SourcePoint.Empty)
					{
						result = newPosition;
						smallestLineDeltaSoFar = lineDeltaSize;
						smallestOffsetDeltaSoFar = offsetDeltaSize;

						if (lineDeltaSize == 0 && offsetDeltaSize == 0)
							if (vector.ElementPosition != ElementPosition.LastDetailEnd || element.ElementType != LanguageElementType.Method && element.ElementType != LanguageElementType.Property)
								return result;
					}
				}
				location = location._Child;
				parent = element;
				element = GetChildElement(element, location);
				if (location != null && element == null)
				{
					// We were expecting a child but found nothing...
					if (location._ElementType == LanguageElementType.Parameter)
					{
						Method method = parent as Method;
						if (method != null)
							return method.ParamOpenRange.End;
						else
						{
							Property property = parent as Property;
							if (property != null)
								return property.NameRange.End;
						}
					}
					else
					{
						SourcePoint newDefaultPosition = GetDefaultPosition(parent);
						if (newDefaultPosition != SourcePoint.Empty)
							return newDefaultPosition;
					}
				}
			}

			return result;
		}
    private static CaretVector GetClosestVector(LanguageElement element, SourcePoint target)
		{
			List<CaretVector> locations = new List<CaretVector>();
			AddAllVectors(locations, element, target);
			locations.Sort(new VectorComparer());
			if (locations.Count > 0)
				return locations[0];
			return null;
		}
    private static ElementLocation FromLanguageElement(LanguageElement element, SourcePoint target)
		{
			ElementLocation elementLocation = new ElementLocation();
			elementLocation._ElementType = element.ElementType;
			elementLocation._IsDetailNode = element.IsDetailNode;
			elementLocation._PreviousMatchingSiblings = CalculatePreviousMatchingSiblings(elementLocation, element);
			elementLocation._Vector = GetClosestVector(element, target);
			return elementLocation;
		}
		public static ElementLocation From(LanguageElement methodOrProperty, SourcePoint sourcePoint)
		{
			LanguageElement element = CodeRush.Source.GetNodeAt(sourcePoint);
			if (element == null)
				return null;
			ElementLocation elementLocation = null;
			ElementLocation childLocation = null;

			while (true)
			{
				elementLocation = FromLanguageElement(element, sourcePoint);
				elementLocation._Child = childLocation;
				childLocation = elementLocation;
				if (element == methodOrProperty)
					break;
				element = element.Parent;
				if (element == null)
					break;
			}
			return elementLocation;
		}
	}
}
