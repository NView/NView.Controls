using System;
using Android.Runtime;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	[Preserve]
	public class Label : IView
	{
		Android.Widget.TextView textView;

		string text = string.Empty;

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get { return text; }
			set {
				text = value;
				if (textView == null)
					return;
				
				textView.Text = text ?? string.Empty; 
			}
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();
			
			textView = ViewHelpers.GetView<Android.Widget.TextView> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				text = textView.Text;

			} else {

				textView.Text = text;

			}
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			textView = null;
		}

		/// <inheritdoc/>
		public Type PreferredNativeType {
			get {
				return typeof(Android.Widget.TextView);
			}
		}

		#endregion

	}
}

