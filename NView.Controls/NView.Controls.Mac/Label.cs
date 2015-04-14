using System;
using AppKit;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	public class Label : IView
	{
		private NSTextField textField;
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text 
		{
			get { return textField.StringValue; }
			set { textField.StringValue = value; }
		}

		#region IView implementation

		/// <summary>
		/// Binds the IView to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		public IDisposable BindToNative (object nativeView)
		{
			textField = ViewHelpers.GetView<NSTextField> (nativeView);


			return new DisposeAction (() => {
				textField = null;
			});
		}

		/// <summary>
		/// Gets the type of the preferred native control.
		/// </summary>
		/// <value>The type of the preferred native.</value>
		public Type PreferredNativeType {
			get {
				return typeof(NSTextField);
			}
		}

		#endregion

	}
}

