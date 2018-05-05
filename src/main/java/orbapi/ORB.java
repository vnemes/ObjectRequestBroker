package orbapi;

import namingservice.NamingService;
import namingservice.replies.*;
import namingservice.requests.*;
import requestreplyapi.entries.Entry;
import requestreplyapi.entries.ExtendedEntry;
import requestreplyapi.requestreply.*;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;
import java.net.Inet4Address;
import java.net.UnknownHostException;


public class ORB {

    public static Object getObjectReference(String objectName, Class interfaceClass) {

        InvocationHandler handler = (proxy, method, args) -> {
            Requestor req = new Requestor("Requestor");
            byte[] reply = req.deliver_and_wait_feedback(getProviderAddress(objectName), Marshaller.marshallMethod(method, args));
            Object retval = Marshaller.unmarshallObject(reply, method.getReturnType());
            System.out.println("@called " + method.getName() + " from " + objectName + " and received " + retval.toString() + " as return value");
            return retval;
        };

        return Proxy.newProxyInstance(interfaceClass.getClassLoader(), new Class[]{interfaceClass}, handler);
    }


    public static void register(String name, int port, String entryType, Object object) {
        try {
            if (!registerToNamingService(name, port, entryType))
                return;

            Replyer replyer = new Replyer(name, new Entry(Inet4Address.getLocalHost().getHostAddress(), port));

            while (true)
                replyer.receive_transform_and_send_feedback(in -> {
                    MethodCall methodRequest = Marshaller.unmarshallMethod(in);
                    try {
                        Class[] paramtypes = new Class[methodRequest.getArgs().length];
                        for (int i = 0; i < methodRequest.getArgs().length; i++) {
                            paramtypes[i] = methodRequest.getArgs()[i].getClass();
                        }
                        Method reflMethod = object.getClass().getMethod(methodRequest.getMethodName(), paramtypes);
                        return Marshaller.marshallObject(reflMethod.invoke(object, methodRequest.getArgs()));
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                    return null;
                });

        } catch (UnknownHostException e) {
            e.printStackTrace();
        }
    }

    private static boolean registerToNamingService(String name, int port, String entryType) throws UnknownHostException {

        Requestor r = new Requestor(name);
        RegistrationRequest rr = new RegistrationRequest(name, new ExtendedEntry(Inet4Address.getLocalHost().getHostAddress(), port, entryType));
        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(rr));
        // todo: for deployment, the IP & Port of the naming service shall be retrieved from a configuration file
        RegistrationReply reply = (RegistrationReply) Marshaller.unmarshallObject(resp, RegistrationReply.class);
        if (reply.isRequest_resolved())
            System.out.println("@registered " + name);
        else {
            System.out.println("@cannot register " + name);
            return false;
        }
        return true;
    }

    public static ExtendedEntry getProviderAddress(String serviceName) {

        Requestor r = new Requestor("getter");
        LocalizationRequest lr = new LocalizationRequest(serviceName);
        byte[] resp = r.deliver_and_wait_feedback(NamingService.NAMING_SERVICE_ENTRY, Marshaller.marshallObject(lr));
        LocalizationReply reply = (LocalizationReply) Marshaller.unmarshallObject(resp, LocalizationReply.class);
        if (reply.isRequest_resolved()) {
            System.out.println("@localized " + serviceName + ": " + reply.getEntry_data().toString());
        } else {
            System.out.println("@cannot localize " + serviceName);
            return null;
        }

        return reply.getEntry_data();
    }




}