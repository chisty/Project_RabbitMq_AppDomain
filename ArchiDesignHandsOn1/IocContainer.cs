using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ArchiDesignHandsOn1
{
    public class IocContainer
    {
        public static IocContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new IocContainer();
                        }
                    }
                }
                return _instance;
            }
        }

        private IocContainer()
        {
            Catalog= new AggregateCatalog();
            AssemblyLists= new List<string>();
            AddAssembly(Assembly.GetExecutingAssembly());
        }

        public T Resolve<T>()
        {
            Create();
            lock (LockObject)
            {
                return Container.GetExportedValue<T>() ;
            }
        }

        public T Resolve<T>(string name)
        {
            Create();
            lock (LockObject)
            {
                return Container.GetExportedValue<T>(name);
            }
        }

        public void Create()
        {
            if (Container == null)
            {
                lock (LockObject)
                {
                    if (Container == null)
                    {
                        Container= new CompositionContainer(Catalog);
                    }
                }
            }
        }

        public void AddAssembly<T>()
        {
            AddAssembly(typeof(T).Assembly);
        }

        public void AddAssembly(Assembly assembly)
        {
            if (AssemblyLists.Contains(assembly.FullName))
            {
                return;
            }
            Catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            AssemblyLists.Add(assembly.FullName);
        }


        #region Property
        private static IocContainer _instance;
        private readonly static object LockObject = new object();
        private CompositionContainer Container { get; set; }
        private AggregateCatalog Catalog { get; set; }
        public List<string> AssemblyLists { get; set; }
        #endregion
    }
}
