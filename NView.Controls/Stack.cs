using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Stack layout for NView. Stacks can be horizontal or vertical.
	/// </summary>
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
			throw Helpers.ThrowNotImplementedException ();
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		#endregion
	}
}

