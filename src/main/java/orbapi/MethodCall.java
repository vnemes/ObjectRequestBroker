package orbapi;

import java.util.Arrays;

public class MethodCall{
    private String methodName;
    private Object[] args;

    public MethodCall() {
    }

    MethodCall(String methodName, Object[] args) {
        this.methodName = methodName;
        this.args = args;
    }

    public String getMethodName() {
        return methodName;
    }


    public Object[] getArgs() {
        return args;
    }

    @Override
    public String toString() {
        return "MethodCall{" +
                "methodName='" + methodName + '\'' +
                ", args=" + Arrays.toString(args) +
                '}';
    }
}