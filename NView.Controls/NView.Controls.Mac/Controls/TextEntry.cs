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

			//Toggle enabled if needed
			if (textField.Enabled != Enabled) {
				Enabled = textField.Enabled;
			}

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (textField.StringValue)) {
				textField.StringValue = Text;
			} else {
				text = textField.StringValue;
			}

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (textField.PlaceholderString)) {
				textField.PlaceholderString = Placeholder;
			} else {
				placeholder = textField.PlaceholderString;
			}


			textField.Changed += TextField_Changed;

			return new DisposeAction (() => {
				textField.Changed -= TextField_Changed;
				textField = null;
			});
		}

		void TextField_Changed (object sender, EventArgs e)
		{
			if (textField != null)
				text = textField.StringValue;
			
			if (TextChanged == null)
				return;

			TextChanged (this, new EventArgs ());
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

