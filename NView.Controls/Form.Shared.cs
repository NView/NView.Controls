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

		public Element ()
		{
		}

		public Element (string text)
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

		string footerText = "";
		public string FooterText { 
			get { return footerText; }
			set {
				var v = value ?? "";
				if (footerText != v) {
					footerText = v;
					OnPropertyChanged ("FooterText");
				}
			}
		}

		public Section ()
		{
		}

		public Section (string text, string footerText = "")
		{
			this.text = text ?? "";
			this.footerText = footerText ?? "";
		}

		public int Count { get { return elements.Count; } }
		public Element this [int index] { get { return elements [index]; } }
		public void Add (Element element) {
			elements.Add (element);
		}
		public void Add (IEnumerable<Element> elements) {
			this.elements.AddRange (elements);
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

	public class Group
	{
	}

	#region Elements

	public class RootElement : Element, IEnumerable, IEnumerable<Section>
	{
		readonly List<Section> sections = new List<Section> ();

		readonly int summarySection;
		readonly int summaryElement;

		readonly Group group;

		public RootElement ()
			: base ("")
		{
		}

		public RootElement (string text)
			: base (text)
		{
		}

		public RootElement (string text, Group group)
			: base (text)
		{
			this.group = group;
		}

		public RootElement (string text, int summarySection, int summaryElement)
			: base (text)
		{
			this.summarySection = summarySection;
			this.summaryElement = summaryElement;
		}

		public int Count { get { return sections.Count; } }
		public Section this [int index] { get { return sections [index]; } }
		public void Add (Section section) {
			sections.Add (section);
		}
		public void Add (IEnumerable<Section> sections) {
			this.sections.AddRange (sections);
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

	public class RadioGroup : Group
	{
		int selectedIndex = 0;

		public RadioGroup ()
		{
		}

		public RadioGroup (int selectedIndex)
		{
			this.selectedIndex = selectedIndex;
		}
	}

	public class RadioElement : Element
	{
		public RadioElement ()
		{
		}

		public RadioElement (string text)
			: base (text)
		{
		}
	}

	public class BooleanElement : Element
	{
		bool value;

		public BooleanElement (string text = "", bool value = false)
			: base (text)
		{
			this.value = value;
		}
	}

	public class FloatElement : Element
	{
		double value = 0.0;
		object minImage = null;
		object maxImage = null;

		public FloatElement ()
		{
		}

		public FloatElement (double value)
		{
			this.value = value;
		}

		public FloatElement (object minImage, object maxImage, double value)
		{
			this.minImage = minImage;
			this.maxImage = maxImage;
			this.value = value;
		}
	}

	public class EntryElement : Element
	{
		bool password = false;

		string placeholderText = "";
		string value = "";

		public EntryElement ()
		{
		}

		public EntryElement (string value)
		{
			this.value = value ?? "";
		}

		public EntryElement (string text, string placeholderText, string value, bool password = false)
			: base (text)
		{
			this.placeholderText = placeholderText;
			this.value = value;
			this.password = password;
		}
	}

	public class DateElement : Element
	{
		DateTime value = DateTime.Now;

		public DateElement ()
		{
		}

		public DateElement (DateTime value)
		{
			this.value = value;
		}

		public DateElement (string text, DateTime value)
			: base (text)
		{
			this.value = value;
		}
	}

	#endregion
}

