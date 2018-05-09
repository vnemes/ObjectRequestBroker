package examples.info;

import orbapi.ORB;

public class InfoClient {

    public static void main(String[] args) {
        IInfo info = (IInfo) ORB.getObjectReference("InfoServer",IInfo.class);
        System.out.println("road info for Route66 is: " + info.get_road_info(66));
        System.out.println("temperature for Timisoara is: " + info.get_temp("Timisoara"));
    }
}
