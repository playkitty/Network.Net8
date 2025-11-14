
namespace ServerModule
{
    public interface IConfig
    {
        int receiveBandWidth { get; set; }
        int sendBandWidth { get; set; }
        int connectionPoolSize { get; set; }
    }
}
