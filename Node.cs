using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Graphing_Tool
{
    public class Node : Control
    {
        //used for dragging functionality
        private bool isMoving;
        private Point previousLocation; 

        //maintaining pen object improves rendering efficiency
        private Pen pen;
        public Node() {
            this.isMoving = false;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onClicked);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onReleased);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onCursorMoves);
            this.pen = new Pen(Color.Black, 1);
        }

        private void onClicked(object sender, MouseEventArgs e)
        {
            previousLocation = e.Location;
            this.isMoving = true;
        }

        private void onCursorMoves(object sender, MouseEventArgs e) { 
            if (this.isMoving)
            {
                Point location = this.Location;
                location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                this.Location = location;
            }
        }

        private void onReleased(object sender, MouseEventArgs e)
        {
            this.isMoving = false;
        }

        /// <summary>
        /// graphics rendering and region setting
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            //calls parent paint method
            base.OnPaint(e);

            //set the node's region in it's container as a circle
            System.Drawing.Drawing2D.GraphicsPath circleRegion = new System.Drawing.Drawing2D.GraphicsPath();
            circleRegion.AddEllipse(this.ClientRectangle);
            this.Region = new System.Drawing.Region(circleRegion);

            //enables antialiasing and renders the circle
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rectangle = this.ClientRectangle;
            rectangle.Inflate(-1, -1); //prevents circle being drawn out of region
            e.Graphics.DrawEllipse(this.pen, rectangle);
        }
    }
}
