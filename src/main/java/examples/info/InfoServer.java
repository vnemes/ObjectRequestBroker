package examples.info;

import orbapi.ORB;

import java.util.Random;

public class InfoServer implements IInfo {
    private final static String INFO_SERVER_NAME = "InfoServer";
    private final static int INFO_SERVER_PORT = 2020;

    @Override
    public String get_road_info(int road_ID) {
        return "Informations about road " + road_ID;
    }

    @Override
    public double get_temp(String city) {
        return new Random().nextDouble();
    }

    public static void main(String[] args) {
        InfoServer infoServer = new InfoServer();
        ORB.register(INFO_SERVER_NAME,INFO_SERVER_PORT,"info",infoServer);
    }
}
