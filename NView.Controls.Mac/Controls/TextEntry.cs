using System;
using AppKit;
using Foundation;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text Entry for NView.
	/// </summary>
	[Preserve]
	public class TextEntry : IView
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

		string placeholder = string.Empty;

		/// <summary>
		/// Gets or sets the placeholder text to display.
		/// </summary>
		/// <value>The placeholder text.</value>
		public string Placeholder {
			get { return placeholder; }
			set {
				placeholder = value;
				if (textField == null)
					return;

				textField.PlaceholderString = placeholder ?? string.Empty;
			}
		}

		bool enabled = true;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.TextEntry"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled {
			get { return enabled; }
			set { 
				enabled = value;
				if (textField == null)
					return;

				textField.Enabled = enabled; 
			}
		}

		/// <summary>
		/// Event that occurs when text has changed.
		/// </summary>
		public event EventHandler TextChanged;

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();
			
			textField = ViewHelpers.GetView<NSTextField> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				text = textField.StringValue;
				placeholder = textField.PlaceholderString;
				enabled = textField.Enabled;

			} else {

				textField.StringValue = text;
				textField.PlaceholderString = placeholder;
				textField.Enabled = enabled;

			}

			textField.Changed += TextField_Changed;
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (textField == null)
				return;
			textField.Changed -= TextField_Changed;
			textField = null;
		}

		void TextField_Changed (object sender, EventArgs e)
		{
			if (textField != null)
				text = textField.StringValue;
			
			if (TextChanged == null)
				return;

			TextChanged (this, new EventArgs ());
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

