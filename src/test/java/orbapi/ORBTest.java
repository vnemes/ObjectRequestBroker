package orbapi;

import namingservice.NamingService;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

public class ORBTest {
    private Thread t, testt;
    private MockImpl obj = new MockImpl();


    interface IMock {
        String doSmth(String arg);
    }

    class MockImpl implements IMock {

        @Override
        public String doSmth(String arg) {
            return arg;
        }
    }


    @After
    public void tearDown() {
        testt.interrupt();
        testt.stop();
        t.interrupt();
        t.stop();
    }

    @Before
    public void setUp() throws Exception {
        obj = new MockImpl();
        NamingService n = new NamingService();
        (t = new Thread(n::startService)).start();
        (testt = new Thread(() -> ORB.register("NASDAQ", 1324, "StockMarket", obj))).start();
        Thread.sleep(20);
    }


    @Test
    public void getObjectReferenceTest()  {
        IMock m = (IMock) ORB.getObjectReference("NASDAQ", IMock.class);
        String retVal = m.doSmth("Value passed as argument");
        System.out.println('\n' + "Method call returned: " + retVal);

        assert retVal.equals("Value passed as argument");
    }

    @Test
    public void registerTest() {
        ORB.register("NASDAQ", 1324, "StockMarket", obj);
    }

    @Test
    public void getProviderAddressTest() {
        System.out.println('\n' + ORB.getProviderAddress("NASDAQ").toString());
    }
}