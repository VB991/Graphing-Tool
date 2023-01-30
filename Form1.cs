namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int nodeDiameter = 50; //diameter of node in pixels

        /// <summary>
        /// Draws a new node for the graph, at the position in pixels from the top left of the container
        /// </summary>
        /// <param name="name">name of the node</param>
        /// <param name="xPos">x coordinate of mouse click</param>
        /// <param name="yPos">y coordinate of mouse click</param>

        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        //e is object containing click event information
        {
            if (e.Button == MouseButtons.Left)
            {
                Node node = new Node();
                node.Location = e.Location;
                node.Size = new Size(nodeDiameter, nodeDiameter);   
                node.Name = "myNode";
                this.mainPanel.Controls.Add(node);
                //.Location.X and Y is cursor position relative to mainPanel
            }
        }
            //.Location.X and Y is cursor position relative to mainPanel
            drawNode(e.Location.X, e.Location.Y);
        }
            //.Location.X and Y is cursor position relative to mainPanel
            drawNode(e.Location.X, e.Location.Y);
        }
    }
}