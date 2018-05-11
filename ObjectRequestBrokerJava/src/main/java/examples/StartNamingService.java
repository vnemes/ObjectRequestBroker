package examples;

import ORB.namingservice.NamingService;

public class StartNamingService {

    public static void main(String[] args) {
        NamingService namingService = NamingService.getInstance();
        namingService.startService();
    }
}
