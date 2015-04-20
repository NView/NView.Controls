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

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();
			
			editText = ViewHelpers.GetView<Android.Widget.EditText> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				text = editText.Text;
				placeholder = editText.Hint;
				enabled = editText.Enabled;

			} else {

				editText.Text = text;
				editText.Hint = placeholder;
				editText.Enabled = enabled;

			}

			editText.TextChanged += EditText_TextChanged;
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			if (editText == null)
				return;
			editText.TextChanged -= EditText_TextChanged;
			editText = null;
		}

		void EditText_TextChanged (object sender, Android.Text.TextChangedEventArgs e)
		{
			if (editText != null)
				text = editText.Text;
			
			if (TextChanged == null)
				return;

			TextChanged (this, new EventArgs ());
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				return typeof(Android.Widget.EditText);
			}
		}

		#endregion

	}
}

