package namingservice;

import orbapi.Marshaller;
import requestreplyapi.entries.ExtendedEntry;
import requestreplyapi.requestreply.Requestor;
import com.google.gson.Gson;
import namingservice.replies.LocalizationReply;
import namingservice.replies.RegistrationReply;
import namingservice.requests.LocalizationRequest;
import namingservice.requests.RegistrationRequest;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;

public class NamingServiceTest {
    private Gson gson;
    Thread t;

    @Before
    public void setUp() throws Exception {
        gson = new Gson();
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
        RegistrationReply reply = gson.fromJson(new String(resp), RegistrationReply.class);
        System.out.println(gson.toJson(reply));
    }

    @Test
    public void localizationRequestTest(){
        registerRequestTest();
        Requestor r = new Requestor("SomeRequestor");
        LocalizationRequest lr = new LocalizationRequest("NASDAQ");
        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(lr));
        LocalizationReply reply = gson.fromJson(new String(resp),LocalizationReply.class);
        System.out.println(gson.toJson(reply));
    }
}