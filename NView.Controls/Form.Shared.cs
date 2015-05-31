using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

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

		string detailText = "";
		public string DetailText { 
			get { return detailText; }
			set {
				var v = value ?? "";
				if (detailText != v) {
					detailText = v;
					OnPropertyChanged ("DetailText");
				}
			}
		}

		string valueText = "";
		public string ValueText { 
			get { return valueText; }
			set {
				var v = value ?? "";
				if (valueText != v) {
					valueText = v;
					OnPropertyChanged ("ValueText");
				}
			}
		}

		IView valueView = null;
		public IView ValueView { 
			get { return valueView; }
			set {
				if (valueView != value) {
					valueView = value;
					OnPropertyChanged ("ValueView");
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

		public void Select ()
		{
			OnSelect ();
		}

		protected virtual void OnSelect ()
		{
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

		public bool IsAction { get; set; }

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

		protected virtual List<Section> GetSections ()
		{
			return sections;
		}

		public int Count { get { return GetSections ().Count; } }
		public Section this [int index] { get { return GetSections ()[index]; } }
		public void Add (Section section) {
			GetSections ().Add (section);
		}
		public void Add (IEnumerable<Section> sections) {
			GetSections ().AddRange (sections);
		}

		#region IEnumerable implementation

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetSections ().GetEnumerator ();
		}

		IEnumerator<Section> IEnumerable<Section>.GetEnumerator ()
		{
			return GetSections ().GetEnumerator ();
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
		public bool Value {
			get { return ((Switch)ValueView).Checked; }
			set { ((Switch)ValueView).Checked = value; }
		}

		public bool Enabled {
			get { return ((Switch)ValueView).Enabled; }
			set { ((Switch)ValueView).Enabled = value; }
		}

		public BooleanElement (string text = "", bool value = false)
			: base (text)
		{
			ValueView = new Switch {
				Checked = value,
			};
		}
	}

	public class FloatElement : Element
	{
		double value = 0.0;
		object minImage = null;
		object maxImage = null;

		public FloatElement ()
		{
			ValueText = value.ToString ();
		}

		public FloatElement (double value)
		{
			this.value = value;
			ValueText = value.ToString ();
		}

		public FloatElement (object minImage, object maxImage, double value)
		{
			this.minImage = minImage;
			this.maxImage = maxImage;
			this.value = value;
			ValueText = value.ToString ();
		}
	}

	public class EntryElement : Element
	{
		bool password = false;

		string placeholderText = "";
		string value = "";

		public EntryElement ()
		{
			ValueText = "";
		}

		public EntryElement (string value)
		{
			this.value = value ?? "";
			ValueText = this.value;
		}

		public EntryElement (string text, string placeholderText, string value, bool password = false)
			: base (text)
		{
			this.placeholderText = placeholderText ?? "";
			this.value = value ?? "";
			this.password = password;
			ValueText = this.value;
		}
	}

	public class DateElement : Element
	{
		DateTime value = DateTime.Now;

		public DateElement ()
		{
			ValueText = value.ToString ();
		}

		public DateElement (DateTime value)
		{
			this.value = value;
			ValueText = value.ToString ();
		}

		public DateElement (string text, DateTime value)
			: base (text)
		{
			this.value = value;
			ValueText = value.ToString ();
		}
	}

	public class ObjectElement : RootElement
	{
		object value = null;
		bool needsEval = true;
		List<Section> sections = new List<Section> ();

		public ObjectElement ()
		{
		}

		public ObjectElement (string title, object value)
			: base (title)
		{
			this.value = value;
		}

		class Mirror
		{
			public static readonly Mirror Null = new Mirror (typeof(object));

			public readonly Type MirroredType;

			public List<Porp> Properties = new List<Porp> ();
			public List<Dohtem> Methods = new List<Dohtem> ();
			public Mirror BaseMirror;

			public class Porp
			{
				public string Name;
				public Mirror Mirror;
				public Func<object, object> Get;
				public Action<object, object> Set;
				public bool CanSet { get { return Set != null; } }
			}

			public class Dohtem
			{
				public string Name;
				public Mirror Mirror;
				public Func<object, object[], object> Get;
			}

			public Mirror (Type type)
			{
				this.MirroredType = type;
			}

			void Fill ()
			{
				var info = MirroredType.GetTypeInfo ();

				BaseMirror = Get (info.BaseType);

				var props = from p in info.DeclaredProperties
						where p.GetMethod != null && p.GetMethod.IsPublic
							orderby p.Name
				             select new Porp {
					Name = p.Name,
					Mirror = Get (p.PropertyType),
					Get = x => p.GetValue (x),
				};
				Properties = props.ToList ();

				var methods = from p in info.DeclaredMethods
						where p.IsPublic && !p.Name.StartsWith ("get_") &&
					!p.Name.StartsWith ("set_") &&
					!p.Name.StartsWith ("add_") && !p.Name.StartsWith ("remove_")
					orderby p.Name
						
					select new Dohtem {
					Name = p.Name,
					Mirror = Get (p.ReturnType),
					Get = p.Invoke,
				};
				Methods = methods.ToList ();
			}

			static readonly Dictionary<Type, Mirror> mirrors = new Dictionary<Type, Mirror> ();
			public static Mirror Get (Type type)
			{
				if (type == null)
					return null;
				Mirror m;
				if (!mirrors.TryGetValue (type, out m)) {
					m = new Mirror (type);
					mirrors.Add (type, m);
					m.Fill ();
				}
				return m;
			}
		}

		List<Section> Eval ()
		{
			var r = new List<Section> ();

			var mirror = value == null ? Mirror.Null : Mirror.Get (value.GetType ());

			var props = new Section ("Properties");
			r.Add (props);
			foreach (var p in mirror.Properties) {
				object pv = null;
				try {
					pv = p.Get (value);
				} catch (Exception ex) {
					pv = ex;
				}
				Element elm;
				if (pv == null) {
					elm = new Element (p.Name) { ValueText = "null" };
				} else if (pv is bool) {
					elm = new BooleanElement (p.Name, (bool)pv) {
						Enabled = p.CanSet,
					};
				} else {
					elm = new Element (p.Name) { ValueText = pv.ToString (), };
				}
				props.Add (elm);
			}

			var methods = new Section ("Methods");
			r.Add (methods);
			foreach (var m in mirror.Methods) {
				var elm = new MethodElement (m.Name, m.Get);
				methods.Add (elm);
			}

			return r;
		}

		protected override List<Section> GetSections ()
		{
			if (needsEval) {
				try {
					sections = Eval ();
				} catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex);
					sections = new List<Section> ();
				}
				needsEval = true;
			}
			return sections;
		}
	}

	public class MethodElement : RootElement
	{
		Func<object, object[], object> get = null;
		bool needsEval = true;
		List<Section> sections = new List<Section> ();

		public MethodElement ()
		{
			IsAction = true;
		}

		public MethodElement (string title, Func<object, object[], object> get)
			: base (title)
		{
			this.get = get;
			IsAction = true;
		}

		List<Section> Eval ()
		{
			var r = new List<Section> ();
			return r;
		}

		protected override List<Section> GetSections ()
		{
			if (needsEval) {
				try {
					sections = Eval ();
				} catch (Exception ex) {
					System.Diagnostics.Debug.WriteLine (ex);
					sections = new List<Section> ();
				}
				needsEval = true;
			}
			return sections;
		}
	}

	#endregion
}

