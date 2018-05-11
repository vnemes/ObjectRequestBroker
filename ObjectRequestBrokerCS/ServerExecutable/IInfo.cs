namespace ServerExecutable
{
    public interface IInfo
    {
        string get_road_info(long road_ID);

        double get_temp(string city);
    }
}