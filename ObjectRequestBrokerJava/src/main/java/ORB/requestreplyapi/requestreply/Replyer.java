package ORB.requestreplyapi.requestreply;

import ORB.requestreplyapi.common.ByteStreamTransformer;
import ORB.requestreplyapi.common.IAddress;

import java.net.*;
import java.io.*;

public class Replyer {
    private ServerSocket srvS;
    private Socket s;
    private InputStream iStr;
    private OutputStream oStr;
    private String myName;
    private IAddress myAddr;

    public Replyer(String theName, IAddress theAddr) {
        myName = theName;
        myAddr = theAddr;
        try {
            srvS = new ServerSocket(myAddr.port(), 1000);
            System.out.println("Replyer Serversocket:" + srvS);
        } catch (Exception e) {
            System.out.println("Error opening server socket");
        }
    }


    public void receive_transform_and_send_feedback(ByteStreamTransformer t) {
        int val;
        byte buffer[] = null;
        try {
            s = srvS.accept();
            System.out.println("Replyer accept: Socket" + s);
            iStr = s.getInputStream();
            //correction performed for objects bigger than 256 bytes
            val = iStr.read() << 8;
            val |= iStr.read();
            buffer = new byte[val];
            iStr.read(buffer);

            byte[] data = t.transform(buffer);

            oStr = s.getOutputStream();
            oStr.write(data.length >> 8);
            oStr.write(data.length & 0xFF);
            oStr.write(data);
            oStr.flush();
            oStr.close();
            iStr.close();
            s.close();

        } catch (IOException e) {
            System.out.println("IOException in receive_transform_and_feedback");
        }

    }

    protected void finalize() throws Throwable {
        super.finalize();
        srvS.close();
    }
}

