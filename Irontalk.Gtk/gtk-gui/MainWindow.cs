
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;

	private global::Gtk.Action FileAction;

	private global::Gtk.Action stopAction;

	private global::Gtk.Action OptionsAction;

	private global::Gtk.ToggleAction WordWrapAction;

	private global::Gtk.Action ModeAction;

	private global::Gtk.RadioAction EvaluateAction;

	private global::Gtk.RadioAction ParseOnlyAction;

	private global::Gtk.ToggleAction ShowParseTreeBeforeEvaluatingAction;

	private global::Gtk.ToggleAction PrintInputBeforeEvaluatingAction;

	private global::Gtk.VPaned vpaned1;

	private global::Gtk.VBox vbox1;

	private global::Gtk.MenuBar menubar1;

	private global::Gtk.ScrolledWindow GtkScrolledWindow;

	private global::Gtk.TextView output;

	private global::Gtk.Notebook notebook1;

	private global::Gtk.VBox vbox2;

	private global::Gtk.HBox hbox1;

	private global::Gtk.Entry input;

	private global::Gtk.Button eval;

	private global::Gtk.Label label1;

	private global::Gtk.HBox hbox3;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView multiInput;

	private global::Gtk.Button evalMulti;

	private global::Gtk.Label label2;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.FileAction = new global::Gtk.Action ("FileAction", global::Mono.Unix.Catalog.GetString ("File"), null, null);
		this.FileAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("File");
		w1.Add (this.FileAction, null);
		this.stopAction = new global::Gtk.Action ("stopAction", global::Mono.Unix.Catalog.GetString ("Exit"), null, "gtk-stop");
		this.stopAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Exit");
		w1.Add (this.stopAction, null);
		this.OptionsAction = new global::Gtk.Action ("OptionsAction", global::Mono.Unix.Catalog.GetString ("Options"), null, null);
		this.OptionsAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Options");
		w1.Add (this.OptionsAction, null);
		this.WordWrapAction = new global::Gtk.ToggleAction ("WordWrapAction", global::Mono.Unix.Catalog.GetString ("Word wrap"), null, null);
		this.WordWrapAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Word wrap");
		w1.Add (this.WordWrapAction, null);
		this.ModeAction = new global::Gtk.Action ("ModeAction", global::Mono.Unix.Catalog.GetString ("Mode"), null, null);
		this.ModeAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Mode");
		w1.Add (this.ModeAction, null);
		this.EvaluateAction = new global::Gtk.RadioAction ("EvaluateAction", global::Mono.Unix.Catalog.GetString ("Evaluate"), null, null, 0);
		this.EvaluateAction.Group = new global::GLib.SList (global::System.IntPtr.Zero);
		this.EvaluateAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Evaluate");
		w1.Add (this.EvaluateAction, null);
		this.ParseOnlyAction = new global::Gtk.RadioAction ("ParseOnlyAction", global::Mono.Unix.Catalog.GetString ("Parse only"), null, null, 0);
		this.ParseOnlyAction.Group = this.EvaluateAction.Group;
		this.ParseOnlyAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Parse only");
		w1.Add (this.ParseOnlyAction, null);
		this.ShowParseTreeBeforeEvaluatingAction = new global::Gtk.ToggleAction ("ShowParseTreeBeforeEvaluatingAction", global::Mono.Unix.Catalog.GetString ("Show parse tree before evaluating"), null, null);
		this.ShowParseTreeBeforeEvaluatingAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Show parse tree before evaluating");
		w1.Add (this.ShowParseTreeBeforeEvaluatingAction, null);
		this.PrintInputBeforeEvaluatingAction = new global::Gtk.ToggleAction ("PrintInputBeforeEvaluatingAction", global::Mono.Unix.Catalog.GetString ("Print input before evaluating"), null, null);
		this.PrintInputBeforeEvaluatingAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Print input before evaluating");
		w1.Add (this.PrintInputBeforeEvaluatingAction, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("Irontalk");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vpaned1 = new global::Gtk.VPaned ();
		this.vpaned1.CanFocus = true;
		this.vpaned1.Name = "vpaned1";
		this.vpaned1.Position = 195;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubar1'><menu name='FileAction' action='FileAction'><menuitem name='stopAction' action='stopAction'/></menu><menu name='OptionsAction' action='OptionsAction'><menuitem name='WordWrapAction' action='WordWrapAction'/><menuitem name='ShowParseTreeBeforeEvaluatingAction' action='ShowParseTreeBeforeEvaluatingAction'/><menuitem name='PrintInputBeforeEvaluatingAction' action='PrintInputBeforeEvaluatingAction'/></menu><menu name='ModeAction' action='ModeAction'><menuitem name='EvaluateAction' action='EvaluateAction'/><menuitem name='ParseOnlyAction' action='ParseOnlyAction'/></menu></menubar></ui>");
		this.menubar1 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubar1")));
		this.menubar1.Name = "menubar1";
		this.vbox1.Add (this.menubar1);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.menubar1]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.output = new global::Gtk.TextView ();
		this.output.Buffer.Text = "Welcome to Irontalk.\n© 2010 William Lahti. This software is licensed under the GNU General Public License version 3.0 or later. For details type:\n\n\tSmalltalk license\n\n";
		this.output.CanFocus = true;
		this.output.Name = "output";
		this.output.Editable = false;
		this.output.CursorVisible = false;
		this.GtkScrolledWindow.Add (this.output);
		this.vbox1.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
		w4.Position = 1;
		this.vpaned1.Add (this.vbox1);
		global::Gtk.Paned.PanedChild w5 = ((global::Gtk.Paned.PanedChild)(this.vpaned1[this.vbox1]));
		w5.Resize = false;
		w5.Shrink = false;
		// Container child vpaned1.Gtk.Paned+PanedChild
		this.notebook1 = new global::Gtk.Notebook ();
		this.notebook1.CanFocus = true;
		this.notebook1.Name = "notebook1";
		this.notebook1.CurrentPage = 1;
		this.notebook1.TabPos = ((global::Gtk.PositionType)(3));
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.vbox2 = new global::Gtk.VBox ();
		this.vbox2.Name = "vbox2";
		this.vbox2.Spacing = 6;
		// Container child vbox2.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.input = new global::Gtk.Entry ();
		this.input.CanFocus = true;
		this.input.Name = "input";
		this.input.IsEditable = true;
		this.input.InvisibleChar = '●';
		this.hbox1.Add (this.input);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.input]));
		w6.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.eval = new global::Gtk.Button ();
		this.eval.CanFocus = true;
		this.eval.Name = "eval";
		this.eval.UseUnderline = true;
		this.eval.Label = global::Mono.Unix.Catalog.GetString ("Evaluate");
		this.hbox1.Add (this.eval);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.eval]));
		w7.Position = 1;
		w7.Expand = false;
		w7.Fill = false;
		this.vbox2.Add (this.hbox1);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
		w8.Position = 0;
		w8.Expand = false;
		w8.Fill = false;
		this.notebook1.Add (this.vbox2);
		// Notebook tab
		this.label1 = new global::Gtk.Label ();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("Quick");
		this.notebook1.SetTabLabel (this.vbox2, this.label1);
		this.label1.ShowAll ();
		// Container child notebook1.Gtk.Notebook+NotebookChild
		this.hbox3 = new global::Gtk.HBox ();
		this.hbox3.Name = "hbox3";
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.multiInput = new global::Gtk.TextView ();
		this.multiInput.CanFocus = true;
		this.multiInput.Name = "multiInput";
		this.GtkScrolledWindow1.Add (this.multiInput);
		this.hbox3.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.GtkScrolledWindow1]));
		w11.Position = 0;
		// Container child hbox3.Gtk.Box+BoxChild
		this.evalMulti = new global::Gtk.Button ();
		this.evalMulti.CanFocus = true;
		this.evalMulti.Name = "evalMulti";
		this.evalMulti.UseUnderline = true;
		this.evalMulti.Label = global::Mono.Unix.Catalog.GetString ("Evaluate");
		this.hbox3.Add (this.evalMulti);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.evalMulti]));
		w12.Position = 1;
		w12.Expand = false;
		w12.Fill = false;
		this.notebook1.Add (this.hbox3);
		global::Gtk.Notebook.NotebookChild w13 = ((global::Gtk.Notebook.NotebookChild)(this.notebook1[this.hbox3]));
		w13.Position = 1;
		// Notebook tab
		this.label2 = new global::Gtk.Label ();
		this.label2.Name = "label2";
		this.label2.LabelProp = global::Mono.Unix.Catalog.GetString ("Multiline");
		this.notebook1.SetTabLabel (this.hbox3, this.label2);
		this.label2.ShowAll ();
		this.vpaned1.Add (this.notebook1);
		global::Gtk.Paned.PanedChild w14 = ((global::Gtk.Paned.PanedChild)(this.vpaned1[this.notebook1]));
		w14.Resize = false;
		w14.Shrink = false;
		this.Add (this.vpaned1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 670;
		this.DefaultHeight = 343;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.WordWrapAction.Activated += new global::System.EventHandler (this.ToggleWordWrap);
		this.ShowParseTreeBeforeEvaluatingAction.Activated += new global::System.EventHandler (this.ToggleShowParseTree);
		this.PrintInputBeforeEvaluatingAction.Activated += new global::System.EventHandler (this.TogglePrintInput);
		this.input.Activated += new global::System.EventHandler (this.InputEnter);
		this.eval.Clicked += new global::System.EventHandler (this.EvalActivated);
		this.evalMulti.Clicked += new global::System.EventHandler (this.MultiInputActivated);
	}
}
