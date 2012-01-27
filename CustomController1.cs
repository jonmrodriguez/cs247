using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Microsoft.Research.Kinect.Nui;
using Coding4Fun.Kinect.Wpf;

using Vector = System.Windows.Vector;

namespace SkeletalTracking
{
    class CustomController1 : SkeletonController
    {
        private Line lineLeft = null;
        private Line lineRight = null;
        public CustomController1(MainWindow win) : base(win){}
        private const double FLASHLIGHT_SIZE = 10.0;
        private const double INFINITY = 10.0;

        //Inside Test derived from http://www.blackpawn.com/texts/pointinpoly/default.html

        private bool SameSide(Vector p1, Vector p2, Vector a, Vector b)
        {
            double c1 = Vector.CrossProduct(Vector.Subtract(b,a),Vector.Subtract(p1,a));
            double c2 = Vector.CrossProduct(Vector.Subtract(b,a), Vector.Subtract(p2,a));
            return c1 * c2 >= 0;
        }

        private bool isInsideFlashLight(Vector p, Vector hand, Vector elbow, ref Line line)
        {
            Vector ray = Vector.Subtract(hand, elbow);
            Vector perpendicular = new Vector();
            perpendicular.X = -ray.Y;
            perpendicular.Y = ray.X;
            perpendicular.Normalize();
            perpendicular = Vector.Multiply(FLASHLIGHT_SIZE, perpendicular);

            Vector a = Vector.Add(hand,perpendicular);
            Vector b = Vector.Subtract(hand, perpendicular);

            if (line != null)
                window.MainCanvas.Children.Remove(line);

            line = new Line();
            line.Stroke = SystemColors.WindowTextBrush;
            line.X1 = hand.X;
            line.Y1 = hand.Y;
            line.X2 = hand.X + ray.X * INFINITY;
            line.Y2 = hand.Y + ray.Y * INFINITY;
            window.MainCanvas.Children.Add(line);

            return SameSide(p, hand, elbow, a) && SameSide(p, hand, elbow, b);
        }

        public override void processSkeletonFrame(SkeletonData skeleton, Dictionary<int, Target> targets)
        {

            /*Flashlight arms implementation*/

            //Scale the joints to the size of the window
            Joint leftHand = skeleton.Joints[JointID.HandLeft].ScaleTo(640, 480, window.k_xMaxJointScale, window.k_yMaxJointScale);
            Joint rightHand = skeleton.Joints[JointID.HandRight].ScaleTo(640, 480, window.k_xMaxJointScale, window.k_yMaxJointScale);
            Joint leftElbow = skeleton.Joints[JointID.ElbowLeft].ScaleTo(640, 480, window.k_xMaxJointScale, window.k_yMaxJointScale);
            Joint rightElbow = skeleton.Joints[JointID.ElbowRight].ScaleTo(640, 480, window.k_xMaxJointScale, window.k_yMaxJointScale);

            Vector lhp = new Vector();
            lhp.X = leftHand.Position.X; lhp.Y = leftHand.Position.Y;
            Vector rhp = new Vector();
            rhp.X = rightHand.Position.X; rhp.Y = rightHand.Position.Y;
            Vector lep = new Vector();
            lep.X = leftElbow.Position.X; lep.Y = leftElbow.Position.Y;
            Vector rep = new Vector();
            rep.X = rightElbow.Position.X; rep.Y = rightElbow.Position.Y;

            foreach (var target in targets)
            {
                Target cur = target.Value;
                int targetID = cur.id; //ID in range [1..5]

                Vector p = new Vector();
                p.X = (float)cur.getXPosition(); p.Y = (float)cur.getYPosition();                
         
                //If in the flashlight, highlight the target
            
                if (isInsideFlashLight(p,lhp,lep,ref lineLeft) || isInsideFlashLight(p,rhp,rep,ref lineRight))
                {
                    cur.setTargetHighlighted();
                    // jon work here
                    // 

                    running_average_XY_velocity;
                    running_average_XY_pointingray;
                    running_average_percent_hittedness[nTargets];
                    running_

                    // constants
                    THRESHOLD for each of above 4;


                    float punchiness = dot product (
                            normalize(running_average_XY_velocity),
                            normalize(running_average_XY_pointingray));

                    if (punchiness >= THRESHOLD_PUNCHINESS)
                    {
                        for target in targets:
                        {
                            target.setSelected();
                        }
                    }

                }
                else
                {
                    cur.setTargetUnselected();
                }
            }


        }

        public override void controllerActivated(Dictionary<int, Target> targets)
        {
        }
    }
}
