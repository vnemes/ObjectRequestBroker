package examples.math;

import orbapi.ORB;

public class MathServer implements IMath {
    private static final int MATH_SERVER_PORT = 1010;
    private static final String MATH_SERVER_NAME = "MathServer";

    @Override
    public double do_add(double a, double b) {
        return a+b;
    }

    @Override
    public double do_sqr(double a) {
        return Math.sqrt(a);
    }

    public static void main(String[] args) {
        MathServer mathServer = new MathServer();
        ORB.register(MATH_SERVER_NAME,MATH_SERVER_PORT,"math",mathServer);

    }
}
