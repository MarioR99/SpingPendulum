using Godot;
using System;

public partial class SimBeginScene : Node3D
{
MeshInstance3D anchor;    
MeshInstance3D ball;


Label KELabel;
Label PELabel;
Label TotLabel;
int displayCtr;
int displayTHold;


double length0;
Vector3 dr;
Vector3 v0;
PendSim pend;
float length;   // length of pendulum
double time;
Vector3 endA;
Vector3 endB; 

    public override void _Ready()
    {
        endA = new Vector3(0.0f, 1.7f, 0.0f);
        dr = new Vector3(-0.7f, -0.2f, 0.5f);
        v0 = new Vector3(0.0f, 0.0f, 0.9f);
        endB = new Vector3();
        endB = endA + dr;

        anchor = GetNode<MeshInstance3D>("Anchor");
        ball = GetNode<MeshInstance3D>("Ball1");

        length0 = 0.9;
        anchor.Position = endA;
        ball.Position = endB;

        KELabel = GetNode<Label>("VBox/KELabel");
        PELabel = GetNode<Label>("VBox/PELabel");
        TotLabel = GetNode<Label>("VBox/TotLabel");

        displayCtr = 0;
        displayTHold = 2;

        pend = new PendSim();

        //default parameters
        pend.K = 90.0;
        pend.NaturalLength = length0;
        pend.M = 1.4;
        pend.X = dr.X;
        pend.Y = dr.Y;
        pend.Z = dr.Z;
        pend.Xdot = v0.X;
        pend.Ydot = v0.Y;
        pend.Zdot = v0.Z;
        time = 0.0;

        //spring.GenMesh(0.05f, 0.015f, length, 6.0f, 62);
}

    public override void _Process(double delta)
    {

        if (displayCtr > displayTHold)
        {
            double ke = pend.Kinetic;
            double pe = pend.Potential;
            double tot = ke + pe;

            KELabel.Text = "KE:" + ke.ToString("0.0000");
            PELabel.Text = "PE:" + pe.ToString("0.0000");
            TotLabel.Text = "Total:" + tot.ToString("0.0000");
            displayCtr = 0;
        }
    ++displayCtr;

    dr.X = (float)pend.X;
    dr.Y = (float)pend.Y;
    dr.Z = (float)pend.Z;

    endB = endA + dr;
    ball.Position = endB;

    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        pend.StepRK4(time, delta);
        time += delta;
    }

    
}
