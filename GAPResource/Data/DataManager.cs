using GAPResource.Interfaces;
using Microsoft.Extensions.Logging;
using System.Xml;

namespace GAPResource.Data
{
    public static class DataManager
    {
        private static IDataSource _data;
        private static bool _inited;
        private static readonly object _lock = new object();
        private static MSDataProvider _provider;

        public static CityRepository CityRepository => _provider.CityRepository;
        public static TruckRepository TruckRepository => _provider.TruckRepository;
        public static EdgeRepository EdgeRepository => _provider.EdgeRepository;

        public static OrderRepository OrderRepository => _provider.OrderRepository;
        public static void Init(XmlNode node)
        {
            if (!_inited)
            {
                lock (_lock)
                {
                    if (!_inited)
                    {
                        _data = new DataSource(node);
                        _provider = new MSDataProvider(_data);
                        _inited = true;
                        AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
                        {
                            (_data as DataSource)?.Dispose();
                        };
                    }
                }
            }
        }

    }
}
