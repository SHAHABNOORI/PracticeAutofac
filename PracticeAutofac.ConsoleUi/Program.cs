using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
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

            //StartableSample(builder);
            //LifetimeEventsSampleOne(builder);
            //DisposalSample(builder);
            //CaptiveDependenciesSample(builder);
            //KeyedServiceSample(builder);
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

        private static void StartableSample(ContainerBuilder builder)
        {
            builder.RegisterType<MyClass>().AsSelf().As<IStartable>().SingleInstance();
            var container = builder.Build();
            container.Resolve<MyClass>();
        }

        private static void LifetimeEventsSampleTwo(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            builder.RegisterType<BadChild>();
            builder.RegisterType<Child>()
                //Before component be in use
                .OnActivating(handler =>
                {
                    Console.WriteLine("Child Activating...");

                    //Property Injection
                    handler.ReplaceInstance(handler.Context.Resolve<BadChild>());
                    handler.Instance.ChildId = Guid.NewGuid();
                    handler.Instance.Parent = handler.Context.Resolve<Parent>();
                }).OnActivated(handler => { Console.WriteLine("Child Activated"); }).OnRelease(handler =>
                {
                    Console.WriteLine("Child about to be removed");
                });

            //ERROR
            //builder.RegisterType<ConsoleLog>().As<ILog>()
            //    .OnActivating(handler => handler.ReplaceInstance(new SmsLog("09359167820")));

            builder.RegisterType<ConsoleLog>().AsSelf();
            builder.Register<ILog>(log => log.Resolve<ConsoleLog>())
                .OnActivating(handler => handler.ReplaceInstance(new SmsLog("09359167820")));

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine($"Parent :: {parent}");
                Console.WriteLine($"Child :: {child}");

                var log = scope.Resolve<ILog>();
                log.Write("Hello World.....");
            }
        }

        private static void LifetimeEventsSampleOne(ContainerBuilder builder)
        {
            builder.RegisterType<Parent>();
            builder.RegisterType<Child>()
                //Before component be in use
                .OnActivating(handler =>
                {
                    Console.WriteLine("Child Activating...");

                    //Property Injection
                    handler.Instance.ChildId = Guid.NewGuid();
                    handler.Instance.Parent = handler.Context.Resolve<Parent>();
                }).OnActivated(handler => { Console.WriteLine("Child Activated"); }).OnRelease(handler =>
                {
                    Console.WriteLine("Child about to be removed");
                });

            using (var scope = builder.Build().BeginLifetimeScope())
            {
                var child = scope.Resolve<Child>();
                var parent = child.Parent;
                Console.WriteLine(parent);
            }
        }

        private static void DisposalSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLog>().ExternallyOwned();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                scope.Resolve<ConsoleLog>();
            }
        }

        private static void CaptiveDependenciesSample(ContainerBuilder builder)
        {
            builder.RegisterType<ResourceManager>().SingleInstance();
            builder.RegisterType<SingletonResource>().As<IResource>().SingleInstance();
            builder.RegisterType<InstancePerDependencyResource>().As<IResource>().InstancePerDependency();

            using (var container = builder.Build())
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var resourceManagerOne = scope.Resolve<ResourceManager>();
                    var resourceManagerTwo = scope.Resolve<ResourceManager>();

                    foreach (var resource in resourceManagerOne.Resources)
                    {
                        resource.GuidGenerator("Resource Manager One :: ");
                    }

                    foreach (var resource in resourceManagerTwo.Resources)
                    {
                        resource.GuidGenerator("Resource Manager Two :: ");
                    }
                }
            }
        }

        private static void KeyedServiceSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().Keyed<ILogger>("cmd");
            builder.Register(c => new SmsLogger("09359167820")).Keyed<ILogger>("sms");
            builder.RegisterType<KeyedServiceReporting>();
            using var container = builder.Build();
            container.Resolve<KeyedServiceReporting>().Report();
        }

        private static void MetaDataWithSettingSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().WithMetadata<Settings>(
                c => c.For(x => x.LogMode, "verbose"));
            builder.RegisterType<MetaDataWithSettingReporting>();
            using var container = builder.Build();
            container.Resolve<MetaDataWithSettingReporting>().Report();
        }

        private static void MetaDataSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().WithMetadata("mode", "verbose");
            builder.RegisterType<MetaDataReporting>();
            using var container = builder.Build();
            container.Resolve<MetaDataReporting>().Report();
        }

        private static void EnumerationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.Register(c => new SmsLogger("09359167820")).As<ILogger>();
            builder.RegisterType<EnumerationReporting>();
            using var container = builder.Build();
            container.Resolve<EnumerationReporting>().Report();
        }

        private static void ParameterizedInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.RegisterType<SmsLogger>();
            builder.RegisterType<DynamicReporting>();
            using var container = builder.Build();
            container.Resolve<DynamicReporting>().Report();
        }

        private static void DynamicInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            //builder.RegisterType<ConsoleLogger>().SingleInstance();
            builder.RegisterType<SmsLogger>();
            builder.RegisterType<DynamicReporting>();
            using var container = builder.Build();
            container.Resolve<DynamicReporting>().Report();
        }

        private static void ControlledInstantiationSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType<OwnedReporting>();
            using var container = builder.Build();
            container.Resolve<OwnedReporting>().ReportOnce();
            Console.WriteLine("Done Reporting");
        }

        private static void DelayedInstantianSample(ContainerBuilder builder)
        {
            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType<Reporting>();
            using var container = builder.Build();
            container.Resolve<Reporting>().Report();
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
