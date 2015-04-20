using System;

using Android.Widget;
using Android.Runtime;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Stack layout for NView. Stacks can be horizontal or vertical.
	/// </summary>
	[Preserve]
	public class Stack : IView
	{
		/// <summary>
		/// The orientation of the <see cref="Stack"/>.
		/// </summary>
		public StackOrientation Orientation { get; set; }

		/// <summary>
		/// Add a child view to this stack with the given layout.
		/// </summary>
		/// <param name="child">The child view.</param>
		/// <param name="layout">The layout to associate with the child.</param>
		public void AddChild (IView child, StackLayout layout)
		{
			throw new NotImplementedException ();
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			throw new NotImplementedException ();
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			throw new NotImplementedException ();
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				return typeof(LinearLayout);
			}
		}

		#endregion
	}
}

