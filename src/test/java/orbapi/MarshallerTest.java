package orbapi;

import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

public class MarshallerTest {
    private mock obj;

    interface mock{
        MethodCall method(String arg);
    }

    @Before
    public void setUp() throws Exception {
        InvocationHandler handler = new InvocationHandler() {
            @Override
            public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {

                byte[] res = Marshaller.marshallMethod(method,args);
                return Marshaller.unMarshallObject(res /*, MethodCall.class*/);
            }
        };

        obj = (mock) Proxy.newProxyInstance(mock.class.getClassLoader(),new Class[]{mock.class},handler);
    }

    @After
    public void tearDown() throws Exception {
    }

    @Test
    public void marshallunmarshallTest() {
        System.out.println(obj.method("some_arg"));
    }
}