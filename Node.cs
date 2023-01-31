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
        private bool isMoving;
        private Point previousLocation;

        public Node() {
            this.isMoving = false;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.onClicked);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.onReleased);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.onCursorMoves);
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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);


            Point newLocation = this.Location;
            newLocation.Offset(-(this.Size.Width/2), -(this.Size.Height/2));
            this.Location = newLocation;

            e.Graphics.FillEllipse(new SolidBrush(Color.Black), this.Location.X, this.Location.Y, this.Width, this.Height);

            /*
            System.Drawing.Drawing2D.GraphicsPath circleRegion =
            new System.Drawing.Drawing2D.GraphicsPath();

            // Set a new rectangle to the same size as the node's 
            // ClientRectangle property.
            System.Drawing.Rectangle newRectangle = this.ClientRectangle;

            newRectangle.Inflate(-1, -1);
            // Draw the circle
            e.Graphics.FillEllipse(new System.Drawing.SolidBrush(Color.Black), newRectangle);
            // Create a circle within the new rectangle.
            newRectangle.Inflate(1, 1);
            circleRegion.AddEllipse(newRectangle);

            // Set the node's Region property to the newly created 
            // circle region.
            this.Region = new System.Drawing.Region(circleRegion);
            */

        }
    }
}
