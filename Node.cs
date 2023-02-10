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
        // Used for dragging functionality.
        private bool isMoving;
        private Point previousLocation; 

        // Maintaining the pen object improves rendering efficiency.
        private Pen pen;
        public Node() {
            this.isMoving = false;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onClicked);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onReleased);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onCursorMoves);
            this.pen = new Pen(Color.Black, 1);
        }

        /// <summary>
        /// Stores location when left-clicked and enables moving state.
        /// </summary>
        private void onClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                previousLocation = e.Location;
                this.isMoving = true;
            }
        }

        /// <summary>
        /// Offset location to cursor position if in moving state.
        /// </summary>
        private void onCursorMoves(object sender, MouseEventArgs e) { 
            if (this.isMoving)
            {
                Point location = this.Location;
                location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
                this.Location = location;
            }
        }

        /// <summary>
        /// Disables moving state when left-click released.
        /// </summary>
        private void onReleased(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.isMoving = false;
            }
        }

        /// <summary>
        /// Renders circle.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Calls the parent paint method.
            base.OnPaint(e);

            // Set the node's region (wihin it's container) as a circle.
            System.Drawing.Drawing2D.GraphicsPath circleRegion = new System.Drawing.Drawing2D.GraphicsPath();
            circleRegion.AddEllipse(this.ClientRectangle);
            this.Region = new System.Drawing.Region(circleRegion);

            // Enables antialiasing and renders the circle.
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectangle rectangle = this.ClientRectangle;
            rectangle.Inflate(-1, -1); // Shrinks circle by a pixel to prevent it being drawn out of region.
            e.Graphics.DrawEllipse(this.pen, rectangle);
        }
    }
}
