using Caliburn.Micro;
using ShowOnTime.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShowOnTime
{
    public class AppBootstrapper: BootstrapperBase
    {
        private CompositionContainer _container;
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container = new CompositionContainer(
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
                    )
                );

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(_container);

            _container.Compose(batch);
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = _container.GetExportedValues<object>(contract);

            if (exports.Any())
                return exports.First();

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            _container.SatisfyImportsOnce(instance);
        }

        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            List<Assembly> assemblies = new List<Assembly>();
            var assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies().Where(item => item.Name.Contains("Careland"));
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }
            assemblies.Add(typeof(AppBootstrapper).Assembly);
            //AssemblySource.Instance.Any() ?
            //new Assembly[] { } :
            //new[] { typeof(App).Assembly };
            return assemblies;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //new Summary.Controls.AppBootstrapper().Initialize();
            DisplayRootViewFor<MainWindowViewModel>();
        }
    }
}
