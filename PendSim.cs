//============================================================================
// PendSim.cs : Defines derived class for simulating a simple pendulum.
//============================================================================
using System;


public class PendSim : Simulator
{
    double L;   // pendulum length
    double k;     // spring constant
    double m;       // mass of the pendulum
    double Fs;    
    double Ke;
    double Pe;
    double Delta;    
    public PendSim() : base(6)
    {

        x[0] = -0.7;   // default x position
        x[1] = 0.0;   // default velocity of x
        x[2] = -0.2;   // default y position
        x[3] = 0.0;   // default velocity of y
        x[4] = 0.5;   // default z position
        x[5] = 0.9;   // default velocity of z

        SetRHSFunc(RHSFuncPendulum);

    }

    //----------------------------------------------------
    // RHSFuncPendulum
    //----------------------------------------------------

    private void RHSFuncPendulum(double[] xx, double t, double[] ff)
    {
    double Lf = Math.Sqrt(xx[0] * xx[0] + xx[2] * xx[2] + xx[4] * xx[4]);
    double Fs = k * (L-Lf);
    double Velocity = Math.Sqrt(xx[1] * xx[1] + xx[3] * xx[3] + xx[5] * xx[5]);
    double Delta = Lf - L;

    ff[0] = xx[1];
    ff[1] = (xx[0] * Fs) / (m * Lf);
    ff[2] = xx[3];
    ff[3] = ((xx[2] * Fs) / (m * Lf)) - g;
    ff[4] = xx[5];
    ff[5] = (xx[4] * Fs) / (m * Lf);

    Pe = 0.5*m * Velocity * Velocity;
    Ke = 0.5*k * Delta * Delta + m * g * xx[2];
    }

    //--------------------------------------------------------------------
    // Getters
    //--------------------------------------------------------------------
  
   public double K
    {
        get { return k; }
        set { k = value; }

    }
     public double M
    {
        get { return m; }
        set { m = value; }

    }
     public double NaturalLength
    {
        get { return L; }
        set { L = value; }

    }
    public double X
    {
        get { return x[0]; }
        set { x[0] = value; }

    }

    public double Y
    {
        get { return x[2]; }
        set { x[2] = value; }
    }

    public double Z
    {
        get { return x[4]; }
        set { x[4] = value; }
    }

    public double Xdot
    {
        get { return x[1]; }
        set { x[1] = value; }

    }

    public double Ydot
    {
        get { return x[3]; }
        set { x[3] = value; }
    }

    public double Zdot
    {
        get { return x[5]; }
        set { x[5] = value; }
    }

   public double Kinetic
    {
        get { return Ke; }
        set { Ke = value; }
    }

    public double Potential
    {
        get { return Pe; }
        set { Pe = value; }
    }

    public double SpringForce
    {
        get { return Fs; }
        set { Fs = value; }
    }

    public double Stretch
    {
        get { return Delta; }
        set { Delta = value; }
    }

}
