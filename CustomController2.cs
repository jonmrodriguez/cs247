﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using Microsoft.Research.Kinect.Nui;
using Coding4Fun.Kinect.Wpf;

namespace SkeletalTracking
{
    class CustomController2 : SkeletonController
    {
        public CustomController2(MainWindow win) : base(win){}

        public override void processSkeletonFrame(SkeletonData skeleton, Dictionary<int, Target> targets)
        {

            /* YOUR CODE HERE*/

        }

        public override void controllerActivated(Dictionary<int, Target> targets)
        {

            System.Media.SoundPlayer myPlayer = new System.Media.SoundPlayer();
            myPlayer.SoundLocation = @"C:\Users\jon\AppData\Local\Temp\Temp2_P3StarterCode.zip\P3StarterCode\coin.wav";
            myPlayer.LoadAsync();
            myPlayer.Play();

        }
    }
}
