using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FMC
{
    public partial class MainWindow : Form
    {
        // Wird nicht verwendet, muss aber stehen bleiben
        private void MainWindow_Load(object sender, EventArgs e)
        {
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }


        // Init
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }


        /*
        public void ShowModObjectInDataGrid(ModObject modObject)
        {
            
            Console.WriteLine("clicked modObject id: "+modObject.id);
            mod_object_data_grid_view.Columns.Clear();
            // Header
            mod_object_data_grid_view.Columns.Add("Property", "Property");
            mod_object_data_grid_view.Columns.Add("Value", "Value");

            // Daten aus ModObject.properties hinzufügen
            foreach (PropertyInfo property in modObject.properties)
            {
                if (property.editable && property.gridInfo.valueBoxType == "text")
                {
                    var type = property.type.ToString();
                    string value = property.value.ToString();
                    mod_object_data_grid_view.Rows.Add(type.ToString(), $"{value}");
                }
            }
            

        }
        */
        public ModObject currentModObject;
        public TreeNode currentNode = new();
        void PlaceNodeInTreeView(TreeNode node)
        {
            ModObject nodesObject = node.Tag as ModObject;
            for (int i = 0;i < mod_objects_tree_view.Nodes.Count;i++)
            {
                for(int j = 0; j < mod_objects_tree_view.Nodes[i].Nodes.Count;j++)
                {
                    ModObject targetObject = mod_objects_tree_view.Nodes[i].Nodes[j].Tag as ModObject;
                    if (targetObject.id == nodesObject.id)
                    {
                        mod_objects_tree_view.Nodes[i].Nodes[j].Tag = nodesObject;
                    }
                }
            }
        }
        private void Event_NodeClick(object sender, TreeNodeMouseClickEventArgs senderEventArgs)
        {
            mod_object_data_grid_view.Columns.Clear();
            if (senderEventArgs.Node.Tag == null)
            {
                return;
            }

            Console.WriteLine("name of clicked node: "+(senderEventArgs.Node.Tag as ModObject).properties[0].value);

            if ((senderEventArgs.Node.Tag as ModObject) != null)
            {
                currentNode = senderEventArgs.Node;
            } else
            {
                return;
            }

            Console.WriteLine(currentNode.Text);
            currentModObject = currentNode.Tag as ModObject;
            // Sicherstellen, dass der Sender ein TreeView ist
            if (sender is TreeView)
            {
                Console.WriteLine("clicked on node with id: " + (currentNode.Tag as ModObject).id);
                
                Console.WriteLine((currentNode.Tag as ModObject).id);
                Console.WriteLine("clicked node's name-property: "+(currentNode.Tag as ModObject).properties[0].value.ToString());
                GenerateDataGrid();
            }
        }
        void GenerateDataGrid()
        {
            
            mod_object_data_grid_view.Columns.Clear();
            mod_object_data_grid_view.Columns.Add("property", "Proptery");
            mod_object_data_grid_view.Columns.Add("value", "Value");
            Console.WriteLine($"[ModObjectType]: {currentModObject.type.ToString().ToLower()}");
            foreach (var prop in currentModObject.properties)
            {
                Console.WriteLine($"Enthält Properties: {prop.type} | Value: {prop.value}");
                if (prop.value != "" || prop.value != string.Empty)
                {
                    Console.WriteLine("Value is set");
                    mod_object_data_grid_view.Rows.Add(prop.type, prop.value.ToString());
                }
                else
                {
                    Console.WriteLine("Value is empty");
                    mod_object_data_grid_view.Rows.Add(prop.type, "...");
                }
                Console.WriteLine("-------------------------------------------");
                
            }
        }

        private void Event_OpenExplorer(object sender, EventArgs e)
        {
            Process.Start("explorer.exe", Program.standardProgramPath);
        }
        private void Event_DataGridError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Invalid value. Please select a value from the list.");
                e.ThrowException = false;
            }
        }
        private void Event_CellValueChange(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView data)
            {
                var rawIndex = data.CurrentCell.RowIndex; //Aktueller index von der Spalte die bearbeitet wird (Box) 
                var property = data.Rows[rawIndex].Cells[0].Value.ToString(); // Gibt die erstellte Spalte (Property die bearbeitet wird)
                var value = data.CurrentCell.Value; // Gibt den Wert der eingestellt wurde
                Console.WriteLine("------------------------------------value changed: "+value);


                currentModObject.SetPropertyValue(property, value);
                PlaceNodeInTreeView(currentNode);
                GenerateDataGrid();
            }



            /*



                currentModObject.SetPropertyValue(property.ToString(), value);
                currentModObject.DisplayPropertiesInformation();

                ModObject newModObject = currentNode.Tag as ModObject;
                currentModObject = newModObject;
            }
            */
        }

        private void Event_AddTemporaryModObjectToProject(object sender, EventArgs e)
        {


            /*
            currentModObject.saved = true;
            //currentNode.Tag = currentModObject;
            for (int i = 0; i < mod_objects_tree_view.Nodes[0].Nodes.Count; i++)
            {
                TreeNode node = mod_objects_tree_view.Nodes[0].Nodes[i];
                Console.WriteLine("searching nodes... found: "+node.Text);
                if (node.Tag is ModObject nodesModObject)
                {
                    Console.WriteLine("has attached ModObject with id: " + nodesModObject.id);
                    if (nodesModObject.id == currentModObject.id)
                    {
                        Console.WriteLine("search id: "+nodesModObject.id+" ,needed id: "+currentModObject.id);
                        Console.WriteLine("saved: " + nodesModObject.saved);
                        Console.WriteLine("tree view node (name property): "+ (node.Tag as ModObject).properties[0].value);
                        mod_objects_tree_view.Nodes[0].Nodes[i] = node;
                        Console.WriteLine("new tree view node (name property): " + (node.Tag as ModObject).properties[0].value);
                        currentModObject = new();
                        mod_object_data_grid_view.Columns.Clear();
                        return;
                    }

                }
            }
            */
        }
        private void Event_PrintTreeViewInConsole(object sender, EventArgs e)
        {

            /*
            Console.WriteLine("print tree view...");
            for (int i = 0; i < mod_objects_tree_view.Nodes.Count;  i++)
            {
                Console.WriteLine("["+ mod_objects_tree_view.Nodes.Count + "] ■ " + mod_objects_tree_view.Nodes[i].Text);
                for (int j = 0; j < mod_objects_tree_view.Nodes[i].Nodes.Count; j++)
                {
                    Console.WriteLine("["+ mod_objects_tree_view.Nodes[i].Nodes.Count + "] └── " + mod_objects_tree_view.Nodes[i].Nodes[j].Text);
                    string baseString = " ";
                    string inputString = "";
                    for (int v=0;i< mod_objects_tree_view.Nodes[i].Nodes.Count; i++)
                    {
                        inputString = $"{inputString}" + baseString;
                    }
                    var test = mod_objects_tree_view.Nodes[i].Nodes[j].Tag as ModObject;
                    Console.WriteLine("[" + (inputString) + "] └──── " + test.id);
                }
            }
            */
        }
    }
}
