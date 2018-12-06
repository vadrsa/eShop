using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usefull
{
    /// <summary>
    /// Provides internal methods for creating property descriptors.
    /// This class should not be used directly.
    /// </summary>
    /// <summary>
    /// Provides internal methods for creating property descriptors.
    /// This class should not be used directly.
    /// </summary>
    internal class InternalPropertyDescriptorFactory : TypeConverter
    {
        public static PropertyDescriptor CreatePropertyDescriptor<TComponent,
               TProperty>(string name, Func<TComponent, TProperty> getter,
               Action<TComponent, TProperty> setter)
        {
            return new GenericPropertyDescriptor<TComponent,
                       TProperty>(name, getter, setter);
        }

        public static PropertyDescriptor CreatePropertyDescriptor<TComponent,
               TProperty>(string name, Func<TComponent, TProperty> getter)
        {
            return new GenericPropertyDescriptor<TComponent,
                       TProperty>(name, getter);
        }

        public static PropertyDescriptor CreatePropertyDescriptor(string name,
               Type componentType, Type propertyType, Func<object, object> getter,
               Action<object, object> setter)
        {
            return new GenericPropertyDescriptor(name, componentType,
                       propertyType, getter, setter);
        }

        public static PropertyDescriptor CreatePropertyDescriptor(string name,
               Type componentType, Type propertyType, Func<object, object> getter)
        {
            return new GenericPropertyDescriptor(name, componentType,
                                                 propertyType, getter);
        }

        protected class GenericPropertyDescriptor<TComponent, TProperty> :
                        TypeConverter.SimplePropertyDescriptor
        {
            Func<TComponent, TProperty> getter;
            Action<TComponent, TProperty> setter;

            public GenericPropertyDescriptor(string name, Func<TComponent,
                   TProperty> getter, Action<TComponent, TProperty> setter)
                 : base(typeof(TComponent), name, typeof(TProperty))
            {
                if (getter == null)
                {
                    throw new ArgumentNullException("getter");
                }
                if (setter == null)
                {
                    throw new ArgumentNullException("setter");
                }

                this.getter = getter;
                this.setter = setter;
            }

            public GenericPropertyDescriptor(string name,
                   Func<TComponent, TProperty> getter)
                 : base(typeof(TComponent), name, typeof(TProperty))
            {
                if (getter == null)
                {
                    throw new ArgumentNullException("getter");
                }

                this.getter = getter;
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this.setter == null;
                }
            }

            public override object GetValue(object target)
            {
                TComponent component = (TComponent)target;
                TProperty value = this.getter(component);
                return value;
            }

            public override void SetValue(object target, object value)
            {
                if (!this.IsReadOnly)
                {
                    TComponent component = (TComponent)target;
                    TProperty newValue = (TProperty)value;
                    this.setter(component, newValue);
                }
            }
        }

        protected class GenericPropertyDescriptor :
                        TypeConverter.SimplePropertyDescriptor
        {
            Func<object, object> getter;
            Action<object, object> setter;

            public GenericPropertyDescriptor(string name, Type componentType,
                   Type propertyType, Func<object, object> getter,
                   Action<object, object> setter)
                 : base(componentType, name, propertyType)
            {
                if (getter == null)
                {
                    throw new ArgumentNullException("getter");
                }
                if (setter == null)
                {
                    throw new ArgumentNullException("setter");
                }

                this.getter = getter;
                this.setter = setter;
            }

            public GenericPropertyDescriptor(string name, Type componentType,
                   Type propertyType, Func<object, object> getter)
                 : base(componentType, name, propertyType)
            {
                if (getter == null)
                {
                    throw new ArgumentNullException("getter");
                }

                this.getter = getter;
            }

            public override bool IsReadOnly
            {
                get
                {
                    return this.setter == null;
                }
            }

            public override object GetValue(object target)
            {
                object value = this.getter(target);
                return value;
            }

            public override void SetValue(object target, object value)
            {
                if (!this.IsReadOnly)
                {
                    object newValue = (object)value;
                    this.setter(target, newValue);
                }
            }
        }
    }


    /// <summary>
    /// Provides methods for easily creating property descriptors.
    /// </summary>
    public static class PropertyDescriptorFactory
    {
        /// <summary>
        /// Creates a custom property descriptor.
        /// </summary>
        /// <typeparam name="TComponent">The component type.</typeparam>
        /// <typeparam name="TProperty">The parameter type.</typeparam>
        /// <param name="name">The name of the property.</param>
        /// <param name="getter">A function that takes
        /// a component and gets this property's value.</param>
        /// <param name="setter">An action that takes
        /// a component and sets this property's value.</param>
        /// <returns>A customer property descriptor.</returns>
        public static PropertyDescriptor CreatePropertyDescriptor<TComponent,
               TProperty>(string name, Func<TComponent, TProperty> getter,
               Action<TComponent, TProperty> setter)
        {
            return InternalPropertyDescriptorFactory.CreatePropertyDescriptor<TComponent,
                   TProperty>(name, getter, setter);
        }

        /// <summary>
        /// Creates a custom read-only property descriptor.
        /// </summary>
        /// <typeparam name="TComponent">The component type.</typeparam>
        /// <typeparam name="TProperty">The parameter type.</typeparam>
        /// <param name="name">The name of the read-only property.</param>
        /// <param name="getter">A function that takes
        /// a component and gets this property's value.</param>
        /// <returns>A customer property descriptor.</returns>
        public static PropertyDescriptor CreatePropertyDescriptor<TComponent,
               TProperty>(string name, Func<TComponent, TProperty> getter)
        {
            return InternalPropertyDescriptorFactory.CreatePropertyDescriptor<TComponent,
                                      TProperty>(name, getter);
        }

        /// <summary>
        /// Creates a custom property descriptor.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="componentType">A System.Type that represents
        /// the type of component to which this property descriptor binds.</param>
        /// <param name="propertyType">A System.Type that
        ///       represents the data type for this property.</param>
        /// <param name="getter">A function that takes
        ///       a component and gets this property's value.</param>
        /// <param name="setter">An action that takes
        ///       a component and sets this property's value.</param>
        /// <returns>A customer property descriptor.</returns>
        public static PropertyDescriptor CreatePropertyDescriptor(string name,
               Type componentType, Type propertyType, Func<object,
               object> getter, Action<object, object> setter)
        {
            return InternalPropertyDescriptorFactory.CreatePropertyDescriptor(name,
                   componentType, propertyType, getter, setter);
        }

        /// <summary>
        /// Creates a custom read-only property descriptor.
        /// </summary>
        /// <param name="name">The name of the read-only property.</param>
        /// <param name="componentType">A System.Type that represents
        ///           the type of component to which this property descriptor binds.</param>
        /// <param name="propertyType">A System.Type
        ///           that represents the data type for this property.</param>
        /// <param name="getter">A function that takes
        ///           a component and gets this property's value.</param>
        /// <returns>A customer property descriptor.</returns>
        public static PropertyDescriptor CreatePropertyDescriptor(string name,
               Type componentType, Type propertyType, Func<object, object> getter)
        {
            return InternalPropertyDescriptorFactory.CreatePropertyDescriptor(name,
                                              componentType, propertyType, getter);
        }
    }
}
