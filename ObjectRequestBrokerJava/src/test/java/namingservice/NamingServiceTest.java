package namingservice;

import ORB.namingservice.NamingService;
import ORB.orbapi.Marshaller;
import ORB.requestreplyapi.entries.ExtendedEntry;
import ORB.requestreplyapi.requestreply.Requestor;
import ORB.namingservice.replies.LocalizationReply;
import ORB.namingservice.replies.RegistrationReply;
import ORB.namingservice.requests.LocalizationRequest;
import ORB.namingservice.requests.RegistrationRequest;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import ORB.vson.Vson;

public class NamingServiceTest {
    Thread t;

    @Before
    public void setUp() throws Exception {
        NamingService n = NamingService.getInstance();
        (t = new Thread(n::startService)).start();
        Thread.sleep(5);
    }

    @After
    public void tearDown() throws Exception {
        t.interrupt();
        t.stop();
    }

    @Test
    public void registerRequestTest() {
        Requestor r = new Requestor("SomeRequestor");
        RegistrationRequest rr = new RegistrationRequest("NASDAQ", new ExtendedEntry("192.168.0.1", 1324, "StockMarket"));
        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(rr));
        RegistrationReply reply = (RegistrationReply) Vson.fromJson(new String(resp));
        System.out.println(Vson.toJson(reply));
    }

    @Test
    public void localizationRequestTest(){
        registerRequestTest();
        Requestor r = new Requestor("SomeRequestor");
        LocalizationRequest lr = new LocalizationRequest("NASDAQ");
        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(lr));
        LocalizationReply reply = (LocalizationReply) Vson.fromJson(new String(resp));
        System.out.println(Vson.toJson(reply));
    }
}