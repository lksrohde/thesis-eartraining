/*
* Newton Interpolation von "Frenchy" zu finden auf
* https://stackoverflow.com/questions/53691899/polynomial-interpolation-newton-method (29.04.2021)
* ist lizenziert unter der CC BY 4.0 License
* hier in leicht angepasster Form
*/


using UnityEngine;

public class Interpolation {
    public const int Points = 400;
    private const int Order = 3;
    float[] y_k;
    float[] x_k;
    float[] poly;
    float[] y_p;
    float[] x_p;
    private float[] cutSpectrum;
        
    public Interpolation(float[] cutSpectrum) {
        this.cutSpectrum = cutSpectrum;
            
        y_k = new float[Order];
        x_k = new float[Order];
        poly = new float[Order];
        y_p = new float[Points];
        x_p = new float[Points];
    }


    public float GetMaxIndexInterpolated(int absMaxIndex) {
        if (absMaxIndex == 0) {
            return 0;
        }

        for (int i = 0; i < Order; i++) {
            y_k[i] = cutSpectrum[i + absMaxIndex - 1];
            x_k[i] = i + absMaxIndex - 1;
            
        }
        
        CalcElements(y_k, Order, 1);
        float max = 0;
        float maxi = 0;
        for (int i = 0; i < Points; i++)
        {
            x_p[i] = (i) * x_k[Order - 1] / Points;
            y_p[i] = Interpolate(x_p[i], Order);
                
            if (max < y_p[i]) {
                max = y_p[i];
                maxi = x_p[i];
            }
        }
        return maxi;
    }
    void CalcElements(float[] x, int order, int step)
    {
        if (order > 0 ) {
            var xx = new float[order];
            for (int i = 0; i < order-1; i++) {
                xx[i] = (x[i + 1] - x[i]) / (x_k[i + step] - x_k[i]);
            }
            poly[step - 1] = x[0];
            CalcElements(xx, order - 1, step + 1);
        }

    }

    float Interpolate(float xp, int order)
    {
        float yp = 0;
        for (int i = 1; i < order; i++) {
            var tempYp = poly[i];
            for (int k = 0; k < i; k++) {
                tempYp = tempYp * (xp - x_k[k]);
            }
            yp = yp + tempYp;
        }
        return poly[0] + yp;
    }     
}