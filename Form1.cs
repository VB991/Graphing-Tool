using System.Windows.Forms.VisualStyles;

namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int nodeDiameter = 70; //diameter of node in pixels

        /// <summary>
        /// Draws a new node for the graph, at the position in pixels from the top left of the container
        /// </summary>
        /// <param name="name">name of the node</param>
        /// <param name="xPos">x coordinate of mouse click</param>
        /// <param name="yPos">y coordinate of mouse click</param>
        private void drawNode(String name, int xPos, int yPos)
        {
            Pen pen = new Pen(Color.Black); //temporary pen object
            //calls create graphics method of mainPanel to draw circle
            this.mainPanel.CreateGraphics().DrawEllipse(pen, xPos,yPos, nodeDiameter,nodeDiameter);
        }

        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        {
            drawNode("node", e.Location.X, e.Location.Y);
        }
    }
}