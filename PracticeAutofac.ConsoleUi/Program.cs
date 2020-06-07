using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.VisualBasic;
using PracticeAutofac.Library;
using Service = PracticeAutofac.Library.Service;

namespace PracticeAutofac.ConsoleUi
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            Console.ReadKey();



            //MetaDataWithSettingSample(builder);
            //MetaDataSample(builder);
            //EnumerationSample(builder);
            //ParameterizedInstantiationSample(builder);
            //DynamicInstantiationSample(builder);
            //ControlledInstantiationSample(builder);
            //DelayedInstantianSample(builder);
            //ScanningModulesSample(builder);
            //ScanTypesSample(builder, assembly);
            //MethodInjectionSample(builder);
            //PropertyInjectionSample(builder);
            //ObjectOnDemanSample(builder);
            //DelegateFactory(builder);
            //PassingParameterToRegister(builder);
            //DifferentConstructor();
            //OwnSampleOfOpenGenericComponents();
            //OpenGenericSample();
            //FasleDovom();
        }

        private static void MetaDataWithSettingSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().WithMetadata<Settings>(
                c => c.For(x => x.LogMode, "verbose"));
            builder.RegisterType<MetaDataWithSettingReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<MetaDataWithSettingReporting>().Report();
            }
        }

        private static void MetaDataSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().WithMetadata("mode", "verbose");
            builder.RegisterType<MetaDataReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<MetaDataReporting>().Report();
            }
        }

        private static void EnumerationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.Register(c => new SmsLogger("09359167820")).As<ILogger>();
            builder.RegisterType<EnumerationReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<EnumerationReporting>().Report();
            }
        }

        private static void ParameterizedInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.RegisterType<SmsLogger>();
            builder.RegisterType<DynamicReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<DynamicReporting>().Report();
            }
        }

        private static void DynamicInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.RegisterType<SmsLogger>();
            builder.RegisterType<DynamicReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<DynamicReporting>().Report();
            }
        }

        private static void ControlledInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType<OwnedReporting>();

            using (var container = builder.Build())
            {
                container.Resolve<OwnedReporting>().ReportOnce();
                Console.WriteLine("Done Reporting");
            }
        }

        private static void DelayedInstantianSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType<Reporting>();

            using (var container = builder.Build())
            {
                container.Resolve<Reporting>().Report();
            }
        }

        private static void ScanningModulesSample(ContainerBuilder builder)
        {
            //builder.RegisterAssemblyModules(typeof(Program).Assembly);
            builder.RegisterAssemblyModules<ParentChildModule>(Assembly.Load("PracticeAutofac.Library"));
            IContainer container = builder.Build();

            Console.WriteLine(container.Resolve<Child>().Parent);
        }

        private static void ScanTypesSample(ContainerBuilder builder)
        {
            var assembly = Assembly.Load("PracticeAutofac.Library");
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Log"))
                .Except<SmsLog>()
                .Except<ConsoleLog>(c => c.As<ILog>().SingleInstance())
                .AsSelf();


            builder.RegisterAssemblyTypes(assembly)
                .Except<SmsLog>()
                .Where(t => t.Name.EndsWith("Log"))
                .As(t => t.GetInterfaces()[0]);

            //var sample = container.Resolve<Sample>();
        }

        private static void MethodInjectionSample(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();

            //builder.Register(c =>
            //{
            //    var child = new Child();
            //    child.SetParent(c.Resolve<Parent>());
            //    return child;
            //});

            builder.RegisterType<Child>().OnActivated(e =>
            {
                var p = e.Context.Resolve<Parent>();
                e.Instance.SetParent(p);
            });


            var container = builder.Build();
            var parent = container.Resolve<Child>().Parent;
            Console.WriteLine(parent);
        }

        private static void PropertyInjectionSample(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            //builder.RegisterType<Child>().PropertiesAutowired();

            builder.RegisterType<Child>().WithProperty("Parent", new Parent());


            var container = builder.Build();
            var parent = container.Resolve<Child>().Parent;
            Console.WriteLine(parent);
        }

        private static void ObjectOnDemanSample(ContainerBuilder builder)
        {

            //builder.RegisterType<Entity>();
            //builder.RegisterType<AnotherViewModel>();

            //var container = builder.Build();
            //var viewModel = container.Resolve<AnotherViewModel>();

            //viewModel.Method();
            //viewModel.Method();

            builder.RegisterType<Entity>().InstancePerDependency();
            builder.RegisterType<ViewModel>();

            var container = builder.Build();
            var viewModel = container.Resolve<ViewModel>();

            viewModel.Method();
            viewModel.Method();
        }

        private static void DelegateFactory(ContainerBuilder builder)
        {
            builder.RegisterType<SomeService>();
            builder.RegisterType<DomainObject>();

            var container = builder.Build();
            var dobjOne = container.Resolve<DomainObject>(new PositionalParameter(1, 420));
            Console.WriteLine(dobjOne);

            var factory = container.Resolve<DomainObject.Factory>();
            var dobjTwo = factory(56);
            Console.WriteLine(dobjTwo);
        }

        private static void PassingParameterToRegister(ContainerBuilder builder)
        {
            //Named parameter
            //builder.RegisterType<SmsLog>().As<ILog>().WithParameter("phoneNumber", "09359167820");

            //typed parameter
            //builder.RegisterType<SmsLog>().As<ILog>().WithParameter(new TypedParameter(typeof(string),"09359167830"));

            //Resolved parameter
            //builder.RegisterType<SmsLog>().As<ILog>().WithParameter(new ResolvedParameter(
            //    (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "phoneNumber",
            //    (pi, ctx) => "09123635647"));

            Random random = new Random();
            builder.Register((c, p) =>
                new SmsLog(p.Named<string>("phoneNumber"))).As<ILog>();

            var container = builder.Build();
            var log = container.Resolve<ILog>(new NamedParameter("phoneNumber", random.Next().ToString()));
            log.Write("my message");
        }

        private static void DifferentConstructor()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>() /*.As<IConsole>()*/ /*.AsSelf()*/;
            builder.RegisterType<Sample>();
            IContainer container = builder.Build();
            var sample = container.Resolve<Sample>();
        }

        private static void OwnSampleOfOpenGenericComponents()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(ReadonlyRepository<>)).As(typeof(IRepository<>));
            builder.RegisterType<Service>();
            IContainer container = builder.Build();
            var service = container.Resolve<Service>();
            service.ShowMessage();
        }

        private static void OpenGenericSample()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(List<>)).As(typeof(IList<>));
            IContainer container = builder.Build();

            var myList = container.Resolve<IList<int>>();
            Console.WriteLine(myList.GetType());
        }

        private static void FasleDovom()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLog>().As<ILog>() /*.As<IConsole>()*/ /*.AsSelf()*/;
            //builder.RegisterType<EmailLog>().As<ILog>().PreserveExistingDefaults()/*.AsSelf()*/;

            //var log = new ConsoleLog();
            //builder.RegisterInstance(log).As<ILog>();


            //builder.RegisterType<Engine>();

            builder.Register(c => new Engine(c.Resolve<ILog>(), 123));

            //builder.RegisterType<Car>().UsingConstructor(typeof(Engine));
            builder.RegisterType<Car>();
            IContainer container = builder.Build();

            //ILog logOne = container.Resolve<ILog>();
            //logOne.Write("message from log one");

            //List<ILog> logs = container.Resolve<IEnumerable<ILog>>().ToList();
            //foreach (var log in logs)
            //{
            //    log.Write($"{log.GetType()}");
            //}


            var car = container.Resolve<Car>();
            car.Go();

        }
    }
}
