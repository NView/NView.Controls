using System;
using UIKit;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	public class Label : IView
	{
		private UILabel label;
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get { return label.Text; }
			set { label.Text = value; }
		}

		#region IView implementation

		/// <summary>
		/// Binds the IView to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		public IDisposable BindToNative (object nativeView)
		{
			label = ViewHelpers.GetView<UILabel> (nativeView);


			return new DisposeAction (() => {
				label = null;
			});
		}

		/// <summary>
		/// Gets the type of the preferred native control.
		/// </summary>
		/// <value>The type of the preferred native.</value>
		public Type PreferredNativeType {
			get {
				return typeof(UILabel);
			}
		}

		#endregion

	}
}

