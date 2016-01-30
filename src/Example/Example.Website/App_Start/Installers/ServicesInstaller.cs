using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System.Web.Mvc;
using Neo4jClient;

namespace Example.Website.App_Start.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //var url = "http://192.168.99.100:32770/db/data";
            //var user = "neo4j";
            //var password = "Password1!";

            var url = "http://sitecore.sb09.stations.graphenedb.com:24789/db/data";
            var user = "sitecore";
            var password = "w1c0t4yXWUV7dY8TotZ8";

            var client = new GraphClient(new Uri(url), user, password);

            container.Register(Component.For<IGraphClient>().Instance(client).LifestyleSingleton());

            //container.Register(Component.For<IExampleService>().ImplementedBy<ExampleService>().LifestylePerWebRequest());
        }
    }
}