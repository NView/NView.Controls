using System;

namespace NView.Controls
{
	/// <summary>
	/// Cross platform Switch/Toggle for NView
	/// </summary>
	public class Toggle : IView 
	{

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Toggle"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NView.Controls.Toggle"/> is checked.
		/// </summary>
		/// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
		public bool Checked { get; set; }

		//ignore warning as this is just a stub class
		#pragma warning disable 67 
		/// <summary>
		/// Event that occurs when checked/toggle changed.
		/// </summary>
		public event EventHandler CheckedChanged;
		#pragma warning restore

		#region IView implementation

		/// <inheritdoc/>
		public void BindToNative (object nativeView, BindOptions options = BindOptions.None)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public void UnbindFromNative ()
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		/// <inheritdoc/>
		public object CreateNative (object context = null)
		{
			throw Helpers.ThrowNotImplementedException ();
		}

		#endregion
	}
}

