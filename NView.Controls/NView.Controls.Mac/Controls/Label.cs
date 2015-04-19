using System;
using AppKit;
using Foundation;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	[Preserve]
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

		/// <inheritdoc/>
		public IDisposable BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");
			
			textField = ViewHelpers.GetView<NSTextField> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				text = textField.StringValue;

			} else {

				textField.StringValue = text;

			}

			return new DisposeAction (() => {
				textField = null;
			});
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				return typeof(NSTextField);
			}
		}

		#endregion

	}
}

