package examples.math;

import ORB.orbapi.ORB;

public class MathClient {

    public static void main(String[] args) {
        IMath math = (IMath)ORB.getObjectReference("MathServer",IMath.class);
        System.out.println("1.5 + 2.4 = "+ math.do_add(1.5,2.4));
        System.out.println("sqrt(16.1) = " + math.do_sqr(16.1));
    }
}
