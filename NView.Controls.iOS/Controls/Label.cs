using System;
using UIKit;
using Foundation;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Text View for NView.
	/// </summary>
	[Preserve]
	public class Label : IView
	{
		UILabel label;
		string text = string.Empty;

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>The text.</value>
		public string Text {
			get { return text; }
			set { 
				text = value;
				if (label == null)
					return;
				
				label.Text = text ?? string.Empty; 
			}
		}

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			if (nativeView == null)
				throw new ArgumentNullException ("nativeView");

			UnbindFromNative ();
			
			label = ViewHelpers.GetView<UILabel> (nativeView);

			if (options.HasFlag (BindOptions.PreserveNativeProperties)) {

				text = label.Text;

			} else {

				label.Text = text;

			}
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			label = null;
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

