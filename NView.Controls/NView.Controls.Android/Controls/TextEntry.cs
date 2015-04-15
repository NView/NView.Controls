using System;
using Android.Runtime;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text Entry for NView.
	/// </summary>
	[Preserve]
	public class TextEntry : IView
	{
		Android.Widget.EditText editText;
		string text = string.Empty;

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get { return text; }
			set {
				text = value;
				if (editText == null)
					return;

				editText.Text = text ?? string.Empty;
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
				if (editText == null)
					return;

				editText.Hint = placeholder ?? string.Empty;
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
				if (editText == null)
					return;
				
				editText.Enabled = enabled; 
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
			
			editText = ViewHelpers.GetView<Android.Widget.EditText> (nativeView);

			//Toggle enabled if needed
			if (editText.Enabled != Enabled) {
				Enabled = editText.Enabled;
			}

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (editText.Text)) {
				editText.Text = Text;
			} else {
				text = editText.Text;
			}

			//If the user didn't set text, set local version, 
			//else we want to take in the button text to sync
			if (string.IsNullOrEmpty (editText.Hint)) {
				editText.Hint = Placeholder;
			} else {
				placeholder = editText.Hint;
			}

			editText.TextChanged += EditText_TextChanged;

			return new DisposeAction (() => {
				editText.TextChanged -= EditText_TextChanged;
				editText = null;
			});
		}

		void EditText_TextChanged (object sender, Android.Text.TextChangedEventArgs e)
		{
			if (editText != null)
				text = editText.Text;
			
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
				return typeof(Android.Widget.EditText);
			}
		}

		#endregion

	}
}

