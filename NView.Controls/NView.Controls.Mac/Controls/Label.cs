using System;
using AppKit;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	public class Label : IView
	{
		NSTextField textField;

		string text = string.Empty;

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get { return text; }
			set { 
				text = value;
				if (textField == null)
					return;

				textField.StringValue = text ?? string.Empty; 
			}
		}

		#region IView implementation

		/// <summary>
		/// Binds the IView to a native view.
		/// </summary>
		/// <returns>A disposable view</returns>
		/// <param name="nativeView">Native view to bind with.</param>
		public IDisposable BindToNative (object nativeView)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");
			
			textField = ViewHelpers.GetView<NSTextField> (nativeView);

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (textField.StringValue)) {
				textField.StringValue = Text;
			} else {
				text = textField.StringValue;
			}

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

