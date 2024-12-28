using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using DockingFrames.Design;

namespace VariIconsSDK.UI
{
	/// <summary>
	/// utility class for managing a set of toolstripitems
	/// and adding them to statusstrips or such
	/// </summary>
	[DesignTimeVisible(true), ToolboxItem(true),
	Designer(typeof(StripControlContainer), typeof(ComponentDesigner))]
	public class StripControlContainer : Component
	{
		#region types
		[Editor(typeof(StripCollectionEditor), typeof(UITypeEditor))]
		public class StripControlCollection : List<ToolStripItem> { }
		#endregion
		#region variables
		private StripControlCollection _items;
		private ToolStrip _strip;
		#endregion
		#region ctor
		public StripControlContainer()
		{
			_items = new StripControlCollection();
		}
		public StripControlContainer(IContainer container)
			: this()
		{
			container.Add(this);
		}
		protected override void Dispose(bool disposing)
		{
			foreach (ToolStripItem item in _items)
				item.Dispose();
			_items.Clear();
			base.Dispose(disposing);
		}
		#endregion
		/// <summary>
		/// removes all items from their owners
		/// </summary>
		public void ReaquireAll()
		{
			foreach (ToolStripItem item in _items)
				item.Owner = null;
			_strip = null;
		}
		#region properties
		/// <summary>
		/// gets or sets the strip to add the controls to
		/// </summary>
		[EditorBrowsable(EditorBrowsableState.Never),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ToolStrip Strip
		{
			get { return _strip; }
			set
			{
				if (_strip == value)
					return;
				//remove from old strip
				if (_strip != null && !DesignMode)
				{
					_strip.SuspendLayout();
					foreach (ToolStripItem item in _items)
						_strip.Items.Remove(item);
					_strip.ResumeLayout();
				}
				_strip = value;
				if (value != null && !DesignMode)
				{
					value.Items.AddRange(_items.ToArray());
				}
			}
		}
		/// <summary>
		/// gets the item collection
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public StripControlCollection Items
		{
			get { return _items; }
		}
		#endregion
	}
	/// <summary>
	/// designer for stripcontrolcontainer
	/// </summary>
	public class StripControlContainterDesigner : ComponentDesigner
	{
		public override System.Collections.ICollection AssociatedComponents
		{
			get
			{
				return ((StripControlContainer)Component).Items;
			}
		}
	}
	/// <summary>
	/// searchereditor with start item types
	/// </summary>
	public class StripCollectionEditor : SearcherEditor
	{
		public StripCollectionEditor(Type t)
			: base(t) { }
		protected override Type[] CreateNewItemTypes()
		{
			List<Type> types = new List<Type>();
			types.AddRange(new Type[]{
				typeof(ToolStripButton), typeof(ToolStripLabel),
				typeof(ToolStripSplitButton), typeof(ToolStripDropDownButton),
				typeof(ToolStripSeparator), typeof(ToolStripComboBox),
				typeof(ToolStripTextBox), typeof(ToolStripProgressBar) });
			AddSubTypes(types, typeof(ToolStripItem));
			return types.ToArray();
		}
	}
}
