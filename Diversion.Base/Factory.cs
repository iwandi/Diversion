using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Diversion.Base
{
    // TODO : Count usage to get rid of unused cached calls
    // TODO : Use Emit for constructot if possible
    // TODO : uses second list for reflection constructors so we can replace them with generics if 
    //        the right call to CreateInstanceGeneric gets made

    // PERF : Register custom FactoryMethodes for know types to avoid reflection
    public delegate object FectoryDelegate();

    public static class Factory
    {
        static Dictionary<Type, FectoryDelegate> constructors = new Dictionary<Type, FectoryDelegate>();

        public static void SetFactoryMethode(Type type, FectoryDelegate constr)
        {
            constructors[type] = constr;
        }

        // uses Reflection as a fallback
        public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            FectoryDelegate constr;
            if (constructors.TryGetValue(type, out constr))
            {
                return (T)constr();
            }
            // we cant instance abstract or interface
            if (type.IsAbstract || type.IsInterface)
            {
                return default(T);
            }
            ReflectionFactory fab = new ReflectionFactory();
            fab.Initialize(type);
            if (fab.IsValid)
            {
                SetFactoryMethode(type, new FectoryDelegate(fab.CreateInstance));
                return (T)fab.CreateInstance();
            }
            return default(T);
        }

        // uses Genrics as fallback
        public static T CreateInstanceGeneric<T>() where T : new()
        {
            Type type = typeof(T);
            FectoryDelegate constr;
            if (constructors.TryGetValue(type, out constr))
            {
                return (T)constr();
            }
            // we cant instance abstract or interface
            if (type.IsAbstract || type.IsInterface)
            {
                return default(T);
            }
            GenericFactory<T> fab = new GenericFactory<T>();
            SetFactoryMethode(type, new FectoryDelegate(fab.CreateInstance));
            return (T)fab.CreateInstance();
        }

        // T is only to typecast and give better defaults
        public static T CreateInstance<T>(Type type)
        {
            Type castType = typeof(T);
            // TODO : Check this
            if (castType.IsAssignableFrom(type))
            {
                FectoryDelegate constr;
                if (constructors.TryGetValue(type, out constr))
                {
                    return (T)constr();
                }
                // we cant instance abstract or interface
                if (type.IsAbstract || type.IsInterface)
                {
                    return default(T);
                }
                ReflectionFactory fab = new ReflectionFactory();
                fab.Initialize(type);
                if (fab.IsValid)
                {
                    SetFactoryMethode(type, new FectoryDelegate(fab.CreateInstance));
                    return (T)fab.CreateInstance();
                }
                return default(T);
            }
            return default(T);
        }

        public static object CreateInstance(Type type)
        {
            FectoryDelegate constr;
            if (constructors.TryGetValue(type, out constr))
            {
                return constr();
            }
            // we cant instance abstract or interface
            if (type.IsAbstract || type.IsInterface)
            {
                return null;
            }
            ReflectionFactory fab = new ReflectionFactory();
            fab.Initialize(type);
            if (fab.IsValid)
            {
                SetFactoryMethode(type, new FectoryDelegate(fab.CreateInstance));
                return fab.CreateInstance();
            }
            return null;
        }

        class ReflectionFactory
        {
            static readonly Type[] constructorPattern = new Type[] { };
            static readonly object[] constuctorParams = new object[] { };

            ConstructorInfo constructor;

            public bool IsValid
            {
                get { return constructor != null; }
            }

            public void Initialize(Type t)
            {
                ConstructorInfo info = t.GetConstructor(constructorPattern);
                if (info.IsPublic)
                {
                    constructor = info;
                }
            }

            public object CreateInstance()
            {
                return constructor.Invoke(constuctorParams);
            }
        }

        class GenericFactory<T> where T : new()
        {
            public object CreateInstance()
            {
                return new T();
            }
        }
    }
}
