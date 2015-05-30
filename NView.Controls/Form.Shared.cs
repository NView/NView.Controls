using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace NView.Controls
{
	public class Element : INotifyPropertyChanged
	{
		string text = "";
		public string Text { 
			get { return text; }
			set {
				var v = value ?? "";
				if (text != v) {
					text = v;
					OnPropertyChanged ("Text");
				}
			}
		}

		public Element (string text = "")
		{
			this.text = text ?? "";
		}

		#region INotifyPropertyChanged implementation

		protected virtual void OnPropertyChanged (string name)
		{
			PropertyChanged (this, new PropertyChangedEventArgs (name));
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate {};

		#endregion
	}

	public class Section : IEnumerable, IEnumerable<Element>
	{
		readonly List<Element> elements = new List<Element> ();

		string text = "";
		public string Text { 
			get { return text; }
			set {
				var v = value ?? "";
				if (text != v) {
					text = v;
					OnPropertyChanged ("Text");
				}
			}
		}

		public Section (string text)
		{
			this.text = text ?? "";
		}

		public int Count { get { return elements.Count; } }
		public Element this [int index] { get { return elements [index]; } }
		public void Add (Element element) {
			elements.Add (element);
		}

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return elements.GetEnumerator ();
		}

		IEnumerator<Element> IEnumerable<Element>.GetEnumerator ()
		{
			return elements.GetEnumerator ();
		}

		#endregion

		#region INotifyPropertyChanged implementation

		protected virtual void OnPropertyChanged (string name)
		{
			PropertyChanged (this, new PropertyChangedEventArgs (name));
		}

		public event PropertyChangedEventHandler PropertyChanged = delegate {};

		#endregion
	}

	public class RootElement : Element, IEnumerable, IEnumerable<Section>
	{
		readonly List<Section> sections = new List<Section> ();

		public RootElement (string text = "")
			: base (text)
		{
		}

		public int Count { get { return sections.Count; } }
		public Section this [int index] { get { return sections [index]; } }
		public void Add (Section section) {
			sections.Add (section);
		}

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return sections.GetEnumerator ();
		}

		IEnumerator<Section> IEnumerable<Section>.GetEnumerator ()
		{
			return sections.GetEnumerator ();
		}

		#endregion
	}
}

