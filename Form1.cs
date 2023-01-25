using System.Windows.Forms.VisualStyles;

namespace Graphing_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int nodeDiameter = 40; //diameter of node in pixels

        /// <summary>
        /// Draws a new node for the graph, at the position in pixels from the top left of the container
        /// </summary>
        /// <param name="name">name of the node</param>
        /// <param name="xPos">x coordinate of mouse click</param>
        /// <param name="yPos">y coordinate of mouse click</param>
        private void drawNode(int xPos, int yPos)
        {
            //offests node centre to cursor position
            xPos -= nodeDiameter / 2;
            yPos -= nodeDiameter / 2;

            Pen pen = new Pen(Color.Black); //temporary pen object
            //calls create graphics method of mainPanel to draw circle
            this.mainPanel.CreateGraphics().DrawEllipse(pen, xPos,yPos, nodeDiameter,nodeDiameter);
        }

        private void mainPanel_Clicked(object sender, MouseEventArgs e)
        {
            //e is object containing click event information
            //.Location.X and Y is cursor position relative to mainPanel
            drawNode(e.Location.X, e.Location.Y);
        }
    }
}