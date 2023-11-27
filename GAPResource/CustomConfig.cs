using GAPResource.Data;
using GAPResource.Interfaces;
using System.Xml;

namespace GAPResource
{
    public class CustomConfig: ICustomConfig
    {
        private XmlDocument _config { get; set; }
        public void Init(string config = "CustomConf\\conf.cfg")
        {
            string sBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string ConfigFileName = Path.Combine(sBaseDirectory, config);

            if (!File.Exists(ConfigFileName))
            {
                Console.WriteLine("file config not found in " + sBaseDirectory);
                return;
            }
            _config = new XmlDocument();
            _config.Load(ConfigFileName);
            DataManager.Init(_config.FirstChild);

            //var cont = DataManager.CityRepository;
            //var dfgdfg = cont.OrderedItems;
          
            //foreach (var sdgfdsfg in dfgdfg)
            //{

            //}

            //var cont2 = DataManager.TruckRepository;
            //var dfgdfg2 = cont2.OrderedItems;

            //foreach (var sdgfdsfg in dfgdfg2)
            //{

            //}

        }

    }
}
