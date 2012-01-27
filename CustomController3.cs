using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Threading;
using Microsoft.Research.Kinect.Nui;
using Coding4Fun.Kinect.Wpf;

namespace SkeletalTracking
{
    class CustomController3 : SkeletonController
    {
        public CustomController3(MainWindow win) : base(win){}

        public override void processSkeletonFrame(SkeletonData skeleton, Dictionary<int, Target> targets)
        {

            foreach (var target in targets)
            {
                Target cur = target.Value;
                int targetID = cur.id; //ID in range [1..5]

                //Scale the joints to the size of the window
                Joint head = skeleton.Joints[JointID.Head].ScaleTo(640, 480, window.k_xMaxJointScale, window.k_yMaxJointScale);
              
                //Calculate how far our head is from the target in both x and y directions
                double deltaX = Math.Abs(head.Position.X - cur.getXPosition());
                double deltaY = Math.Abs(head.Position.Y - cur.getYPosition());


                //If we have a hit in a reasonable range, highlight the target
                if (deltaX < 30 && deltaY < 30)
                {
                    System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
                    myPlayer.SoundLocation = @"c:\Users\Tilley\Dropbox\CS247\P3StarterCode\coin.wav";
                    myPlayer.LoadAsync();
                    myPlayer.Play();
                    cur.setTargetSelected();
                }
                else
                {
                    cur.setTargetUnselected();
                }
            }

        }

        private void bgMusic()
        {
            System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
            myPlayer.SoundLocation = @"c:\Users\Tilley\Dropbox\CS247\P3StarterCode\theme.wav";
            myPlayer.Play();
        }

        public override void controllerActivated(Dictionary<int, Target> targets)
        {

            Thread mBeepThread = new Thread(new ThreadStart(bgMusic));
            mBeepThread.Name = "Music Thread";
            mBeepThread.Start();
        }
    }
}
