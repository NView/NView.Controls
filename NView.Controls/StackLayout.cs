using System;

namespace NView.Controls
{
	/// <summary>
	/// Layout attributes to assign to children contained in a <see cref="Stack"/>.
	/// Do not modify any properties after adding it to the <see cref="Stack"/>.
	/// </summary>
	public class StackLayout
	{
		/// <summary>
		/// Gets the horizontal alignment. Defaults to Left.
		/// </summary>
		public HorizontalAlignment HorizontalAlignment { get; set; }

		/// <summary>
		/// Gets the vertical alignment. Defaults to Top.
		/// </summary>
		public VerticalAlignment VerticalAlignment { get; set; }

		/// <summary>
		/// Gets the width. Defaults to WrapContent.
		/// </summary>
		public StackLayoutSize Width { get; set; }

		/// <summary>
		/// Gets the height. Defaults to WrapContent.
		/// </summary>
		public StackLayoutSize Height { get; set; }

		/// <summary>
		/// Defaults to a top-left layout that wraps its content.
		/// </summary>
		public StackLayout ()
		{
			HorizontalAlignment = HorizontalAlignment.Left;
			VerticalAlignment = VerticalAlignment.Top;
			Width = StackLayoutSize.WrapContent ();
			Height = StackLayoutSize.WrapContent ();
		}
	}

	/// <summary>
	/// Possible ways to size children of a <see cref="Stack"/>.
	/// </summary>
	public enum StackLayoutSizeType
	{
		/// <summary>
		/// The size is specified in absolute points. (160 points per inch.)
		/// </summary>
		Absolute,
		/// <summary>
		/// The size is specified as a proportion of the parent and should range from 0 (empty) to 1 (full sized).
		/// </summary>
		RelativeToParent,
		/// <summary>
		/// Size that wraps the content of the view.
		/// </summary>
		WrapContent,
	}

	/// <summary>
	/// Size of a view inside of a <see cref="Stack"/>
	/// </summary>
	public struct StackLayoutSize
	{
		/// <summary>
		/// Which of the possible ways to specify this size.
		/// </summary>
		/// <value>The type of the size.</value>
		public StackLayoutSizeType SizeType;

		/// <summary>
		/// The generic value for the size.
		/// For absolute sizes, this is in points (160 points/inch).
		/// For relative sizes, this is a proportion between 0 and 1.
		/// For wrapped sizes, this value is ignored.
		/// </summary>
		public double Value;

		/// <summary>
		/// Initializes a new instance of the <see cref="NView.Controls.StackLayoutSize"/> class.
		/// </summary>
		/// <param name="sizeType">Size type.</param>
		/// <param name="value">
		/// The generic value for the size.
		/// For absolute sizes, this is in points (160 points/inch).
		/// For relative sizes, this is a proportion between 0 and 1.
		/// For wrapped sizes, this value is ignored.
		/// </param>
		public StackLayoutSize (StackLayoutSizeType sizeType, double value)
		{
			SizeType = sizeType;
			Value = value;
		}

		/// <summary>
		/// Creates an absolute size specified in points where there are 160 points per inch.
		/// </summary>
		/// <param name="points">Points (160 points / inch).</param>
		public static StackLayoutSize Absolute (double points)
		{
			return new StackLayoutSize (StackLayoutSizeType.Absolute, points);
		}

		/// <summary>
		/// Creates a size that is relative to the parent's size.
		/// The proportion is specified as a value in the range 0 to 1.
		/// </summary>
		/// <param name="proportion">The proportion of the parent's size (0 to 1).</param>
		public static StackLayoutSize RelativeToParent (double proportion)
		{
			return new StackLayoutSize (StackLayoutSizeType.RelativeToParent, proportion);
		}

		/// <summary>
		/// Creates a size that wraps the view's content.
		/// </summary>
		public static StackLayoutSize WrapContent ()
		{
			return new StackLayoutSize (StackLayoutSizeType.WrapContent, 0.0);
		}

		/// <inheritdoc/>
		public override string ToString ()
		{
			switch (SizeType) {
			case StackLayoutSizeType.Absolute:
				return string.Format ("{0} points", Value);
			case StackLayoutSizeType.RelativeToParent:
				return string.Format ("{0:P}", Value * 100);
			case StackLayoutSizeType.WrapContent:
				return "Wrap Content";
			default:
				return "???";
			}
		}
	}

	/// <summary>
	/// Orientation of a <see cref="Stack"/>.
	/// </summary>
	public enum StackOrientation
	{
		/// <summary>
		/// Horizontal orientation.
		/// </summary>
		Horizontal,
		/// <summary>
		/// Vertical orientation.
		/// </summary>
		Vertical,
	}

	/// <summary>
	/// Horizontal alignment of a child contained in a parent view.
	/// </summary>
	public enum HorizontalAlignment
	{
		/// <summary>
		/// Align the view towards the left of the parent view.
		/// </summary>
		Left,
		/// <summary>
		/// Align the view towards the horizontal center of the parent view.
		/// </summary>
		Center,
		/// <summary>
		/// Align the view towards the right of the parent view.
		/// </summary>
		Right,

// 		Leading,
//		Trailing,
	}

	/// <summary>
	/// Vertical alignment of a child contained in a parent view.
	/// </summary>
	public enum VerticalAlignment
	{
		/// <summary>
		/// Align the view towards the top of the parent view.
		/// </summary>
		Top,
		/// <summary>
		/// Align the view towards the vertical center of the parent view.
		/// </summary>
		Center,
		/// <summary>
		/// Align the view towards the bottom of the parent view.
		/// </summary>
		Bottom,
	}
}

