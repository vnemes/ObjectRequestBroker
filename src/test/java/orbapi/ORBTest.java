package orbapi;

import namingservice.NamingService;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

import static org.junit.Assert.*;

public class ORBTest {
    private Thread t;

    @Before
    public void setUp() throws Exception {
        NamingService n = new NamingService();
        (t = new Thread(n::startService)).start();
    }

    @After
    public void tearDown() throws Exception {
        t.interrupt();
        t.stop();
    }

    @Test
    public void registerToNamingServiceTest() {
        ORB.registerToNamingService("NASDAQ",1324,"StockMarket");
    }

    @Test
    public void getProviderAddressTest() {
        registerToNamingServiceTest();

        System.out.println(ORB.getProviderAddress("NASDAQ").toString());
    }
}